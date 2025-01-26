global using Wajba.Dtos.ItemsDtos;
global using Wajba.ItemServices;

namespace Wajba.Controllers;

public class ItemController : WajbaController
{
    private readonly ItemAppServices _itemAppServices;

    public ItemController(ItemAppServices itemAppServices)
    {
        _itemAppServices = itemAppServices;
    }

    [HttpGet("name/categoryid")]
    public async Task<ActionResult<ApiResponse<List<ItemDto>>>> GetItemsByCategory([FromQuery] int? categoryId, [FromQuery] string? name)
    {
        try
        {
            var itemDto = await _itemAppServices.GetItemsByCategoryAsync(categoryId, name);
            return Ok(new ApiResponse<List<ItemDto>>
            {
                Success = true,
                Message = "Item created successfully.",
                Data = itemDto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating item: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet("by-branch/{branchId}")]
    public async Task<List<ItemDto>> GetItemsByBranch(int branchId)
    {
        return await _itemAppServices.GetItemsByBranchAsync(branchId);
    }

    [HttpGet("{id}/details")]
    public async Task<ItemDto> GetItemWithDetails(int id)
    {
        return await _itemAppServices.GetItemWithDetailsAsync(id);
    }
    [HttpGet("{id}/details-transformed")]
    public async Task<IActionResult> GetItemWithTransformedDetails(int id)
    {
        try
        {
            var item = await _itemAppServices.GetItemWithTransformedDetailsAsync(id);
            return Ok(new { success = true, data = item });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateItemDto input)
    {
        try
        {
            ItemDto itemDto = await _itemAppServices.CreateAsync(input);
            return Ok(new ApiResponse<ItemDto>
            {
                Success = true,
                Message = "Item created successfully.",
                Data = itemDto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating item: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateItemDTO input)
    {
        try
        {
            ItemDto itemDto = await _itemAppServices.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<ItemDto>
            {
                Success = true,
                Message = "Item updated successfully.",
                Data = itemDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item not found.",
                Data = null
            });
        }
    }
    [HttpPut("editimage")]
    public async Task<IActionResult> UpdatImage(int id, Base64ImageModel model)

    {
        try
        {
            ItemDto itemDto = await _itemAppServices.updateimage(id, model);
            return Ok(new ApiResponse<ItemDto>
            {
                Success = true,
                Message = "Itemimage uppated successfully.",
                Data = itemDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error update imageitem: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetItemInput input)
    {
        try
        {
            PagedResultDto<ItemDto> itemDtos = await _itemAppServices.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<ItemDto>>
            {
                Success = true,
                Message = "Items retrieved successfully.",
                Data = itemDtos
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving items: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            ItemDto itemDto = await _itemAppServices.GetByIdAsync(id);
            return Ok(new ApiResponse<ItemDto>
            {
                Success = true,
                Message = "Item retrieved successfully.",
                Data = itemDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving item: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _itemAppServices.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Item deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting item: {ex.Message}",
                Data = null
            });
        }
    }
}