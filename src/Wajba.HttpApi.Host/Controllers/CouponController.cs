global using Wajba.CouponService;
global using Wajba.Dtos.CouponContract;

namespace Wajba.Controllers;

public class CouponController : WajbaController
{
    private readonly CouponAppService _couponAppService;

    public CouponController(CouponAppService couponAppService)
    {
        _couponAppService = couponAppService;
    }

    // Create Coupon
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUpdateCouponDto input)
    {
        try
        {
            var coupon = await _couponAppService.CreateAsync(input);
            return Ok(new ApiResponse<CouponDto>
            {
                Success = true,
                Message = "Coupon created successfully.",
                Data = coupon
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating coupon: {ex.Message}",
                Data = null
            });
        }
    }

    // Get Coupon by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var coupon = await _couponAppService.GetAsync(id);
            return Ok(new ApiResponse<CouponDto>
            {
                Success = true,
                Message = "Coupon retrieved successfully.",
                Data = coupon
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving coupon: {ex.Message}",
                Data = null
            });
        }
    }

    // Get List of Coupons
    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetCouponsInput input)
    {
        try
        {
            var coupons = await _couponAppService.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<CouponDto>>
            {
                Success = true,
                Message = "Coupons retrieved successfully.",
                Data = coupons
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving coupons: {ex.Message}",
                Data = null
            });
        }
    }

    // Update Coupon
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCoupondto input)
    {
        try
        {
            var updatedCoupon = await _couponAppService.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<CouponDto>
            {
                Success = true,
                Message = "Coupon updated successfully.",
                Data = updatedCoupon
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating coupon: {ex.Message}",
                Data = null
            });
        }
    }

    // Delete Coupon
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _couponAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Coupon deleted successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting coupon: {ex.Message}",
                Data = null
            });
        }
    }
}