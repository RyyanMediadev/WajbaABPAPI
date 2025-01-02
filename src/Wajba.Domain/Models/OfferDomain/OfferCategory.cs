namespace Wajba.Models.OfferDomain;

public class OfferCategory : FullAuditedEntity<int>
{
    public int OfferId { get; set; }
    public virtual Offer Offer { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public OfferCategory()
    {

    }
}
