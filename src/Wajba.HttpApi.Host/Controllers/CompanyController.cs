﻿global using Wajba.CompanyService;
global using Wajba.Dtos.CompanyContact;

namespace Wajba.Controllers;

public class CompanyController : WajbaController
{
    private readonly CompanyAppService _companyAppService;

    public CompanyController(CompanyAppService companyAppService)
    {
        _companyAppService = companyAppService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateComanyDto input)
    {
        if (!ModelState.IsValid)
            return BadRequest("Data is not valid");
        try
        {
            await _companyAppService.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Company created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating company: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateComanyDto input)
    {
        if (!ModelState.IsValid)
            return BadRequest("Data is not valid");
        try
        {
            CompanyDto companyDto = await _companyAppService.UpdateAsync(id, input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "companyDto updated successfully.",
                Data = companyDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "companyDto not found.",
                Data = null
            });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            CompanyDto companyDto = await _companyAppService.GetByIdAsync(id);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "companyDto retrieved successfully.",
                Data = companyDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "companyDto not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving companyDto: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetComanyInput input)
    {
        try
        {
            var dto = await _companyAppService.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<CompanyDto>>
            {
                Success = true,
                Message = "CompanyDtos retrieved successfully.",
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving CompanyDtos: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _companyAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "CompanyDto deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "CompanyDto not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting CompanyDto: {ex.Message}",
                Data = null
            });
        }
    }
}