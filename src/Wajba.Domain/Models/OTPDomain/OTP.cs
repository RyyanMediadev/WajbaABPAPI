namespace Wajba.Models.OTPDomain;

public class OTP : FullAuditedEntity<int> 
{
    public OTPType Type { get; set; }
    public int DigitLimit { get; set; }
    public int ExpiryTimeInMinutes { get; set; }

    public OTP()
    {

    }
}
