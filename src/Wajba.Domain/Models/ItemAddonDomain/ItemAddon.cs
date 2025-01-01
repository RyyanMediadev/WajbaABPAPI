namespace Wajba.Models.ItemAddonDomain;

public class ItemAddon : FullAuditedEntity<int>
{
    public string AddonName { get; set; }
    public decimal AdditionalPrice { get; set; }

    // Foreign key to Item
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }

    public ItemAddon()
    {

    }
}
