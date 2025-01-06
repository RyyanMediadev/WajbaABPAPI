namespace Wajba.Dtos.OTPContract;

public class GetOtpInput:PagedAndSortedResultRequestDto
{
    public string PhoneNumber { get; set; }
}
