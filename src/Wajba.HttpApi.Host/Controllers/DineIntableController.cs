﻿global using Wajba.DineIntableService;
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
    public async Task<IActionResult> CreateAsync(CreateDineIntable input)
    {
        try
        {
            DiniINTableDto diniINDto = await _dineinTableAppServices.CreateAsync(input);
            return Ok(new ApiResponse<DiniINTableDto>
            {
                Success = true,
                Message = "DineTable created successfully.",
                Data = diniINDto
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

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateDinInTable input)
    {
        try
        {
            DiniINTableDto dineInTable = await _dineinTableAppServices.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<DiniINTableDto>
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
    public async Task<ActionResult<ApiResponse<DiniINTableDto>>> GetByIdAsync(int id)
    {
        try
        {
            DiniINTableDto dini = await _dineinTableAppServices.GetByIdAsync(id);

            return Ok(new ApiResponse<DiniINTableDto>
            {
                Success = true,
                Message = "dineInTable retrieved successfully.",
                Data = dini
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
            return Ok(new ApiResponse<PagedResultDto<DiniINTableDto>>
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