global using Wajba.Dtos.OrderSetupContract;
global using Wajba.OrderSetupService;

namespace Wajba.Controllers;

//[Route("api/[controller]")]
//[ApiController]
public class OrderSetupController : WajbaController
{
    private readonly OrderSetupAppService _orderSetupAppService;

    public OrderSetupController(OrderSetupAppService orderSetupAppService)
    {
        _orderSetupAppService = orderSetupAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync( CreateUpdateOrderSetupDto input)
    {
        try
        {
            await _orderSetupAppService.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Order setup created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating order setup: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateOrderSetupDto input)
    {
        try
        {
            var updatedOrderSetup = await _orderSetupAppService.UpdateAsync(input);
            return Ok(new ApiResponse<OrderSetupDto>
            {
                Success = true,
                Message = "Order setup updated successfully.",
                Data = updatedOrderSetup
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Order setup not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating order setup: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var orderSetup = await _orderSetupAppService.GetByIdAsync(id);
            return Ok(new ApiResponse<OrderSetupDto>
            {
                Success = true,
                Message = "Order setup retrieved successfully.",
                Data = orderSetup
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Order setup not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving order setup: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetOrderSetupInput input)
    {
        try
        {
            var orderSetups = await _orderSetupAppService.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<OrderSetupDto>>
            {
                Success = true,
                Message = "Order setups retrieved successfully.",
                Data = orderSetups
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving order setups: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _orderSetupAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Order setup deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Order setup not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting order setup: {ex.Message}",
                Data = null
            });
        }
    }
}
