global using Wajba.Dtos.CurrenciesContract;
global using Wajba.CurrenciesService;

namespace Wajba.Controllers;

//[Route("api/[controller]")]
//[ApiController]
public class CurrenciesController : AbpController
{
    private readonly CurrenciesAppService _currenciesAppService;

    public CurrenciesController(CurrenciesAppService currenciesAppService)
    {
        _currenciesAppService = currenciesAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUpdateCurrenciesDto input)
    {
        try
        {
            await _currenciesAppService.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Currencies created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating currencies: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpadteCurrency input)
    {
        try
        {
            var updatedCurrencies = await _currenciesAppService.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<CurrenciesDto>
            {
                Success = true,
                Message = "Currencies updated successfully.",
                Data = updatedCurrencies
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Currencies not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating currencies: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var currencies = await _currenciesAppService.GetByIdAsync(id);
            return Ok(new ApiResponse<CurrenciesDto>
            {
                Success = true,
                Message = "Currencies retrieved successfully.",
                Data = currencies
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Currencies not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving currencies: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        try
        {
            var currencies = await _currenciesAppService.GetAllAsync(input);
            return Ok(new ApiResponse<PagedResultDto<CurrenciesDto>>
            {
                Success = true,
                Message = "Currencies retrieved successfully.",
                Data = currencies
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving currencies: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _currenciesAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Currencies deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Currencies not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting currencies: {ex.Message}",
                Data = null
            });
        }
    }
}