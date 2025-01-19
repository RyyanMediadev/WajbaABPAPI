global using Wajba.Languages;
global using Wajba.Dtos.Languages;

namespace Wajba.Controllers;


public class LanguageController : WajbaController
{
    private readonly LanguageAppService _languageAppService;

    public LanguageController(LanguageAppService languageAppService)
    {
        _languageAppService = languageAppService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetLanguageInput dto)
    {
        try
        {
            var languages = await _languageAppService.GetListAsync(dto);
            return Ok(new ApiResponse<PagedResultDto<LanguageDto>>
            {
                Data = languages,
                Message = "Languages retrieved successfully.",
                Success = true
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Data = null,
                Message = $"Error retrieving languages: {ex.Message}",
            });
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<LanguageDto>>> GetByIdAsync(int id)
    {
        try
        {
            LanguageDto languageDto = await _languageAppService.GetAsync(id);
            return Ok(new ApiResponse<object>
            {
                Data = languageDto,
                Success = true,
                Message = "Language retrieved successfully.",
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Language not found.",
                Data = null
            });
        }
    }
    [HttpPost]
    public async Task<IActionResult> Createasync(CreateUpdateLanguageDto languageDto)
    {
        try
        {
            await _languageAppService.CreateAsync(languageDto);
            return Ok(new ApiResponse<object>
            {
                Data = languageDto,
                Success = true,
                Message = "Language created successfully.",
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating language: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<IActionResult> Upadte(UpdateLanguagedto update)
    {
        try
        {
            LanguageDto languageDto = await _languageAppService.UpdateAsync(update.Id, update);
            return Ok(new ApiResponse<object>
            {
                Data = languageDto,
                Success = true,
                Message = "language updated successfully.",
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>()
            {
                Message = "language not found.",
                Success = false,
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _languageAppService.DeleteAsync(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Language deleted successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting language: {ex.Message}",
                Data = null
            });
        }
    }
}