global using Wajba.Models.ItemVariationDomain;

namespace Wajba.Models.ItemAttributeDomain;

public class ItemAttribute : FullAuditedEntity<int>
{
    public ItemAttribute()
    {

    }

    public string Name { get; set; } // e.g., "Size", "Drink", "Addons"
    public Status Status { get; set; }

    // Navigation property 
    public ICollection<ItemVariation> ItemVariations { get; set; } = new List<ItemVariation>();
}
