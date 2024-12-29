global using Wajba.Dtos.ItemsDtos;
global using Wajba.Models.Items;

namespace Wajba.ItemServices;

public class ItemAppServices<T> : ApplicationService  
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
}