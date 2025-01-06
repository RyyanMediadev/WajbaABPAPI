global using Microsoft.AspNetCore.Mvc;
global using Volo.Abp.Application.Dtos;
global using Volo.Abp.Domain.Entities;
global using Wajba.APIResponse;
global using Wajba.Categories;
global using Wajba.Dtos.Categories;

namespace Wajba.Controllers;

public class CategoryController : WajbaController
{
    private readonly CategoryAppService _categoryAppService;

    public CategoryController(CategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUpdateCategoryDto input)
    {
        try
        {
            // Call the service to create the category
            await _categoryAppService.CreateAsync(input);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Category created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating category: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCategory input)
    {
        try
        {
            var updatedcategory = await _categoryAppService.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<CategoryDto>
            {
                Success = true,
                Message = "Category updated successfully.",
                Data = updatedcategory
            });
        }
        catch (EntityNotFoundException)
        {
            // If the category is not found, return a 404 Not Found response
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Category not found.",
                Data = null
            });
        }

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            // Call the service to get the category by ID
            var category = await _categoryAppService.GetByIdAsync(id);

            return Ok(new ApiResponse<CategoryDto>
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
    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetCategoryInput input)
    {
        try
        {
            // Call the service to get the list of categories
            var categories = await _categoryAppService.GetListAsync(input);

            return Ok(new ApiResponse<PagedResultDto<CategoryDto>>
            {
                Success = true,
                Message = "Categories retrieved successfully.",
                Data = categories
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving categories: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpGet("getcategoryItems{branchid:int}")]
    public async Task<IActionResult> GetCategoryItemsdto(int branchid)
    {
        try
        {
            var categories = await _categoryAppService.GetCategoryItemsDtosAsync(branchid);
            return Ok(new ApiResponse<PagedResultDto<CategoryItemsDto>>
            {
                Success = true,
                Message = "Categories retrieved successfully.",
                Data = categories
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving categories: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _categoryAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Category deleted successfully.",
                Data = null
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
                Message = $"Error deleting category: {ex.Message}",
                Data = null
            });
        }
    }
}