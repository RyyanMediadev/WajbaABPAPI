global using Wajba.DineIntableService;
global using Wajba.Dtos.DineInTableContract;

namespace Wajba.Controllers;

public class DineIntableController : WajbaController
{
    private readonly DineinTableAppServices _dineinTableAppServices;

    public DineIntableController(DineinTableAppServices dineinTableAppServices)
    {
        _dineinTableAppServices = dineinTableAppServices;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateDineIntable input)
    {
        try
        {
            await _dineinTableAppServices.CreateAsync(input);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "DineTable created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating DineTable: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id,  CreateDineIntable input)
    {
        try
        {
            DiniINDto dineInTable = await _dineinTableAppServices.UpdateAsync(id, input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "dineInTable updated successfully.",
                Data = dineInTable
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "dineInTable not found.",
                Data = null
            });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var category = await _dineinTableAppServices.GetByIdAsync(id);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "dineInTable retrieved successfully.",
                Data = category
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "dineInTable not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving dineInTable: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetDiniTableInput input)

    {
        try
        {
            var dto = await _dineinTableAppServices.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<DiniINDto>>
            {
                Success = true,
                Message = "dinetables retrieved successfully.",
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving dinetables: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _dineinTableAppServices.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "DinieTable deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "DinieTable not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting DinieTable: {ex.Message}",
                Data = null
            });
        }
    }

}
