global using Wajba.Models.PopularItemsDomain;
using System.Data.Entity;

namespace Wajba.PopularItemServices;

[RemoteService(false)]
public class PopularItemAppservice : ApplicationService
{
    private readonly IRepository<PopularItem, int> _popularitemrepo;
    private readonly IRepository<Item, int> _itemrepo;
    private readonly IRepository<Branch, int> _branchrepo;
    private readonly IRepository<Category, int> _categoryrepo;
    private readonly IRepository<PopulartItemBranches, int> _popularitemsbranches;
    private readonly IImageService _imageService;

    public PopularItemAppservice(IRepository<PopularItem, int> popularitemrepo,
        IRepository<Item, int> itemrepo,
        IRepository<Branch, int> branchrepo,
        IRepository<Category, int> categoryrepo,
        IRepository<PopulartItemBranches, int> popularitemsbranches,
        IImageService imageService)
    {
        _popularitemrepo = popularitemrepo;
        _itemrepo = itemrepo;
        _branchrepo = branchrepo;
        _categoryrepo = categoryrepo;
        _popularitemsbranches = popularitemsbranches;
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
            Status = item.Status,
        };
        foreach (var i in item.ItemBranches)
            popularitem.PopulartItemBranches.Add(new PopulartItemBranches() { BranchId = i.BranchId, Branch = i.Branch });
        popularitem.ImageUrl = null;
        if (input.Model != null)
        {
            var url = Convert.FromBase64String(input.Model.Base64Content);
            using var ms = new MemoryStream(url);
            popularitem.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        }
        popularitem = await _popularitemrepo.InsertAsync(popularitem, autoSave: true);
        return topopularitemdto(popularitem);
    }
    public async Task<PagedResultDto<Popularitemdto>> Getbyname(string name)
    {
        var populatitems = await _popularitemrepo.WithDetailsAsync(p => p.PopulartItemBranches);
        populatitems = populatitems.Where(p => p.Name.ToLower() == name.ToLower());
        IList<Popularitemdto> popularitemdtos = new List<Popularitemdto>();
        var  op =await populatitems.ToListAsync();

        foreach (var i in await populatitems.ToListAsync())
            popularitemdtos.Add(topopularitemdto(i));
        return new PagedResultDto<Popularitemdto>()
        {
            Items = (IReadOnlyList<Popularitemdto>)popularitemdtos,
            TotalCount = popularitemdtos.Count
        };
    }
    public async Task<PagedResultDto<Popularitemdto>> GetPopularItems(GetPopulariteminput input)
    {
        var popularitems = await _popularitemrepo.WithDetailsAsync(p => p.PopulartItemBranches);
        if (!string.IsNullOrEmpty(input.Name))
            popularitems = popularitems.Where(p => p.Name.ToLower() == input.Name.ToLower());
        if (input.status.HasValue)
            popularitems = popularitems.Where(p => p.Status ==(Status) input.status);
        popularitems = popularitems.OrderBy(input.Sorting ?? nameof(PopularItem.Name)).PageBy(input.SkipCount, input.MaxResultCount);
        List<PopularItem> popularitemslist = await popularitems.ToListAsync();
        int count = popularitemslist.Count();
        List<Popularitemdto> populartitemsdto = new List<Popularitemdto>();
        foreach (var i in popularitemslist)
            populartitemsdto.Add(topopularitemdto(i));
        return new PagedResultDto<Popularitemdto>(count, populartitemsdto);
    }
    public async Task<Popularitemdto> GetPopularItemById(int id)
    {
        var popularitem = await _popularitemrepo.GetAsync(id);
        if (popularitem == null)
            throw new EntityNotFoundException(typeof(PopularItem), id);
        return topopularitemdto(popularitem);
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
        //if (input.ImgFile == null)
        //    throw new Exception("Image is required");
        foreach (var i in await _popularitemsbranches.ToListAsync())
        {
            if (i.PopularItemId == popularitem.Id)
                await _popularitemsbranches.HardDeleteAsync(i, true);
        }
        popularitem.PopulartItemBranches = new List<PopulartItemBranches>();
        popularitem.ItemId = input.ItemId;
        popularitem.Name = item.Name;
        popularitem.PrePrice = input.preprice;
        popularitem.CurrentPrice = input.currentprice;
        popularitem.Description = input.Description;
        popularitem.CategoryName = category.Name;
        foreach (var i in item.ItemBranches)
            popularitem.PopulartItemBranches.Add(new PopulartItemBranches() { BranchId = i.BranchId, Branch = i.Branch });
        popularitem.LastModificationTime = DateTime.UtcNow;
        if (input.Model != null)
        {
            var url = Convert.FromBase64String(input.Model.Base64Content);
            using var ms = new MemoryStream(url);
            popularitem.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        }
        popularitem = await _popularitemrepo.UpdateAsync(popularitem, autoSave: true);
        return topopularitemdto(popularitem);
    }
    public async Task<Popularitemdto> Updateimage(int id, Base64ImageModel model)
    {
        PopularItem popularItem = await _popularitemrepo.GetAsync(id);
        if (popularItem == null)
            throw new Exception("Not found");
        if (model == null)
            throw new Exception("invalid data");
        var url = Convert.FromBase64String(model.Base64Content);
        using var ms = new MemoryStream(url);
        popularItem.ImageUrl = await _imageService.UploadAsync(ms, model.FileName);
        popularItem.LastModificationTime = DateTime.UtcNow;
        popularItem = await _popularitemrepo.UpdateAsync(popularItem, true);
        return topopularitemdto(popularItem);
    }
    public async Task DeleteAsync(int id)
    {
        var popularitem = await _popularitemrepo.GetAsync(id);
        if (popularitem == null)
            throw new EntityNotFoundException(typeof(PopularItem), id);
        await _popularitemrepo.HardDeleteAsync(popularitem, autoSave: true);
    }
    private static Popularitemdto topopularitemdto(PopularItem popularItem)
    {
        return new Popularitemdto()
        {
            CategoryName = popularItem.CategoryName,
            Description = popularItem.Description,
            Id = popularItem.Id,
            ImageUrl = popularItem.ImageUrl,
            Status = (int)popularItem.Status,
            CurrentPrice = popularItem.CurrentPrice,
            ItemId = popularItem.ItemId,
            Name = popularItem.Name,
            PrePrice = popularItem.PrePrice,
            BranchId = popularItem.PopulartItemBranches.Select(p => p.BranchId).ToList()
        };
    }

    public async Task<PopularItem> GetpopItemByitemId(int id)
    {
        throw new NotImplementedException();
    }
}