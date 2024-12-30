global using Wajba.Dtos.OTPContract;

namespace Wajba.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OTPController : AbpController, IOTPAppService
{
    private readonly IOTPAppService _otpAppService;

    public OTPController(IOTPAppService otpAppService)
    {
        _otpAppService = otpAppService;
    }

    [HttpGet]
    public Task<PagedResultDto<OTPDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return _otpAppService.GetListAsync(input);
    }

    [HttpGet("{id}")]
    public Task<OTPDto> GetAsync(int id)
    {
        return _otpAppService.GetAsync(id);
    }

    [HttpPost]
    public Task<OTPDto> CreateAsync(CreateUpdateOTPDto input)
    {
        return _otpAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public Task<OTPDto> UpdateAsync(int id, CreateUpdateOTPDto input)
    {
        return _otpAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(int id)
    {
        return _otpAppService.DeleteAsync(id);
    }
}