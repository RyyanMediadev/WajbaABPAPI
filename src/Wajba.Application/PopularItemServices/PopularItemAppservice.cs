global using Wajba.Models.PopularItemsDomain;
using Volo.Abp.Domain.Entities;

namespace Wajba.PopularItemServices;
[RemoteService(false)]
public class PopularItemAppservice:ApplicationService
{
    private readonly IRepository<PopularItem, int> _popularitemrepo;
    private readonly IRepository<Item, int> _itemrepo;
    private readonly IRepository<Branch, int> _branchrepo;
    private readonly IRepository<Category, int> _categoryrepo;
    private readonly IImageService _imageService;

    public PopularItemAppservice(IRepository<PopularItem, int> popularitemrepo,
        IRepository<Item, int> itemrepo,
        IRepository<Branch, int> branchrepo,
        IRepository<Category,int> categoryrepo,
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
        Item item = await _itemrepo.GetAsync(input.Id);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), input.Id);
        Category category = await _categoryrepo.GetAsync(item.CategoryId);
        if (input.ImgFile == null)
            throw new Exception("Image is required");
        Branch branch = item.ItemBranches.FirstOrDefault(p=>p.BranchId==input.BranchId).Branch;
        if (branch == null)
            throw new EntityNotFoundException(typeof(Branch), item.ItemBranches.FirstOrDefault().BranchId);
        PopularItem popularitem = new PopularItem()
        {
            ItemId = input.Id,
            Name = input.Name,
            PrePrice = input.preprice,
            CurrentPrice = input.currentprice,
            Description = input.Description,
            Status = (Status)input.Status,
            CategoryName = category.Name,
            BranchId = branch.Id,
        };
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
        {
            throw new EntityNotFoundException(typeof(PopularItem), id);
        }
        return ObjectMapper.Map<PopularItem, Popularitemdto>(popularitem);
    }
    public async Task<Popularitemdto> UpdateAsync(int id, UpdatePopularitem input)
    {
        var popularItem = await _popularitemrepo.GetAsync(id);
        if (popularItem == null)
        {
            throw new EntityNotFoundException(typeof(PopularItem), id);
        }

        Item item = await _itemrepo.GetAsync(input.Id);
        if (item == null)
            throw new EntityNotFoundException(typeof(Item), input.Id);
        Category category = await _categoryrepo.GetAsync(item.CategoryId);

        Branch branch = item.ItemBranches.FirstOrDefault(p => p.BranchId == input.BranchId)?.Branch;
        if (branch == null)
            throw new EntityNotFoundException(typeof(Branch), input.BranchId);

        // If a new image is provided, upload it
        if (input.ImgFile != null)
        {
            popularItem.ImageUrl = await _imageService.UploadAsync(input.ImgFile);
        }

        popularItem.Name = input.Name;
        popularItem.PrePrice = input.PrePrice;
        popularItem.CurrentPrice = input.CurrentPrice;
        popularItem.Description = input.Description;
        popularItem.Status = (Status)input.Status;
        popularItem.CategoryName = category.Name;
        popularItem.BranchId = branch.Id;

        await _popularitemrepo.UpdateAsync(popularItem);
        return ObjectMapper.Map<PopularItem, Popularitemdto>(popularItem);
    }

    public async Task DeleteAsync(int id)
    {
        var popularItem = await _popularitemrepo.GetAsync(id);
        if (popularItem == null)
        {
            throw new EntityNotFoundException(typeof(PopularItem), id);
        }

        await _popularitemrepo.DeleteAsync(popularItem);
    }
}
