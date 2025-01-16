namespace Wajba.Dtos.OTPContract;

public class CreateUpdateOTPDto
{
    public int Type { get; set; }
    public int DigitLimit { get; set; }
    public int ExpiryTimeInMinutes { get; set; }
}
