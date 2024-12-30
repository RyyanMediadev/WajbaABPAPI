global using System;
global using Wajba.Enums;
global using Wajba.Models.BranchDomain;

namespace Wajba.Models.CouponsDomain;

public class Coupon : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public decimal Code { get; set; }
    public decimal Discount { get; set; }
    public DiscountType DiscountType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public decimal MaximumDiscount { get; set; }
    public int LimitPerUser { get; set; }
    public int CountOfUsers { get; set; } = 0;
    public string ImageUrl { get; set; }
    public string Description { get; set; }
  //  public int BranchId { get; set; }
    public bool IsExpired { get; set; } = false;
    // public virtual Branch Branch { get; set; }

    public Coupon()
    {
        
    }
}
