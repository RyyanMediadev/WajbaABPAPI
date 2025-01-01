global using Wajba.Models.ItemAttributeDomain;

namespace Wajba.Models.ItemVariationDomain;

public class ItemVariation : FullAuditedEntity<int>
{
    public string Name { get; set; } // e.g., "Single", "Double", "Coke", "Pepsi"
    public string Note { get; set; }
    public Status Status { get; set; }
    public decimal AdditionalPrice { get; set; }

    // Foreign key to ItemAttributes
    public int ItemAttributesId { get; set; }
    public ItemAttribute ItemAttributes { get; set; }

    // Foreign key to Item
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }


    public ItemVariation()
    {

    }
}
