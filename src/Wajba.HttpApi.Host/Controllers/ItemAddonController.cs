global using Wajba.Dtos.ItemAddonContract;

namespace Wajba.Controllers;


public class ItemAddonController : WajbaController
{
    private readonly IItemAddonAppService _itemAddonAppService;

    public ItemAddonController(IItemAddonAppService itemAddonAppService)
    {
        _itemAddonAppService = itemAddonAppService;
    }

    [HttpGet("item/{itemId}")]
    public async Task<IActionResult> GetAddonsByItemIdAsync(int itemId)
    {
        try
        {
            var addons = await _itemAddonAppService.GetByItemIdAsync(itemId);
            return Ok(new ApiResponse<List<ItemAddonDto>>
            {
                Success = true,
                Message = "Addons retrieved successfully.",
                Data = addons
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving addons: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet("item/{itemId}/addon/{addonId}")]
    public async Task<IActionResult> GetByIdAsync(int itemId,int addonId)
    {
        try
        {
            var addon = await _itemAddonAppService.GetByIdAsync(itemId, addonId);

            return Ok(new ApiResponse<ItemAddonDto>
            {
                Success = true,
                Message = "Item addon retrieved successfully.",
                Data = addon
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item addon not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving item addon: {ex.Message}",
                Data = null
            });
        }
    }
    [IgnoreAntiforgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateItemAddonDto input)
    {
        try
        {
            var createdAddon = await _itemAddonAppService.CreateAsync(input);

            return Ok(new ApiResponse<ItemAddonDto>
            {
                Success = true,
                Message = "Item addon created successfully.",
                Data = createdAddon
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating item addon: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAddonForItemAsync(  UpdateItemAddonDto input)
    {
        try
        {
            var updatedAddon = await _itemAddonAppService.UpdateForSpecificItemAsync(input.ItemId, input.addonId, input);
            return Ok(new ApiResponse<ItemAddonDto>
            {
                Success = true,
                Message = "Addon updated successfully.",
                Data = updatedAddon
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Addon not found for the specified item.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating addon: {ex.Message}",
                Data = null
            });
        }
    }


    [HttpDelete("item/{itemId}/addon/{addonId}")]
    public async Task<IActionResult> DeleteAddonForItemAsync(int itemId, int addonId)
    {
        try
        {
            await _itemAddonAppService.DeleteForSpecificItemAsync(itemId, addonId);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Item addon deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item addon not found for the specified item.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting item addon: {ex.Message}",
                Data = null
            });
        }
    }
}
