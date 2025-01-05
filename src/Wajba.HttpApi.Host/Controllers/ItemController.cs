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
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateItemDto input)
    {
        try
        {
            ItemDto itemDto = await _itemAppServices.CreateAsync(input);
            return Ok(new ApiResponse<object>
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
    public async Task<IActionResult> UpdateAsync(int id,  CreateItemDto input)
    {
        try
        {
            ItemDto itemDto = await _itemAppServices.UpdateAsync(id, input);
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