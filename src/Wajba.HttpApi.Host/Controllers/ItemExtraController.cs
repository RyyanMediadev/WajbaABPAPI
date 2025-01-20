global using Wajba.Dtos.ItemExtraContract;

namespace Wajba.Controllers
{

    public class ItemExtraController : WajbaController
    {
        private readonly IItemExtraAppService _appService;

        public ItemExtraController(IItemExtraAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("item/{itemId}/extra/{extraId}")]
        public async Task<IActionResult> GetAsync(int itemId,int extraId)
        {
            try
            {
                var itemExtra = await _appService.GetAsync(itemId, extraId);

                return Ok(new ApiResponse<ItemExtraDto>
                {
                    Success = true,
                    Message = "Item extra retrieved successfully.",
                    Data = itemExtra
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Item extra not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error retrieving item extra: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("item/{itemId}")]
        public async Task<IActionResult> GetExtrasByItemIdAsync(int itemId)
        {
            try
            {
                var extras = await _appService.GetListByItemIdAsync(itemId);
                return Ok(new ApiResponse<List<ItemExtraDto>>
                {
                    Success = true,
                    Message = "Extras retrieved successfully.",
                    Data = extras
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error retrieving extras: {ex.Message}",
                    Data = null
                });
            }
        }
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateItemExtraDto input)
        {
            try
            {
                var createdItemExtra = await _appService.CreateAsync(input);

                return Ok(new ApiResponse<ItemExtraDto>
                {
                    Success = true,
                    Message = "Item extra created successfully.",
                    Data = createdItemExtra
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error creating item extra: {ex.Message}",
                    Data = null
                });
            }
        }
        [IgnoreAntiforgeryToken]
        [HttpPut]
        public async Task<IActionResult> UpdateExtraForItemAsync( UpdateItemExtraDto input)
        {
            try
            {
                var updatedExtra = await _appService.UpdateForSpecificItemAsync( input);
                return Ok(new ApiResponse<ItemExtraDto>
                {
                    Success = true,
                    Message = "Extra updated successfully.",
                    Data = updatedExtra
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Extra not found for the specified item.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error updating extra: {ex.Message}",
                    Data = null
                });
            }
       
    }

        [HttpDelete("item/{itemId}/extra/{extraId}")]
        public async Task<IActionResult> DeleteAsync(int itemId,int extraId)
        {
            try
            {
                await _appService.DeleteAsync(itemId,extraId);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Item extra deleted successfully.",
                    Data = null
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Item extra not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error deleting item extra: {ex.Message}",
                    Data = null
                });
            }
        }
    }

}
