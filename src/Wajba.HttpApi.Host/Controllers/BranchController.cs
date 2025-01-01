global using Wajba.Dtos.BranchContract;
global using Wajba.BranchService;

namespace Wajba.Controllers;

public class BranchController : WajbaController
{
    private readonly BranchAppService _branchAppService;

    public BranchController(BranchAppService branchAppService)
    {
        _branchAppService = branchAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync( CreateBranchDto input)
    {
        try
        {
            await _branchAppService.CreateAsync(input);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Branch created successfully.",
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

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(  UpdateBranchDto input)
    {
        try
        {
            var updatedBranch = await _branchAppService.UpdateAsync(input.Id, input);

            return Ok(new ApiResponse<BranchDto>
            {
                Success = true,
                Message = "Branch updated successfully.",
                Data = updatedBranch
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Branch not found.",
                Data = null
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var branch = await _branchAppService.GetByIdAsync(id);

            return Ok(new ApiResponse<BranchDto>
            {
                Success = true,
                Message = "Branch retrieved successfully.",
                Data = branch
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Branch not found.",
                Data = null
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetBranchInput input)
    {
        try
        {
            var branches = await _branchAppService.GetListAsync(input);

            return Ok(new ApiResponse<PagedResultDto<BranchDto>>
            {
                Success = true,
                Message = "Branches retrieved successfully.",
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _branchAppService.DeleteAsync(id);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Branch deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Branch not found.",
                Data = null
            });
        }
    }
}