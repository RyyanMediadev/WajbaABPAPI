global using System.Collections.Generic;
global using Wajba.Models.CategoriesDomain;
global using Wajba.Models.ItemAddonDomain;
global using Wajba.Models.ItemExtraDomain;

namespace Wajba.Models.Items;

public class Item : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public bool IsFeatured { get; set; }
    public Status Status { get; set; }
    public ItemType ItemType { get; set; }
    public string Note { get; set; }
    public string Description { get; set; }
    public decimal? TaxValue { get; set; }

    // Foreign key to Category
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    // Navigation properties
    public virtual ICollection<ItemBranch> ItemBranches { get; set; } = new List<ItemBranch>();
    public virtual ICollection<ItemAddon> ItemAddons { get; set; } = new List<ItemAddon>();
    public ICollection<ItemExtra> ItemExtras { get; set; } = new List<ItemExtra>();
    public ICollection<ItemVariation> ItemVariations { get; set; } = new List<ItemVariation>();
    public virtual ICollection<OfferItem> OfferItems { get; set; } = new HashSet<OfferItem>();
    public Item()
    {
        
    }
    
}