﻿global using Wajba.Dtos.ThemesContract;
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
    public async Task<IActionResult> CreateAsync(CreateThemesDto createThemes)
    {
        try
        {
            await _themesAppservice.CreateAsync(createThemes);

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

    [HttpPost]
    [Route("upload-base64")]
    public IActionResult UploadBase64Image([FromBody] Base64ImageModel model)
    {
        if (string.IsNullOrEmpty(model.Base64Content))
            return BadRequest("No file content provided.");

        var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images");

        if (!Directory.Exists(uploadsFolderPath))
            Directory.CreateDirectory(uploadsFolderPath);

        var filePath = Path.Combine(uploadsFolderPath, model.FileName+".jpg");
        var fileBytes = Convert.FromBase64String(model.Base64Content);

        System.IO.File.WriteAllBytes(filePath, fileBytes);

        return Ok(new
        {
            Message = "File uploaded successfully",
            FileName = model.FileName,
            FilePath = filePath
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(CreateThemesDto themesDto)
    {
        try
        {
            var updatedcategory = await _themesAppservice.UpdateAsync(themesDto);
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
    [HttpGet]
    public async Task<IActionResult> GetAsync()
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