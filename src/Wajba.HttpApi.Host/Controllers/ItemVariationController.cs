using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wajba.Dtos.ItemVariationContract;
using Wajba.Models.Items;

namespace Wajba.Controllers
{
 
    public class ItemVariationController : WajbaController
    {
        private readonly IItemVariationAppService _appService;

        public ItemVariationController(IItemVariationAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var itemVariation = await _appService.GetAsync(id);

                return Ok(new ApiResponse<ItemVariationDto>
                {
                    Success = true,
                    Message = "Item variation retrieved successfully.",
                    Data = itemVariation
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Item variation not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error retrieving item variation: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("item/{itemId}")]
        public async Task<IActionResult> GetVariationsByItemIdAsync(int itemId)
        {
            try
            {
                var variations = await _appService.GetListByItemIdAsync(itemId);
                return Ok(new ApiResponse<List<ItemVariationDto>>
                {
                    Success = true,
                    Message = "Variations retrieved successfully.",
                    Data = variations
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error retrieving variations: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateItemVariationDto input)
        {
            try
            {
                var createdItemVariation = await _appService.CreateAsync(input);

                return Ok(new ApiResponse<ItemVariationDto>
                {
                    Success = true,
                    Message = "Item variation created successfully.",
                    Data = createdItemVariation
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error creating item variation: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("item/{itemId}/variation/{variationId}")]
        public async Task<IActionResult> UpdateVariationForItemAsync(int itemId, int variationId, [FromBody] UpdateItemVariationDto input)
        {
            try
            {
                var updatedVariation = await _appService.UpdateForSpecificItemAsync(itemId, variationId, input);
                return Ok(new ApiResponse<ItemVariationDto>
                {
                    Success = true,
                    Message = "Variation updated successfully.",
                    Data = updatedVariation
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Variation not found for the specified item.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error updating variation: {ex.Message}",
                    Data = null
                });
            }
         }
        [HttpDelete("item/{itemId}/variation/{variationId}")]
        public async Task<IActionResult> DeleteAsync(int itemId, int variationId)
        {
          
            try
            {
                await _appService.DeleteForSpecificItemAsync(itemId, variationId);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Item variation deleted successfully.",
                    Data = null
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Item variation not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error deleting item variation: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
