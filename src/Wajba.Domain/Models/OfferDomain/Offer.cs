using Wajba.Models.BranchDomain;

namespace Wajba.Models.OfferDomain;

public class Offer : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public Status status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ImageUrl { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DiscountType discountType { get; set; }
    public string Description { get; set; }

    // Foreign key to Restaurant
    public int BranchId { get; set; }

    // Navigation property to Restaurant
    public virtual Branch Branch { get; set; }

    // Many-to-many relationship with Item
    public virtual ICollection<OfferItem> OfferItems { get; set; }

    // Many-to-many relationship with Category
    public virtual ICollection<OfferCategory> OfferCategories { get; set; }


    public Offer()
    {

    }
}
