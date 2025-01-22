namespace Wajba.Dtos.CouponContract;

public class GetCouponsInput : PagedAndSortedResultRequestDto
{
    public string? name { get; set; }
    public int? branchid { get; set; }
    public decimal? discount { get; set; }
    public int? discountype { get; set; }
    public string? startdate { get; set; }
    public string? enddate { get; set; }
    public decimal? minimumOrderAmount { get; set; }
    public decimal? maximumDiscount { get; set; }
    public int? limitPerUser { get; set; }
    public string? description { get; set; }
    public string? code { get; set; }
    public int? branchId { get; set; }
    public bool? isexpire { get; set; }
    public bool? isused { get; set; }
}