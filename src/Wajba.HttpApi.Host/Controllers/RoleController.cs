using Microsoft.AspNetCore.Http.HttpResults;
using Wajba.Dtos.RoleContract;
using Wajba.OTPService;
using Wajba.RolesServices;

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
          var p=  await _roleAppservices.CreateAsync(create);
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
}
