global using Wajba.Dtos.ItemsDtos;
global using Wajba.Models.Items;
global using Wajba.Enums;

namespace Wajba.ItemServices;

[RemoteService(false)]
public class ItemAppServices : ApplicationService  
{
    private readonly IRepository<Item, int> _repository;
    private readonly IRepository<Category, int> _repository1;
    private readonly IRepository<Branch,int> _repositoryBranch;
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
        Category category = await _repository1.FindAsync(input.CategoryId);
        if (category == null)
            return null;
        List<ItemBranch> itemBranches = new List<ItemBranch>();
        foreach (var branchId in input.BranchIds)
        {
            Branch branch = await _repositoryBranch.FindAsync(branchId);
            if (branch == null)
                return null;
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

        await _repository.InsertAsync(item);
        return ObjectMapper.Map<Item, ItemDto>(item);
    }
    public async Task<ItemDto> UpdateAsync(int id, CreateItemDto input)
    {
        Item item = await _repository.FindAsync(id);
        if (item == null)
            return null;
        string? imageUrl = null;
        if (input.ImageUrl != null)
            imageUrl = await _imageService.UploadAsync(input.ImageUrl);
        Category category = await _repository1.FindAsync(input.CategoryId);
        if (category == null)
            return null;
        List<ItemBranch> itemBranches = new List<ItemBranch>();
        foreach (var branchId in input.BranchIds)
        {
            Branch branch = await _repositoryBranch.FindAsync(branchId);
            if (branch == null)
                return null;
            itemBranches.Add(new ItemBranch() { BranchId = branchId, Branch = branch });
        }
        item.Name = input.Name;
        item.Description = input.Description;
        item.ImageUrl = imageUrl;
        item.CategoryId = input.CategoryId;
        item.IsFeatured = input.IsFeatured;
        item.TaxValue = input.TaxValue;
        item.Price = input.Price;
        item.ItemType = (Enums.ItemType)input.ItemType;
        item.Note = input.Note;
        item.Status = (Enums.Status)input.status;
        item.ItemBranches = itemBranches;
        item.LastModificationTime = DateTime.UtcNow;
        await _repository.UpdateAsync(item);
        return ObjectMapper.Map<Item, ItemDto>(item);
    }
    public async Task<ItemDto> GetByIdAsync(int id)
    {
        Item item = await _repository.GetAsync(id);
        return ObjectMapper.Map<Item, ItemDto>(item);
    }
    public async Task<PagedResultDto<ItemDto>> GetListAsync(GetItemInput input)
    {
        IQueryable<Item> queryable = await _repository.GetQueryableAsync();
        queryable = queryable.WhereIf(
            !string.IsNullOrWhiteSpace(input.Filter),
            c => c.Name.Contains(input.Filter) || c.Description.Contains(input.Filter)
        ).WhereIf(input.ItemType.HasValue, c => c.ItemType == (ItemType)input.ItemType.Value)
        .WhereIf(global::Wajba.Enums.Status.Active == (Status)input.Status, c => c.Status == global::Wajba.Enums.Status.Active);
        int totalCount = await AsyncExecuter.CountAsync(queryable);
        List<Item> items = await AsyncExecuter.ToListAsync(queryable
            .OrderBy(input.Sorting ?? nameof(Item.Name))
            .PageBy(input.SkipCount, input.MaxResultCount));
        return new PagedResultDto<ItemDto>(
            totalCount,
            ObjectMapper.Map<List<Item>, List<ItemDto>>(items)
        );
    }
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

}