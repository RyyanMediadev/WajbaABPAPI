global using Wajba.Dtos.ItemAttributes;

namespace Wajba.Controllers;

//[Route("api/[controller]")]
//[ApiController]
public class ItemAttributeController : AbpController
{
    private readonly IItemAttributeAppService _itemAttributeAppService;

    public ItemAttributeController(IItemAttributeAppService itemAttributeAppService)
    {
        _itemAttributeAppService = itemAttributeAppService;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        try
        {
            var result = await _itemAttributeAppService.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<ItemAttributeDto>>
            {
                Success = true,
                Message = "Item attributes retrieved successfully.",
                Data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving item attributes: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var result = await _itemAttributeAppService.GetAsync(id);
            return Ok(new ApiResponse<ItemAttributeDto>
            {
                Success = true,
                Message = "Item attribute retrieved successfully.",
                Data = result
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item attribute not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving item attribute: {ex.Message}",
                Data = null
            });
        }
    }
    [IgnoreAntiforgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateItemAttributeDto input)
    {
        try
        {
            var result = await _itemAttributeAppService.CreateAsync(input);
            return Ok(new ApiResponse<ItemAttributeDto>
            {
                Success = true,
                Message = "Item attribute created successfully.",
                Data = result
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating item attribute: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateItemAttributeDto input)
    {
        try
        {
            var result = await _itemAttributeAppService.UpdateAsync(input.id, input);
            return Ok(new ApiResponse<ItemAttributeDto>
            {
                Success = true,
                Message = "Item attribute updated successfully.",
                Data = result
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item attribute not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating item attribute: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _itemAttributeAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Item attribute deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Item attribute not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting item attribute: {ex.Message}",
                Data = null
            });
        }
    }
}