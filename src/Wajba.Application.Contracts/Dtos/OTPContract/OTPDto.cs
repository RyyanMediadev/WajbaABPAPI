namespace Wajba.Dtos.OTPContract;

public class OTPDto : EntityDto<int>
{
    public OTPType Type { get; set; }
    public int DigitLimit { get; set; }
    public int ExpiryTimeInMinutes { get; set; }
}
