global using Microsoft.AspNetCore.SignalR;
global using Wajba.Dtos.OffersContract;
global using Wajba.Hubs;
global using Wajba.Models.OfferDomain;
using Microsoft.AspNetCore.Mvc;

namespace Wajba.OfferService;

[RemoteService(false)]
public class OfferAppService : ApplicationService
{
    private readonly IRepository<Offer, int> _offerRepository;
    private readonly IImageService _fileUploadService;
    private readonly IRepository<Branch, int> _branchrepo;
    private readonly IRepository<Category, int> _categoryrepo;
    private readonly IRepository<Item, int> _itemrepo;
    private readonly IRepository<OfferCategory, int> _offercategory;
    private readonly IRepository<OfferItem, int> _offeritems;
    private readonly IHubContext<OfferHub> _hubContext;

    public OfferAppService(
        IRepository<Offer, int> offerRepository,
        IImageService imageService,
        IRepository<Branch, int> branchrepo,
        IRepository<Category, int> categoryrepo,
        IRepository<Item, int> itemrepo,
        IRepository<OfferCategory, int> offercategory,
        IRepository<OfferItem, int> offeritems,
        IHubContext<OfferHub> hubContext)
    {
        _offerRepository = offerRepository;
        _fileUploadService = imageService;
        _branchrepo = branchrepo;
        _categoryrepo = categoryrepo;
        _itemrepo = itemrepo;
        _offercategory = offercategory;
        _offeritems = offeritems;
        _hubContext = hubContext;
    }

    public async Task<OfferDto> CreateAsync(CreateUpdateOfferDto input)
    {
        Branch branch = await _branchrepo.GetAsync(input.BranchId);
        if (branch == null)
            throw new Exception("Branch not found");
        if (input.Model == null)
            throw new Exception("Image is required");
        Offer offer = new Offer()
        {
            Name = input.Name,
            Description = input.Description,
            DiscountPercentage = input.DiscountPercentage,
            StartDate = input.StartDate,
            EndDate = input.EndDate,
            BranchId = input.BranchId,
        };
        offer.discountType = (Enums.DiscountType)input.DiscountType;
        offer.status = (Enums.Status)input.Status;
        //if (input.CategoryIds == null || !input.CategoryIds.Any()
        //    && input.ItemIds == null || !input.ItemIds.Any())
        //    throw new Exception("At least one of ItemIds or CategoryIds must be provided.");
        offer.OfferCategories = new List<OfferCategory>();
        offer.OfferItems = new List<OfferItem>();
        if (input.ItemIds.Count == 0 && input.CategoryIds.Count() == 0)
            throw new Exception("Invalid data");
        if (input.ItemIds.Count > 0)
            foreach (var i in input.ItemIds)
            {
                Item item = await _itemrepo.FindAsync(i);
                if (item == null)
                    throw new Exception("item not found");
                offer.OfferItems.Add(new OfferItem() { Item = item, ItemId = i });
            }
        else
            foreach (var cat in input.CategoryIds)
            {
                Category category = await _categoryrepo.FindAsync(cat);
                if (category == null)
                    throw new Exception("category not found");
                offer.OfferCategories.Add(new OfferCategory() { Category = category });
            }
        var imagebytes = Convert.FromBase64String(input.Model.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        offer.ImageUrl = await _fileUploadService.UploadAsync(ms, input.Model.FileName);
        var createdOffer = await _offerRepository.InsertAsync(offer, true);
        var offerdto = tooffedto(createdOffer);
        await _hubContext.Clients.All.SendAsync("ReceiveOffer", offerdto);
        return offerdto;
    }


    public async Task<OfferDto> UpdateAsync(int id, UpdateOfferdto input)
    {
        var offers = await _offerRepository.WithDetailsAsync(p => p.OfferCategories,
           x => x.OfferItems).Result.Include(p => p.OfferCategories).ThenInclude(p => p.Category)
           .Include(p => p.OfferItems).ThenInclude(p => p.Item).ToListAsync();
        var offer = offers.FirstOrDefault(p => p.Id == input.Id);
        if (offer == null)
            throw new Exception("Not found");
        if (input.Model == null)
            throw new Exception("Image is required");
        if (await _branchrepo.GetAsync(input.BranchId) == null)
            throw new Exception("Branch not found");
        offer.Description = input.Description;
        offer.BranchId = input.BranchId;
        offer.status = (Status)input.Status;
        offer.StartDate = input.StartDate;
        offer.Name = input.Name;
        offer.EndDate = input.EndDate;
        offer.DiscountPercentage = input.DiscountPercentage;
        offer.discountType = (DiscountType)input.DiscountType;
        if (input.ItemIds.Count == 0 && input.CategoryIds.Count() == 0)
            throw new Exception("Invalid data");
        offer.OfferCategories.Clear();
        offer.OfferItems.Clear();
        //offer.OfferItems.RemoveAll(oi => !input.ItemIds.Contains(oi.ItemId));
        //offer.OfferCategories.RemoveAll(oi => !input.CategoryIds.Contains(oi.CategoryId));
        offer.OfferCategories = new List<OfferCategory>();
        offer.OfferItems = new List<OfferItem>();
        //offer = await _offerRepository.UpdateAsync(offer, true);
        if (input.ItemIds.Count > 0)
        {
            foreach (var i in input.ItemIds)
            {
                var py = await _offeritems.FirstOrDefaultAsync(p => p.ItemId == i && p.OfferId == offer.Id);
                if (py == null)
                {
                    Item item = await _itemrepo.FindAsync(i);
                    if (item == null)
                        throw new Exception("item not found");
                    py = new OfferItem() { Item = item, ItemId = i, OfferId = offer.Id };

                }
                offer.OfferItems.Add(py);
            }
        }
        else
        {
            foreach (var cat in input.CategoryIds)
            {
                var py = await _offercategory.FirstOrDefaultAsync(p => p.CategoryId == cat && p.OfferId == offer.Id);
                if (py == null)
                {
                    Category category = await _categoryrepo.FindAsync(cat);
                    if (category == null)
                        throw new Exception("category not found");
                    py = new OfferCategory() { Category = category, OfferId = offer.Id };
                    offer.OfferCategories.Add(py);
                }
            }
        }
        var imagebytes = Convert.FromBase64String(input.Model.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        offer.LastModificationTime = DateTime.UtcNow;
        offer.ImageUrl = await _fileUploadService.UploadAsync(ms, input.Model.FileName);
        var o = await _offerRepository.UpdateAsync(offer, true);
        return tooffedto(o);
    }
    public async Task<OfferDto> updateimage(int id, Base64ImageModel input)
    {
        var offer = await _offerRepository.FindAsync(id);
        if (offer == null)
            throw new EntityNotFoundException("not found");
        if (input == null)
            throw new Exception("invalid data");
        var imagebytes = Convert.FromBase64String(input.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        offer.LastModificationTime = DateTime.UtcNow;
        offer.ImageUrl = await _fileUploadService.UploadAsync(ms, input.FileName);
        var o = await _offerRepository.UpdateAsync(offer, true);
        return new OfferDto();
    }
    public async Task<OfferDto> GetAsync(int id)
    {
        var offers = await _offerRepository.WithDetailsAsync(p => p.OfferCategories,
         x => x.OfferItems).Result.Include(p => p.OfferCategories).ThenInclude(p => p.Category)
         .Include(p => p.OfferItems).ThenInclude(p => p.Item).ToListAsync();
        var o = offers.FirstOrDefault(p => p.Id == id);
        if (o == null)
            throw new Exception("Not found");
        int l = o.OfferCategories.Count;
        l = o.OfferItems.Count;
        //var pp = o.OfferCategories.FirstOrDefault().Category.Name;
        return tooffedto(o);
    }

    public async Task<PagedResultDto<OfferDto>> GetListAsync(GetOfferInput input)
    {
        var offers = await _offerRepository.WithDetailsAsync(p => p.OfferCategories,
            x => x.OfferItems).Result.Include(p => p.OfferCategories).ThenInclude(p => p.Category)
            .Include(p => p.OfferItems).ThenInclude(p => p.Item).ToListAsync();
        offers = (List<Offer>)offers.WhereIf(!string.IsNullOrEmpty(input.name), p => p.Name.ToLower() == input.name.ToLower())
            .WhereIf(input.status.HasValue, p => p.status == (Status)input.status.Value)
            .WhereIf(input.startDate.HasValue, p => p.StartDate.Value == input.startDate.Value)
            .WhereIf(input.endDate.HasValue, p => p.EndDate.Value == input.endDate.Value).ToList();
        var totalCount = offers.Count();
        //offers = (IQueryable<Offer>)offers.PageResult(input.SkipCount, input.MaxResultCount);
        //offers = offers.OrderBy(input.Sorting);
        //List<Offer> offers1 = await offers.ToListAsync();
        List < OfferDto > offersdto = offers.Select(o => tooffedto(o)).ToList();
        //var offers = await _offerRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        //var totalCount = await _offerRepository.GetCountAsync();
        return new PagedResultDto<OfferDto>(
            totalCount,
           offersdto
        );
    }

    public async Task<OfferDto> deleteitems(int id, int itemid)
    {
        var offers = await _offerRepository.WithDetailsAsync(p => p.OfferCategories,
      x => x.OfferItems).Result.Include(p => p.OfferCategories).ThenInclude(p => p.Category)
      .Include(p => p.OfferItems).ThenInclude(p => p.Item).ToListAsync();
        var o = offers.FirstOrDefault(p => p.Id == id);
        if (o == null)
            throw new Exception("Not found");
            OfferItem offerItem = o.OfferItems.FirstOrDefault(p => p.ItemId == itemid);
            if (offerItem == null)
                throw new Exception("invalid data");
            o.OfferItems.Remove(offerItem);
        o.LastModificationTime = DateTime.UtcNow;
        o = await _offerRepository.UpdateAsync(o, true);
        return tooffedto(o);
    }
    public async Task<OfferDto> deletecategory(int id, int categoryid)
    {
        var offers = await _offerRepository.WithDetailsAsync(p => p.OfferCategories,
      x => x.OfferItems).Result.Include(p => p.OfferCategories).ThenInclude(p => p.Category)
      .Include(p => p.OfferItems).ThenInclude(p => p.Item).ToListAsync();
        var o = offers.FirstOrDefault(p => p.Id == id);
        if (o == null)
            throw new Exception("Not found");
        OfferCategory offerCategory = o.OfferCategories.FirstOrDefault(p => p.CategoryId == categoryid);
        if (offerCategory == null)
            throw new Exception("invalid data");
        o.OfferCategories.Remove(offerCategory);
        o.LastModificationTime = DateTime.UtcNow;
        o = await _offerRepository.UpdateAsync(o, true);
        return tooffedto(o);
    }
    public async Task DeleteAsync(int id)
    {
        var offer = await _offerRepository.GetAsync(id);
        if (offer == null)
            throw new Exception("Not found");
        await _offerRepository.DeleteAsync(id, true);
    }
    public OfferDto tooffedto(Offer o)
    {
        OfferDto offerDto = new OfferDto()
        {
            Id = o.Id,
            Name = o.Name,
            Description = o.Description,
            Image = o.ImageUrl,
            Status = (int)o.status,
            DiscountType = (int)o.discountType,
            StartDate = o.StartDate,
            EndDate = o.EndDate,
            BranchId = o.BranchId,
            DiscountPercentage = o.DiscountPercentage,
            categoryDtos = o.OfferCategories.Select(c => new CategoryDto()
            {
                Description = c.Category.Description,
                ImageUrl = c.Category.ImageUrl,
                Id = c.Category.Id,
                status = (int)c.Category.Status,
                name = c.Category.Name
            }).ToList(),
            itemDtos = o.OfferItems.Select(m => new ItemDto()
            {
                Name = m.Item.Name,
                status = m.Item.Status.ToString(),
                imageUrl = m.Item.ImageUrl,
                CategoryId = m.Item.CategoryId,
                IsFeatured = m.Item.IsFeatured,
                Description = m.Item.Description,
                Id = m.Item.Id,
                ItemType = m.Item.ItemType.ToString(),
                Price = m.Item.Price,
                TaxValue = m.Item.TaxValue,
                Note = m.Item.Note,
            }).ToList()
        };
        return offerDto;
    }
}