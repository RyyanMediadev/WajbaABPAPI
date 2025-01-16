namespace Wajba.Dtos.OTPContract;

public class UpdateOtpDto
{
    //public int Id { get; set; }
    public int Type { get; set; }
    public int DigitLimit { get; set; }
    public int ExpiryTimeInMinutes { get; set; }
}
