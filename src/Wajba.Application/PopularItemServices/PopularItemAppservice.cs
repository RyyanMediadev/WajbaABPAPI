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
        Item item = await items.FirstOrDefaultAsync(p => p.Id == input.ItemId);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), input.ItemId);
        Category category = _categoryrepo.WithDetailsAsync(p => p.Items).Result.FirstOrDefault(p => p.Id == item.CategoryId);
        if (category == null)
            throw new EntityNotFoundException(typeof(Category), item.CategoryId);
        PopularItem popularitem = new PopularItem()
        {
            ItemId = input.ItemId,
            Name = item.Name,
            PrePrice = input.preprice,
            CurrentPrice = input.currentprice,
            Description = input.Description,
            CategoryName = category.Name,
        };
        popularitem.ImageUrl = null;
        if (input.Model != null)
        {
            var url = Convert.FromBase64String(input.Model.Base64Content);
            using var ms = new MemoryStream(url);
            popularitem.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        }
        popularitem = await _popularitemrepo.InsertAsync(popularitem, autoSave: true);
        return ObjectMapper.Map<PopularItem, Popularitemdto>(popularitem);
    }
    public async Task<PagedResultDto<Popularitemdto>> GetPopularItems(GetPopulariteminput input)
    {
        var popularitems = await _popularitemrepo.GetQueryableAsync();
        int count = popularitems.Count();
        popularitems = popularitems.OrderBy(input.Sorting?? nameof(PopularItem.Name)).PageBy(input.SkipCount, input.MaxResultCount);
        List<PopularItem> popularitemslist = await popularitems.ToListAsync();
        List<Popularitemdto> populartitemsdto = ObjectMapper.Map<List<PopularItem>, List<Popularitemdto>>(popularitemslist);
        return new PagedResultDto<Popularitemdto>(count, populartitemsdto);
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
      popularitem.ItemId = input.ItemId;
        popularitem.Name = item.Name;
        popularitem.PrePrice = input.preprice;
        popularitem.CurrentPrice = input.currentprice;
        popularitem.Description = input.Description;
        popularitem.CategoryName = category.Name;
        popularitem.LastModificationTime = DateTime.UtcNow;
        if (input.Model != null)
        {
            var url = Convert.FromBase64String(input.Model.Base64Content);
            using var ms = new MemoryStream(url);
            popularitem.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        }
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