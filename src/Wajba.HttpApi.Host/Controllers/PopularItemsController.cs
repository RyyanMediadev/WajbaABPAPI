global using Wajba.Dtos.PopularItemstoday;
global using Wajba.PopularItemServices;

namespace Wajba.Controllers;

public class PopularItemsController : WajbaController
{
    private readonly PopularItemAppservice _popularItemAppservice;

    public PopularItemsController(PopularItemAppservice popularItemAppservice)
    {
        _popularItemAppservice = popularItemAppservice;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]CreatePopularitem input)
    {
        try
        {
         var p=   await _popularItemAppservice.CreateAsync(input);
            return Ok(new ApiResponse<Popularitemdto>
            {
                Success = true,
                Message = "Popular Item created successfully.",
                Data = p
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
    public async Task<IActionResult> GetAsync([FromQuery]GetPopulariteminput input)
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
    public async Task<ActionResult<ApiResponse<Popularitemdto>>> GetByIdAsync(int id)
    {
        try
        {
            Popularitemdto popularItem = await _popularItemAppservice.GetPopularItemById(id);
            return Ok(new ApiResponse<Popularitemdto>
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
    public async Task<IActionResult> UpdateAsync([FromBody]UpdatePopularItemdto input)
    {
        try
        {
            Popularitemdto popularItemDto = await _popularItemAppservice.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<Popularitemdto>
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
    [HttpPut("UpdateImage")]
    public async Task<IActionResult> UpdateImage([FromBody] UpdateImage updateImage)
    {
        try
        {
          var p=  await _popularItemAppservice.Updateimage(updateImage.Id, updateImage.model);
            return Ok(new ApiResponse<Popularitemdto>
            {
                Success = true,
                Message = "Popular Itemiamge updated successfully.",
                Data = p
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
        catch (Exception ex)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = $"Popular Item not found.{ex.Message}",
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