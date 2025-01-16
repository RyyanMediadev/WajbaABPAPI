global using Microsoft.AspNetCore.SignalR;
global using Wajba.Dtos.OffersContract;
global using Wajba.Hubs;
global using Wajba.Models.OfferDomain;

namespace Wajba.OfferService
{
    [RemoteService(false)]
    public class OfferAppService : ApplicationService
    {
        private readonly IRepository<Offer, int> _offerRepository;
        private readonly IImageService _fileUploadService;
        private readonly IRepository<Branch, int> _branchrepo;
        private readonly IRepository<Category, int> _categoryrepo;
        private readonly IRepository<Item, int> _itemrepo;
        private readonly IHubContext<OfferHub> _hubContext;

        public OfferAppService(
            IRepository<Offer, int> offerRepository,
            IImageService imageService,
            IRepository<Branch, int> branchrepo,
            IRepository<Category, int> categoryrepo,
            IRepository<Item, int> itemrepo,
            IHubContext<OfferHub> hubContext)
        {
            _offerRepository = offerRepository;
            _fileUploadService = imageService;
            _branchrepo = branchrepo;
            _categoryrepo = categoryrepo;
            _itemrepo = itemrepo;
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
            if (input.CategoryIds == null || !input.CategoryIds.Any()
                && input.ItemIds == null || !input.ItemIds.Any())
                throw new Exception("At least one of ItemIds or CategoryIds must be provided.");
            offer.OfferCategories = new List<OfferCategory>();
            offer.OfferItems = new List<OfferItem>();
            foreach (var i in input.ItemIds)
            {
                Item item = await _itemrepo.FindAsync(i);
                if (item == null)
                    throw new Exception("item not found");
                offer.OfferItems.Add(new OfferItem() { Item = item, ItemId = i });
            }
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
            var offerdto = ObjectMapper.Map<Offer, OfferDto>(createdOffer);
            await _hubContext.Clients.All.SendAsync("ReceiveOffer", offerdto);
            return offerdto;
        }

        public async Task<OfferDto> UpdateAsync(int id, CreateUpdateOfferDto input)
        {
            var offer = await _offerRepository.GetAsync(id);
            if (offer == null)
                throw new Exception("Not found");
            if (input.Model == null)
                throw new Exception("Image is required");
            if (await _branchrepo.GetAsync(input.BranchId) == null)
                throw new Exception("Branch not found");
            ObjectMapper.Map(input, offer);
            var imagebytes = Convert.FromBase64String(input.Model.Base64Content);
            using var ms = new MemoryStream(imagebytes);
            offer.LastModificationTime = DateTime.UtcNow;
            offer.ImageUrl = await _fileUploadService.UploadAsync(ms, input.Model.FileName);
            var updatedOffer = await _offerRepository.UpdateAsync(offer,true);
            return ObjectMapper.Map<Offer, OfferDto>(updatedOffer);
        }

        public async Task<OfferDto> GetAsync(int id)
        {
            var offer = await _offerRepository.GetAsync(id);
            if (offer == null)
                throw new Exception("Not found");
            return ObjectMapper.Map<Offer, OfferDto>(offer);
        }

        public async Task<PagedResultDto<OfferDto>> GetListAsync(GetOfferInput input)
        {
            var offers = await _offerRepository.WithDetailsAsync(p => p.OfferCategories,
                x => x.OfferItems);
            offers = offers.WhereIf(string.IsNullOrEmpty(input.name), p => p.Name.ToLower() == input.name.ToLower())
                .WhereIf(input.status.HasValue, p => p.status == (Status)input.status.Value)
                .WhereIf(input.startDate.HasValue, p => p.StartDate.Value == input.startDate.Value)
                .WhereIf(input.endDate.HasValue, p => p.EndDate.Value == input.endDate.Value);
            var totalCount =await offers.CountAsync();
            offers = (IQueryable<Offer>)offers.PageResult(input.SkipCount, input.MaxResultCount);
            offers = offers.OrderBy(input.Sorting);
            List<Offer> offers1 = await offers.ToListAsync();

            List<OfferDto> offersdto = await offers.Select(o => new OfferDto()
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                Image = o.ImageUrl,
                Status = (int)o.status,
                DiscountType = (int)o.discountType,
                StartDate = o.StartDate,
                EndDate = o.EndDate,
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
            }).ToListAsync();
            //var offers = await _offerRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
            //var totalCount = await _offerRepository.GetCountAsync();
            return new PagedResultDto<OfferDto>(
                totalCount,
               offersdto
            );
        }

        public async Task DeleteAsync(int id)
        {
            var offer = await _offerRepository.GetAsync(id);
            if (offer == null)
                throw new Exception("Not found");
            await _offerRepository.DeleteAsync(id);
        }
    }
}