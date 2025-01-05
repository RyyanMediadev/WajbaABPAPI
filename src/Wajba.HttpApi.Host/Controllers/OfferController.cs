global using Wajba.OffersContract;
global using Wajba.OfferService;

namespace Wajba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : AbpController
    {
        private readonly OfferAppService _offerAppService;

        public OfferController(OfferAppService offerAppService)
        {
            _offerAppService = offerAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateUpdateOfferDto input)
        {
            try
            {
                var offer = await _offerAppService.CreateAsync(input);
                return Ok(new ApiResponse<OfferDto>
                {
                    Success = true,
                    Message = "Offer created successfully.",
                    Data = offer
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error creating offer: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateUpdateOfferDto input)
        {
            try
            {
                var updatedOffer = await _offerAppService.UpdateAsync(id, input);
                return Ok(new ApiResponse<OfferDto>
                {
                    Success = true,
                    Message = "Offer updated successfully.",
                    Data = updatedOffer
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Offer not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error updating offer: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var offer = await _offerAppService.GetAsync(id);
                return Ok(new ApiResponse<OfferDto>
                {
                    Success = true,
                    Message = "Offer retrieved successfully.",
                    Data = offer
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Offer not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error retrieving offer: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            try
            {
                //var offers = await _offerAppService.GetListAsync(input);
                return Ok(new ApiResponse<PagedResultDto<OfferDto>>
                {
                    Success = true,
                    Message = "Offers retrieved successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error retrieving offers: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _offerAppService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Offer deleted successfully.",
                    Data = null
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Offer not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error deleting offer: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}