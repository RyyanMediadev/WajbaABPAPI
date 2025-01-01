namespace Wajba.Models.ItemExtraDomain;

public class ItemExtra : FullAuditedEntity<int>
{
    public ItemExtra()
    {

    }
    public string Name { get; set; }
    public Status Status { get; set; }
    public decimal AdditionalPrice { get; set; }

    // Foreign key to Item
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
}
