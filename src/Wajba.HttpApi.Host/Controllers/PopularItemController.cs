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
  

    // Create a Popular Item
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreatePopularitem input)
    {
        try
        {
            var createdPopularItem = await _popularItemAppservice.CreateAsync(input);
            return Ok(new ApiResponse<Popularitemdto>
            {
                Success = true,
                Message = "Popular item created successfully.",
                Data = createdPopularItem
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = ex.Message,
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

    // Get all Popular Items
    [HttpGet]
    public async Task<IActionResult> GetPopularItems([FromQuery] GetPopulariteminput input)
    {
        try
        {
            var popularItems = await _popularItemAppservice.GetPopularItems(input);
            return Ok(new ApiResponse<List<Popularitemdto>>
            {
                Success = true,
                Message = "Popular items retrieved successfully.",
                Data = popularItems
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving popular items: {ex.Message}",
                Data = null
            });
        }
    }

    // Get a Popular Item by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPopularItemByIdAsync(int id)
    {
        try
        {
            var popularItem = await _popularItemAppservice.GetPopularItemById(id);
            return Ok(new ApiResponse<Popularitemdto>
            {
                Success = true,
                Message = "Popular item retrieved successfully.",
                Data = popularItem
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving popular item: {ex.Message}",
                Data = null
            });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdatePopularitem input)
    {
        try
        {
            var updatedPopularItem = await _popularItemAppservice.UpdateAsync(id, input);
            return Ok(new ApiResponse<Popularitemdto>
            {
                Success = true,
                Message = "Popular item updated successfully.",
                Data = updatedPopularItem
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating popular item: {ex.Message}",
                Data = null
            });
        }
    }

    // Delete a Popular Item
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _popularItemAppservice.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Popular item deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting popular item: {ex.Message}",
                Data = null
            });
        }
    }
}


