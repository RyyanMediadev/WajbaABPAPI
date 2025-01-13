global using Wajba.Models.OfferDomain;
global using Wajba.OffersContract;
using Microsoft.AspNet.SignalR;
using Wajba.Hubs;

namespace Wajba.OfferService
{
    [RemoteService(false)]
    public class OfferAppService : ApplicationService
    {
        private readonly IRepository<Offer, int> _offerRepository;
        private readonly IImageService _fileUploadService;
        private readonly IRepository<Branch, int> _branchrepo;
        private readonly IHubContext<OfferHub> _hubContext;

        public OfferAppService(IRepository<Offer, int> offerRepository,
            IImageService imageService,
            IRepository<Branch, int> branchrepo,
            IHubContext<OfferHub> hubContext)
        {
            _offerRepository = offerRepository;
            _fileUploadService = imageService;
            _branchrepo = branchrepo;
            _hubContext = hubContext;
        }

        public async Task<OfferDto> CreateAsync(CreateUpdateOfferDto input)
        {
            Branch branch = await _branchrepo.GetAsync(input.BranchId);
            if (branch == null)
                throw new Exception("Branch not found");
            if (input.Image == null)
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
            offer.ImageUrl = await _fileUploadService.UploadAsync(input.Image);
            var createdOffer = await _offerRepository.InsertAsync(offer, true);
            var offerdto= ObjectMapper.Map<Offer, OfferDto>(createdOffer);
           // await _hubContext.Clients.All.SendAsync("ReceiveOffer", offerdto);
            return offerdto;
        }

        public async Task<OfferDto> UpdateAsync(int id, CreateUpdateOfferDto input)
        {
            var offer = await _offerRepository.GetAsync(id);
            if (offer == null)
                throw new Exception("Not found");
            if (input.Image == null)
                throw new Exception("Image is required");
            if (await _branchrepo.GetAsync(input.BranchId) == null)
                throw new Exception("Branch not found");

            ObjectMapper.Map(input, offer);
            offer.ImageUrl = await _fileUploadService.UploadAsync(input.Image);
            var updatedOffer = await _offerRepository.UpdateAsync(offer);
            return ObjectMapper.Map<Offer, OfferDto>(updatedOffer);
        }

        public async Task<OfferDto> GetAsync(int id)
        {
            var offer = await _offerRepository.GetAsync(id);
            if (offer == null)
                throw new Exception("Not found");
            return ObjectMapper.Map<Offer, OfferDto>(offer);
        }

        public async Task<PagedResultDto<OfferDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var offers = await _offerRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
            var totalCount = await _offerRepository.GetCountAsync();

            return new PagedResultDto<OfferDto>(
                totalCount,
                ObjectMapper.Map<List<Offer>, List<OfferDto>>(offers)
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
