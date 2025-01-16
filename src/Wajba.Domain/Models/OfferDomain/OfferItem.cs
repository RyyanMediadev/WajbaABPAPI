namespace Wajba.Models.OfferDomain;

public class OfferItem : FullAuditedEntity<int>
{
    public int OfferId { get; set; } // Foreign Key
    public int ItemId { get; set; }  // Foreign Key

    // Navigation Properties
    public virtual Offer Offer { get; set; }
    public virtual Item Item { get; set; }

    public OfferItem()
    {

    }
}
