namespace Wajba.Dtos.CouponContract;

public class CreateUpdateCouponDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Code { get; set; }
    [Required]
    public decimal Discount { get; set; }
    [Required]
    public DiscountType DiscountType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public decimal MinimumOrderAmount { get; set; }
    [Required]
    public decimal MaximumDiscount { get; set; }
    [Required]
    public int LimitPerUser { get; set; }
    public string? Description { get; set; }
    [Required]
  //  public int BranchId { get; set; }
    public IFormFile? Image { get; set; }
}
