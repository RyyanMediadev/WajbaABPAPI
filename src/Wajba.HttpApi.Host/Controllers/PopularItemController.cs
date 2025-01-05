global using Wajba.PopularItemServices;
global using Wajba.Dtos.PopularItemstoday;

namespace Wajba.Controllers;


public class PopularItemController : WajbaController
{
    private readonly PopularItemAppservice _popularItemAppservice;

    public PopularItemController(PopularItemAppservice popularItemAppservice)
    {
        _popularItemAppservice = popularItemAppservice;
    }
    [HttpPost]
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
    [HttpGet]
    public async Task<IActionResult> GetAsync(GetPopulariteminput input)
    {
        try
        {
            var popularItem = await _popularItemAppservice.GetPopularItems(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Popular Item retrieved successfully.",
                Data = popularItem
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            Popularitemdto popularItem = await _popularItemAppservice.GetPopularItemById(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Popular Item retrieved successfully.",
                Data = popularItem
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
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePopularItemdto input)
    {
        try
        {
            Popularitemdto popularItemDto = await _popularItemAppservice.UpdateAsync(input.ItemId, input);
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

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _popularItemAppservice.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Popular Item deleted successfully.",
                Data = null
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
