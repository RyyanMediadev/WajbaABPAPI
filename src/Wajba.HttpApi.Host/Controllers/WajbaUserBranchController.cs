
using Wajba.Dtos.WajbaUserBranchContract;
using Wajba.WajbaUserBranchService;


namespace Wajba.Controllers;

public class WajbaUserBranchController : WajbaController
{
    private readonly WajbaUserBranchAppService _WajbaUserBranchAppService;

    public WajbaUserBranchController(WajbaUserBranchAppService WajbaUserBranchAppService)
    {
        _WajbaUserBranchAppService = WajbaUserBranchAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(WajbaUserBranchCreateDto input)
    {
        try
        {
            await _WajbaUserBranchAppService.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "WajbaUserBranch created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating branch: {ex.Message}",
                Data = null
            });
        }
    }

    //[HttpPut]
    //public async Task<IActionResult> UpdateAsync(UpdateWajbaUserBranchDto input)
    //{
    //    try
    //    {
    //        var updatedWajbaUserBranch = await _branchAppService.UpdateAsync(input.Id, input);
    //        return Ok(new ApiResponse<WajbaUserBranchDto>
    //        {
    //            Success = true,
    //            Message = "WajbaUserBranch updated successfully.",
    //            Data = updatedWajbaUserBranch
    //        });
    //    }
    //    catch (EntityNotFoundException)
    //    {
    //        return NotFound(new ApiResponse<object>
    //        {
    //            Success = false,
    //            Message = "WajbaUserBranch not found.",
    //            Data = null
    //        });
    //    }
    //}

    //[HttpGet("{id}")]
    //public async Task<ActionResult<ApiResponse<WajbaUserBranchDto>>> GetByIdAsync(int id)
    //{
    //    try
    //    {
    //        var branch = await _branchAppService.GetByIdAsync(id);
    //        return Ok(new ApiResponse<WajbaUserBranchDto>
    //        {
    //            Success = true,
    //            Message = "WajbaUserBranch retrieved successfully.",
    //            Data = branch
    //        });
    //    }
    //    catch (EntityNotFoundException)
    //    {
    //        return NotFound(new ApiResponse<object>
    //        {
    //            Success = false,
    //            Message = "WajbaUserBranch not found.",
    //            Data = null
    //        });
    //    }
    //}

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] WajbaUserBranchDto WajbaUserBranchDto)
    {
        try
        {
            var branches = await _WajbaUserBranchAppService.GetListAsync(WajbaUserBranchDto);
            return Ok(new ApiResponse<PagedResultDto<WajbaUserBranchDto>>
            {
                Success = true,
                Message = "WajbaUserBranches retrieved successfully.",
                Data = branches
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving branches: {ex.Message}",
                Data = null
            });
        }
    }

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteAsync(int id)
    //{
    //    try
    //    {
    //        await _WajbaUserBranchAppService.DeleteAsync(id);
    //        return Ok(new ApiResponse<object>
    //        {
    //            Success = true,
    //            Message = "WajbaUserBranch deleted successfully.",
    //            Data = null
    //        });
    //    }
    //    catch (EntityNotFoundException)
    //    {
    //        return NotFound(new ApiResponse<object>
    //        {
    //            Success = false,
    //            Message = "WajbaUserBranch not found.",
    //            Data = null
    //        });
    //    }
    //}
}