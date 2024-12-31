global using Wajba.Dtos.ItemsDtos;
global using Wajba.Enums;
global using Wajba.Models.Items;

namespace Wajba.ItemServices;

[RemoteService(false)]
public class ItemAppServices : ApplicationService
{
    private readonly IRepository<Item, int> _repository;
    private readonly IRepository<Category, int> _repository1;
    private readonly IRepository<Branch, int> _repositoryBranch;
    private readonly IImageService _imageService;

    public ItemAppServices(IRepository<Item, int> repository,
        IRepository<Category, int> repository1,
        IRepository<Branch, int> repositoryBranch,
        IImageService imageService)
    {
        _repository = repository;
        _repository1 = repository1;
        _repositoryBranch = repositoryBranch;
        _imageService = imageService;
    }
    public async Task<ItemDto> CreateAsync(CreateItemDto input)
    {
        string? imageUrl = null;
        if (input.ImageUrl != null)
            imageUrl = await _imageService.UploadAsync(input.ImageUrl);
        else throw new Exception("Image is required");
        Category category = await _repository1.FindAsync(input.CategoryId);
        if (category == null)
            throw new Exception("Category not found");
        List<ItemBranch> itemBranches = new List<ItemBranch>();
        if (input.BranchIds == null || input.BranchIds.Count == 0)
            throw new Exception("Branches are required");
        foreach (var branchId in input.BranchIds)
        {
            Branch branch = await _repositoryBranch.FindAsync(branchId);
            if (branch == null)
                throw new Exception("Branch not found");
            itemBranches.Add(new ItemBranch() { BranchId = branchId, Branch = branch });
        }
        Item item = new Item()
        {
            Name = input.Name,
            Description = input.Description,
            ImageUrl = imageUrl,
            CategoryId = input.CategoryId,
            IsFeatured = input.IsFeatured,
            TaxValue = input.TaxValue,
            Price = input.Price,
            ItemType = (Enums.ItemType)input.ItemType,
            Note = input.Note,
            Status = (Enums.Status)input.status,
            IsDeleted = false,
        };
        item.ItemBranches = itemBranches;
        await _repository.InsertAsync(item, true);
        return ObjectMapper.Map<Item, ItemDto>(item);
    }
    public async Task<ItemDto> UpdateAsync(int id, CreateItemDto input)
    {
        Item item = await _repository.FindAsync(id);
        if (item == null)
            throw new Exception("Item not found");
        if (input.ImageUrl != null)
            item.ImageUrl = await _imageService.UploadAsync(input.ImageUrl);
        Category category = await _repository1.FindAsync(input.CategoryId);
        if (category == null)
            throw new Exception("Category not found");
        List<ItemBranch> itemBranches = new List<ItemBranch>();
        foreach (var branchId in input.BranchIds)
        {
            Branch branch = await _repositoryBranch.FindAsync(branchId);
            if (branch == null)
                throw new Exception("Branch not found");
            itemBranches.Add(new ItemBranch() { BranchId = branchId, Branch = branch });
        }
        item.Name = input.Name;
        item.Description = input.Description;
        item.CategoryId = input.CategoryId;
        item.IsFeatured = input.IsFeatured;
        item.TaxValue = input.TaxValue;
        item.Price = input.Price;
        item.ItemType = (Enums.ItemType)input.ItemType;
        item.Note = input.Note;
        item.Status = (Enums.Status)input.status;
        item.ItemBranches = itemBranches;
        item.LastModificationTime = DateTime.UtcNow;
        Item item1 = await _repository.UpdateAsync(item, true);
        return ObjectMapper.Map<Item, ItemDto>(item1);
    }
    public async Task<ItemDto> GetByIdAsync(int id)
    {
        Item item = await _repository.GetAsync(id);
        return ObjectMapper.Map<Item, ItemDto>(item);
    }
    public async Task<PagedResultDto<ItemDto>> GetListAsync(GetItemInput input)
    {
        IQueryable<Item> queryable = await _repository.GetQueryableAsync();
        IQueryable<Category> categoryQueryable = await _repository1.GetQueryableAsync();

        // Join query to include category name
        var query = from item in queryable
                    join category in categoryQueryable
                    on item.CategoryId equals category.Id into categoryGroup
                    from category in categoryGroup.DefaultIfEmpty()
                    select new
                    {
                        item.Id,
                        item.Name,
                        item.Description,
                        item.Price,
                        item.ItemType,
                        item.Status,
                        item.IsFeatured,
                        item.CategoryId,
                        CategoryName = category != null ? category.Name : null
                    };

        // Apply filters
        query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
            c => c.Name.ToLower() == input.Filter.ToLower())
        .WhereIf(input.ItemType.HasValue, c => c.ItemType == (ItemType)input.ItemType.Value)
        .WhereIf(input.Status.HasValue, c => c.Status == (Status)input.Status)
        .WhereIf(input.CategoryId.HasValue, c => c.CategoryId == input.CategoryId)
        .WhereIf(input.MaxPrice.HasValue, c => c.Price <= input.MaxPrice.Value)
        .WhereIf(input.IsFeatured.HasValue, c => c.IsFeatured == input.IsFeatured.Value);

        // Count total items after filtering
        var totalCount = await AsyncExecuter.CountAsync(query);

        // Fetch the filtered and paginated items
        var itemsWithCategory = await AsyncExecuter.ToListAsync(
            query.OrderBy(input.Sorting ?? nameof(Item.Name))
                 .PageBy(input.SkipCount, input.MaxResultCount)
        );

        // Map to ItemDto
        var itemDtos = itemsWithCategory.Select(x => new ItemDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            ItemType = x.ItemType.ToString(),
            status = x.Status.ToString(),
            IsFeatured = x.IsFeatured,
            CategoryId = x.CategoryId,
            CategoryName = x.CategoryName
        }).ToList();

        return new PagedResultDto<ItemDto>(
            totalCount,
            itemDtos
        );
    }
    public async Task DeleteAsync(int id)
    {
        Item item = await _repository.GetAsync(id);
        if (item == null)
            throw new Exception("Not found");
        await _repository.DeleteAsync(id);
    }
}