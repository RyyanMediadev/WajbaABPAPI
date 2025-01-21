global using Wajba.FaqService;
global using Wajba.Dtos.FaqsContract;

namespace Wajba.Controllers;

public class FaqController : WajbaController
{
    private readonly FaqAppService _faqAppService;

    public FaqController(FaqAppService faqAppService)
    {
        _faqAppService = faqAppService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateFaqs input)
    {
        try
        {
            await _faqAppService.CreateAsync(input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Faq created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating Faq: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpadtefaqDto input)
    {
        try
        {
            FaqDto faqDto = await _faqAppService.UpdateAsync(input.Id, input);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Faq updated successfully.",
                Data = faqDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Faq not found.",
                Data = null
            });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            FaqDto faqDto = await _faqAppService.GetByIdAsync(id);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "faq retrieved successfully.",
                Data = faqDto
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "faq not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving faq: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] GetFaqInput input)

    {
        try
        {
            var pagedResultDto = await _faqAppService.GetListAsync(input);
            return Ok(new ApiResponse<PagedResultDto<FaqDto>>
            {
                Success = true,
                Message = "Faqs retrieved successfully.",
                Data = pagedResultDto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving faqs: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _faqAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Faqs deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Faqs not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting faq: {ex.Message}",
                Data = null
            });
        }
    }
}