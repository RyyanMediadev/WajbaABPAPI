global using Wajba.Dtos.OTPContract;
global using Wajba.OTPService;

namespace Wajba.Controllers;

public class OTPController : WajbaController
{
    private readonly OTPAppService _oTPAppService;

    public OTPController(OTPAppService oTPAppService)
    {
        _oTPAppService = oTPAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync( CreateUpdateOTPDto input)
    {
        try
        {
            await _oTPAppService.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "OTP created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating OTP: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var oTPDtos = await _oTPAppService.GetByIdAsync();
            return Ok(new ApiResponse<OTPDto>
            {
                Success = true,
                Message = "OTP retrieved successfully.",
                Data = oTPDtos
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving OTP: {ex.Message}",
                Data = null
            });
        }
    }
    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetAsync(int id)
    //{
    //    try
    //    {
    //        var oTPDto = await _oTPAppService.GetByIdAsync(id);
    //        return Ok(new ApiResponse<OTPDto>
    //        {
    //            Success = true,
    //            Message = "OTP retrieved successfully.",
    //            Data = oTPDto
    //        });
    //    }
    //    catch (EntityNotFoundException ex)
    //    {
    //        return NotFound(new ApiResponse<object>
    //        {
    //            Success = false,
    //            Message = $"Error retrieving OTP: {ex.Message}",
    //            Data = null
    //        });
    //    }
    //}
    [HttpPut]
    public async Task<IActionResult> UpdateAsync( UpdateOtpDto input)
    {
        try
        {
            var updatedOTP = await _oTPAppService.UpdateAsync( input);
            return Ok(new ApiResponse<OTPDto>
            {
                Success = true,
                Message = "OTP updated successfully.",
                Data = updatedOTP
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating OTP: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync()
    {
        try
        {
            await _oTPAppService.DeleteAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "OTP deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting OTP: {ex.Message}",
                Data = null
            });
        }
    }
}