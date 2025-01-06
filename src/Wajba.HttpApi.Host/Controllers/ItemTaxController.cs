global using Wajba.Dtos.ItemTaxContract;
global using Wajba.ItemTaxService;

namespace Wajba.Controllers;

public class ItemTaxController : WajbaController
{
    private readonly ItemTaxAppService _itemTaxAppService;

    public ItemTaxController(ItemTaxAppService itemTaxAppService)
    {
        _itemTaxAppService = itemTaxAppService;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        try
        {
            var categories = await _itemTaxAppService.GetAllAsync(input);
            return Ok(new ApiResponse<PagedResultDto<ItemTaxDto>>
            {
                Success = true,
                Message = "ItemTax retrieved successfully.",
                Data = categories
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving ItemTaxes: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            ItemTaxDto itemTaxDto = await _itemTaxAppService.GetByIdAsync(id);
            return Ok(new ApiResponse<ItemTaxDto>
            {
                Success = true,
                Message = "itemTaxDto retrieved successfully.",
                Data = itemTaxDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "itemTaxDto not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving category: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateItemTaxDto input)
    {
        try
        {
            await _itemTaxAppService.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "ItemTax created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating itemtax: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync( UpdateItemTaxDto input)
    { 
        try
        {
            var updatedcategory = await _itemTaxAppService.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<ItemTaxDto>
            {
                Success = true,
                Message = "ItemTax updated successfully.",
                Data = updatedcategory
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "ItemTax  not found.",
                Data = null
            });
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _itemTaxAppService.DeleteAsync(id);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "ItemTax deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "ItemTax not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting ItemTax: {ex.Message}",
                Data = null
            });
        }
    }
}