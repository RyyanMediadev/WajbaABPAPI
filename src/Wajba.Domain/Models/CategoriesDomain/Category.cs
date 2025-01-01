namespace Wajba.Models.CategoriesDomain;

public class Category : FullAuditedEntity<int>
{

    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }

    // Navigation properties
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    public virtual ICollection<OfferCategory> OfferCategories { get; set; } = new List<OfferCategory>();

    public Category()
    {

    }

}
