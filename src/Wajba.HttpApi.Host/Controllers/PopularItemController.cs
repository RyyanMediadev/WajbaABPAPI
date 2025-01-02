global using Wajba.PopularItemServices;
using Wajba.Dtos.PopularItemstoday;

namespace Wajba.Controllers;

public class PopularItemController : WajbaController
{
    private readonly PopularItemAppservice _popularItemAppservice;

    public PopularItemController(PopularItemAppservice popularItemAppservice)
    {
        _popularItemAppservice = popularItemAppservice;
    }
    public async Task<IActionResult> CreateAsync(CreatePopularitem input)
    {
        try
        {
            await _popularItemAppservice.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Popular Item created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating popular item: {ex.Message}",
                Data = null
            });
        }
    }
    public async Task<IActionResult> UpdateAsync(UpdatePopularItemdto input)
    {
        try
        {
            PopularItemDto popularItemDto = await _popularItemAppservice.UpdateAsync(input.ItemId, input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Popular Item updated successfully.",
                Data = popularItemDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Popular Item not found.",
                Data = null
            });
        }
    }
}
