global using Microsoft.EntityFrameworkCore;
global using Wajba.Dtos.ItemsDtos;
global using Wajba.Enums;
global using Wajba.Models.Items;
global using Wajba.Dtos.ItemsDtos.ItemDependencies;
global using Wajba.Dtos.ItemVariationContract;


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

    public async Task<List<ItemDto>> GetItemsByCategoryAsync(int? categoryId,string? name)
    {
        var items = await _repository.WithDetailsAsync(
            x => x.ItemAddons,
            x => x.ItemExtras,
            x => x.ItemVariations
        );
        if (categoryId != null && categoryId.Value != 0)
            items = (IQueryable<Item>)await items.Where(p => p.CategoryId == categoryId.Value).ToListAsync();
        if (!string.IsNullOrEmpty(name))
            items = (IQueryable<Item>)await items.Where(p => p.Name.ToLower() == name.ToLower()).ToListAsync();
        var result = items.Where(x => x.CategoryId == categoryId)
                          .Select(item => ObjectMapper.Map<Item, ItemDto>(item))
                          .ToList();
        return result;
    }

    public async Task<List<ItemDto>> GetItemsByBranchAsync(int branchId)
    {
        var items = await _repository.WithDetailsAsync(
            x => x.ItemBranches,
            x => x.ItemAddons,
            x => x.ItemExtras,
            x => x.ItemVariations
        );
        var result = items.Where(item => item.ItemBranches.Any(ib => ib.BranchId == branchId))
                          .Select(item => ObjectMapper.Map<Item, ItemDto>(item))
                          .ToList();
        return result;
    }

    public async Task<ItemDto> GetItemWithDetailsAsync(int id)
    {
        var queryable = await _repository.WithDetailsAsync(
         x => x.ItemAddons,
         x => x.ItemExtras,
         x => x.ItemVariations,
         x => x.ItemBranches
     );

        var item = await queryable.FirstOrDefaultAsync(x => x.Id == id);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), id);

        //  map the main item to ItemDto
        ItemDto itemDto = new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Note = item.Note,
            status = item.Status.ToString(),
            IsFeatured = item.IsFeatured,
            imageUrl = item.ImageUrl,
            Price = item.Price,
            TaxValue = item.TaxValue,
            CategoryId = item.CategoryId,
            CategoryName = item.Category?.Name, 
            ItemType = item.ItemType.ToString(),
            IsDeleted = item.IsDeleted,
            Branchesids = item.ItemBranches.Select(p => p.BranchId).ToList() 
        };

        //  map ItemAddons to ItemAddonDto
        itemDto.ItemAddons = item.ItemAddons.Select(addon => new ItemAddonDto
        {
            Id = addon.Id,
            Name = addon.AddonName,
          
            AdditionalPrice = addon.AdditionalPrice
        }).ToList();

        //  map ItemExtras to ItemExtraDto
        itemDto.ItemExtras = item.ItemExtras.Select(extra => new ItemExtraDto
        {
            Id = extra.Id,
            Name = extra.Name,
           
            AdditionalPrice = extra.AdditionalPrice
        }).ToList();

        // map ItemVariations to ItemVariationDto
        itemDto.ItemVariations = item.ItemVariations.Select(variation => new ItemVariationDto
        {
            Id = variation.Id,
            Name = variation.Name,
            Note = variation.Note,
            AdditionalPrice = variation.AdditionalPrice
        }).ToList();

        return itemDto;
       
    }
    public async Task<ItemDto> CreateAsync(CreateItemDto input)
    {
        //if (input.ImageUrl != null)
        //    imageUrl = await _imageService.UploadAsync(input.ImageUrl);
        if (await _repository1.FindAsync(input.CategoryId) == null)
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
        if(itemBranches.Count == 0)
            throw new Exception("Branches are required");
        Item item = new Item()
        {
            Name = input.Name,
            Description = input.Description,
            CategoryId = input.CategoryId,
            IsFeatured = input.IsFeatured,
            TaxValue = input.TaxValue,
            Price = input.Price,
            ItemType = (Enums.ItemType)input.ItemType,
            Note = input.Note,
            Status = (Enums.Status)input.status,
            IsDeleted = false,
        };
        var imagebytes = Convert.FromBase64String(input.model.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        item.ImageUrl = await _imageService.UploadAsync(ms, input.model.FileName);
        item.ItemBranches = itemBranches;
        await _repository.InsertAsync(item, true);
        return ObjectMapper.Map<Item, ItemDto>(item);
    }

    public async Task<ItemWithDependenciesDto> GetItemWithTransformedDetailsAsync(int id)
    {
        // Load item and related entities
        var queryable = await _repository.WithDetailsAsync(
            x => x.ItemAddons,
            x => x.ItemExtras,
            x => x.ItemVariations,  
            x => x.ItemBranches ,
            x => x.Category
        );

        // Explicitly include nested navigation properties
        queryable = queryable.Include(x => x.ItemVariations)
                             .ThenInclude(v => v.ItemAttributes);

        // Fetch the item
        var item = await queryable.FirstOrDefaultAsync(x => x.Id == id);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), id);

        // Map the main item to ItemDto
        var itemDto = new ItemWithDependenciesDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Note = item.Note,
            Status = item.Status.ToString(),
            IsFeatured = item.IsFeatured,
            ImageUrl = item.ImageUrl,
            Price = item.Price,
            TaxValue = (decimal)item.TaxValue,
            CategoryId = item.CategoryId,
            CategoryName = item.Category?.Name,
            ItemType = item.ItemType.ToString(),
            IsDeleted = item.IsDeleted,
            BranchesIds = item.ItemBranches.Select(p => p.BranchId).ToList()
        };

        // Map ItemAddons
        itemDto.ItemAddons = item.ItemAddons.Select(addon => new ItemAddonDTO
        {
            Id = addon.Id,
            Name = addon.AddonName,
            AdditionalPrice = addon.AdditionalPrice,

            ImageUrl = addon.Item?.ImageUrl
        }).ToList();

        // Map ItemExtras
        itemDto.ItemExtras = item.ItemExtras.Select(extra => new ItemExtraDTO
        {
            Id = extra.Id,
            Name = extra.Name,
            AdditionalPrice = extra.AdditionalPrice
        }).ToList();

        // Group and map ItemVariations
        itemDto.Attributes = item.ItemVariations
            .Where(v => v.ItemAttributes != null) // Exclude variations without attributes
            .GroupBy(v => v.ItemAttributes.Name)  // Group by Attribute Name
            .Select(group => new AttributeDto
            {
                AttributeName = group.Key,
                Variations = group.Select(v => new VariationDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    Note = v.Note,
                    AdditionalPrice = v.AdditionalPrice,
                    ItemAttributesId = v.ItemAttributesId
                }).ToList()
            }).ToList();

        return itemDto;
    }


    public async Task<ItemDto> updateimage(int id, Base64ImageModel model)
    {
        Item item = await _repository.FindAsync(id);
        if (item == null)
            throw new Exception("Item not found");
        if (model == null)
            throw new Exception("Invalid data");
        var imagebytes = Convert.FromBase64String(model.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        item.ImageUrl = await _imageService.UploadAsync(ms, model.FileName);
        item.LastModificationTime = DateTime.UtcNow;
        var item1 = await _repository.UpdateAsync(item, true);
        return ObjectMapper.Map<Item, ItemDto>(item1);
    }

    public async Task<ItemDto> UpdateAsync(int id, UpdateItemDTO input)
    {
        Item item = await _repository.FindAsync(id);
        if (item == null)
            throw new Exception("Item not found");
        //if (input.ImageUrl == null)
        //    throw new Exception("Image is required");
        if (await _repository1.FindAsync(input.CategoryId) == null)
            throw new Exception("Category not found");
        List<ItemBranch> itemBranches = new List<ItemBranch>();
        foreach (var branchId in input.BranchIds)
        {
            Branch branch = await _repositoryBranch.FindAsync(branchId);
            if (branch == null)
                throw new Exception("Branch not found");
            itemBranches.Add(new ItemBranch() { BranchId = branchId, Branch = branch });
        }
        if (input.model != null)
        {
            var imagebytes = Convert.FromBase64String(input.model.Base64Content);
            using var ms = new MemoryStream(imagebytes);
            item.ImageUrl = await _imageService.UploadAsync(ms, input.model.FileName);
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
        if (item == null) throw new Exception("Not found");
        return ObjectMapper.Map<Item, ItemDto>(item);
    }
    public async Task<PagedResultDto<ItemDto>> GetListAsync(GetItemInput input)
    {
        IQueryable<Item> queryable = await _repository.GetQueryableAsync();
        var items = _repository.WithDetailsAsync(p => p.Category).Result.ToList();
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
    public async Task<PagedResultDto<ItemDto>> GetByname(string name)
    {
        var queryable = await _repository.WithDetailsAsync(x => x.ItemBranches);
        List<ItemDto> itemDtos1 = new List<ItemDto>();
        var items = queryable.ToList();
        foreach (var item in items)
        {
            if (item.Name.ToLower() == name.ToLower())
                itemDtos1.Add(ObjectMapper.Map<Item, ItemDto>(item));
        }
        return new PagedResultDto<ItemDto>(
        itemDtos1.Count,
          itemDtos1);
    }
    public async Task<PagedResultDto<ItemDto>> GetByBranchId(int branchid)
    {
        var queryable = await _repository.WithDetailsAsync(x => x.ItemBranches);
        List<ItemDto> itemDtos1 = new List<ItemDto>();
        var items = queryable.ToList();
        foreach (var item in items)
        {
            var itemBranches = item.ItemBranches.Any(p => p.BranchId == branchid);
            if (itemBranches)
                itemDtos1.Add(ObjectMapper.Map<Item, ItemDto>(item));
        }
        return new PagedResultDto<ItemDto>(
            itemDtos1.Count,
              itemDtos1
        );
    }
    public async Task DeleteAsync(int id)
    {
        Item item = await _repository.GetAsync(id);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), id);
        await _repository.DeleteAsync(id);
    }
}