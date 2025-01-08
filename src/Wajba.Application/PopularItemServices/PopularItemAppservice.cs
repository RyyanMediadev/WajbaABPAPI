global using Wajba.Models.PopularItemsDomain;

namespace Wajba.PopularItemServices;

[RemoteService(false)]
public class PopularItemAppservice : ApplicationService
{
    private readonly IRepository<PopularItem, int> _popularitemrepo;
    private readonly IRepository<Item, int> _itemrepo;
    private readonly IRepository<Branch, int> _branchrepo;
    private readonly IRepository<Category, int> _categoryrepo;
    private readonly IImageService _imageService;

    public PopularItemAppservice(IRepository<PopularItem, int> popularitemrepo,
        IRepository<Item, int> itemrepo,
        IRepository<Branch, int> branchrepo,
        IRepository<Category, int> categoryrepo,
        IImageService imageService)
    {
        _popularitemrepo = popularitemrepo;
        _itemrepo = itemrepo;
        _branchrepo = branchrepo;
        _categoryrepo = categoryrepo;
        _imageService = imageService;
    }

    public async Task<Popularitemdto> CreateAsync(CreatePopularitem input)
    {
        var items = await _itemrepo.WithDetailsAsync(p => p.ItemBranches);
        Item item = await items.FirstOrDefaultAsync(p => p.Id == input.Id);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), input.Id);
        Category category = _categoryrepo.WithDetailsAsync(p => p.Items).Result.FirstOrDefault(p => p.Id == item.CategoryId);
        if (category == null)
            throw new EntityNotFoundException(typeof(Category), item.CategoryId);
        bool isfound = item.ItemBranches.Any(p => p.BranchId == input.BranchId);
        if (!isfound)
            throw new EntityNotFoundException(typeof(Branch), input.BranchId);
        PopularItem popularitem = new PopularItem()
        {
            ItemId = input.Id,
            Name = input.Name,
            PrePrice = input.preprice,
            CurrentPrice = input.currentprice,
            Description = input.Description,
            Status = (Status)input.Status,
            CategoryName = category.Name,
            BranchId = input.BranchId
        };
        popularitem.ImageUrl = null;
        if (input.ImgFile != null)
            popularitem.ImageUrl = await _imageService.UploadAsync(input.ImgFile);
        popularitem = await _popularitemrepo.InsertAsync(popularitem, autoSave: true);
        return ObjectMapper.Map<PopularItem, Popularitemdto>(popularitem);
    }
    public async Task<List<Popularitemdto>> GetPopularItems(GetPopulariteminput input)
    {
        var popularitems = await _popularitemrepo.GetListAsync();
        return ObjectMapper.Map<List<PopularItem>, List<Popularitemdto>>(popularitems);
    }
    public async Task<Popularitemdto> GetPopularItemById(int id)
    {
        var popularitem = await _popularitemrepo.GetAsync(id);
        if (popularitem == null)
            throw new EntityNotFoundException(typeof(PopularItem), id);
        return ObjectMapper.Map<PopularItem, Popularitemdto>(popularitem);
    }
    public async Task<Popularitemdto> UpdateAsync(int id, UpdatePopularItemdto input)
    {
        var popularitem = await _popularitemrepo.GetAsync(id);
        if (popularitem == null)
            throw new EntityNotFoundException(typeof(PopularItem), id);
        var items = await _itemrepo.WithDetailsAsync(p => p.ItemBranches);
        Item item = await items.FirstOrDefaultAsync(p => p.Id == input.ItemId);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), input.Id);
        Category category = await _categoryrepo.GetAsync(item.CategoryId);
        if (category == null)
            throw new EntityNotFoundException(typeof(Category), item.CategoryId);
        if (popularitem.ItemId != input.ItemId)
            throw new EntityNotFoundException(typeof(Item), input.ItemId);
        //if (input.ImgFile == null)
        //    throw new Exception("Image is required");
        if (item.ItemBranches.Any(p => p.BranchId == input.BranchId))
            throw new EntityNotFoundException(typeof(Branch), item.ItemBranches.FirstOrDefault().BranchId);
        popularitem.ItemId = input.ItemId;
        popularitem.Name = input.Name;
        popularitem.PrePrice = input.preprice;
        popularitem.CurrentPrice = input.currentprice;
        popularitem.Description = input.Description;
        popularitem.Status = (Status)input.Status;
        popularitem.CategoryName = category.Name;
        popularitem.LastModificationTime = DateTime.UtcNow;
        popularitem.BranchId = input.BranchId;
        if (input.ImgFile != null)
            popularitem.ImageUrl = await _imageService.UploadAsync(input.ImgFile);
        popularitem = await _popularitemrepo.UpdateAsync(popularitem, autoSave: true);
        return ObjectMapper.Map<PopularItem, Popularitemdto>(popularitem);
    }
    public async Task DeleteAsync(int id)
    {
        var popularitem = await _popularitemrepo.GetAsync(id);
        if (popularitem == null)
            throw new EntityNotFoundException(typeof(PopularItem), id);
        await _popularitemrepo.DeleteAsync(id, autoSave: true);
    }
}