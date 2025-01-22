global using Wajba.OfferService;
global using Wajba.Dtos.OffersContract;

namespace Wajba.Controllers;
public class OfferController : WajbaController
{
    private readonly OfferAppService _offerAppService;

    public OfferController(OfferAppService offerAppService)
    {
        _offerAppService = offerAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUpdateOfferDto input)
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
    [HttpPut("Editimage")]
    public async Task<IActionResult> Updateimage(int id, Base64ImageModel model)
    {
        try
        {
            var updatedOffer = await _offerAppService.updateimage(id, model);
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
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateOfferdto input)
    {
        try
        {
            var updatedOffer = await _offerAppService.UpdateAsync(input.Id, input);
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
    public async Task<ActionResult<ApiResponse<OfferDto>>> GetByIdAsync(int id)
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
    public async Task<IActionResult> GetListAsync([FromQuery] GetOfferInput input)
    {
        try
        {
            var offers = await _offerAppService.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<OfferDto>>
            {
                Success = true,
                Message = "Offers retrieved successfully.",
                Data = offers,
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

    [HttpDelete("Deletecategoryoffer")]
    public async Task<IActionResult> Deletecategorys(int offerid, int categoryid)
    {
        try
        {
            await _offerAppService.deletecategory(offerid, categoryid);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "categoryoffer deleted successfully.",
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

    [HttpDelete("DeleteItemsoffer")]
    public async Task<IActionResult> DeleteItems(int offerid, int itemid)
    {
        try
        {
         var p=   await _offerAppService.deleteitems(offerid, itemid);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Offeritems deleted successfully.",
                Data = p
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