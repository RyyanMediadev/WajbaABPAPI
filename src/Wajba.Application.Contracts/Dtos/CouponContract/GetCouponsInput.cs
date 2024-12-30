namespace Wajba.Dtos.CouponContract;

public class GetCouponsInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }


}
