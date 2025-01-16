
namespace Wajba.Dtos.OTPContract;

public class OTPDto : EntityDto<int>
{
    public int Type { get; set; }
    public int DigitLimit { get; set; }
    public int ExpiryTimeInMinutes { get; set; }

    public static implicit operator PagedResultDto<object>(OTPDto v)
    {
        throw new NotImplementedException();
    }
}
