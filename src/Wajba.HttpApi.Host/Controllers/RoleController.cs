global using Wajba.Dtos.RoleContract;
global using Wajba.RolesServices;
using Wajba.ThemesService;

namespace Wajba.Controllers;

public class RoleController : WajbaController
{
    private readonly RoleAppservices _roleAppservices;

    public RoleController(RoleAppservices roleAppservices)
    {
        _roleAppservices = roleAppservices;
    }
    [HttpPost]
    public async Task<ActionResult<ApiResponse<RolesDto>>> Createasync(CreateRole create)
    {
        try
        {
            var p = await _roleAppservices.CreateAsync(create);
            return Ok(new ApiResponse<RolesDto>
            {
                Success = true,
                Message = "Role created successfully.",
                Data = p
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating Role: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<ActionResult<ApiResponse<RolesDto>>> UpdateAsync(UpdateRole updateRole)
    {
        try
        {
            RolesDto rolesDto = await _roleAppservices.Update(updateRole);
            return Ok(new ApiResponse<RolesDto>
            {
                Success = true,
                Message = "Role updated successfully.",
                Data = rolesDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Role not found.",
                Data = null
            });
        }
    }
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResultDto<RolesDto>>>> Getall()
    {
        try
        {
            var p = await _roleAppservices.GetAll();
            return Ok(new ApiResponse<PagedResultDto<RolesDto>>
            {
                Success = true,
                Message = "Role retrived successfully.",
                Data = p
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrive Role: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet("GetById{id}")]
    public async Task<ActionResult<ApiResponse<RolesDto>>> GetByIdAsync(int id)
    {
        try
        {
            RolesDto role = await _roleAppservices.GetById(id);
            return Ok(new ApiResponse<RolesDto>
            {
                Success = true,
                Message = "Role retrieved successfully.",
                Data = role
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Role not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving Role: {ex.Message}",
                Data = null
            });
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _roleAppservices.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Role deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Role not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting Role: {ex.Message}",
                Data = null
            });
        }
    }
}