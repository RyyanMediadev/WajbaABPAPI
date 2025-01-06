global using Microsoft.AspNetCore.Http;
global using Wajba.Dtos.ThemesContract;
global using Wajba.ThemesService;

namespace Wajba.Controllers;

public class ThemeController : WajbaController
{
    private readonly ThemesAppservice _themesAppservice;

    public ThemeController(ThemesAppservice themesAppservice)
    {
        _themesAppservice = themesAppservice;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(IFormFile BrowserTabIconUrl, IFormFile FooterLogoUrl, IFormFile LogoUrl)
    {
        CreateThemesDto input = new CreateThemesDto()
        {
            BrowserTabIconUrl = BrowserTabIconUrl,
            FooterLogoUrl = FooterLogoUrl,
            LogoUrl = LogoUrl
        };
        try
        {
            await _themesAppservice.CreateAsync(input);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Theme created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating theme: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(CreateThemesDto  createThemesDto)
    {
        //CreateThemesDto input = new CreateThemesDto()
        //{
        //    BrowserTabIconUrl = BrowserTabIconUrl,
        //    FooterLogoUrl = FooterLogoUrl,
        //    LogoUrl = LogoUrl
        //};
        try
        {
            var updatedcategory = await _themesAppservice.UpdateAsync(createThemesDto);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "THeme updated successfully.",
                Data = updatedcategory
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Theme not found.",
                Data = null
            });
        }
    }
    //[HttpPut("UpdateBrowserTabIconUrlAsync")]
    //public async Task<IActionResult> UpdateBrowserTabIconUrlAsync()
    //{
    //    try
    //    {
    //        var updatedcategory = await _themesAppservice.UpdateBrowserTabIconUrlAsync();
    //        return Ok(new ApiResponse<object>
    //        {
    //            Success = true,
    //            Message = "BrowserTabIconUrl updated successfully.",
    //            Data = updatedcategory
    //        });
    //    }
    //    catch (EntityNotFoundException)
    //    {
    //        return NotFound(new ApiResponse<object>
    //        {
    //            Success = false,
    //            Message = "BrowserTabIconUrl not found.",
    //            Data = null
    //        });
    //    }
    //    return null;
    //}
    [HttpGet]
    public async Task<IActionResult> GetByIdAsync()
    {
        try
        {
           var category = await _themesAppservice.GetByIdAsync();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Category retrieved successfully.",
                Data = category
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Category not found.",
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

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync()
    {
        try
        {
            await _themesAppservice.DeleteAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Theme deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Theme not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting theme: {ex.Message}",
                Data = null
            });
        }
    }
}
