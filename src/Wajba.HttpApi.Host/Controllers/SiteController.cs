﻿global using Wajba.Dtos.SitesContact;
global using Wajba.SiteService;

namespace Wajba.Controllers;

[IgnoreAntiforgeryToken]

public class SiteController :AbpController
{
    private readonly SitesAppservice _sitesAppservice;
    public SiteController(SitesAppservice sitesAppservice)
    {
        _sitesAppservice = sitesAppservice;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateSiteDto input)
    {
        try
        {
            await _sitesAppservice.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Site created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating site: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateSiteDto input)
    {
        try
        {
            var updatedsite = await _sitesAppservice.UpdateAsync(id, input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Site updated successfully.",
                Data = updatedsite
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Site not found.",
                Data = null
            });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            SiteDto site = await _sitesAppservice.GetByIdAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Site retrieved successfully.",
                Data = site
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Site not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving site: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetSiteInput input)
    {
        try
        {
            var dto = await _sitesAppservice.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<SiteDto>>
            {
                Success = true,
                Message = "sites retrieved successfully.",
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving sites: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _sitesAppservice.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Site deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Site not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting site: {ex.Message}",
                Data = null
            });
        }
    }

}