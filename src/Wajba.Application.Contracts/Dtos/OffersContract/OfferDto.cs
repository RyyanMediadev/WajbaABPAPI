namespace Wajba.Dtos.OffersContract;

public class OfferDto : EntityDto<int>
{
    public string Name { get; set; }
    public int Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Image { get; set; }
    public decimal DiscountPercentage { get; set; }
    public int DiscountType { get; set; }
    public string Description { get; set; }
    public int BranchId { get; set; }
}
