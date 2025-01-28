
namespace Wajba.Models.PopularItemsDomain;

public class PopularItem : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal PrePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public Status Status { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public ICollection<PopulartItemBranches> PopulartItemBranches { get; set; } = new List<PopulartItemBranches>();
    public Item Item { get; set; }
    [ForeignKey(nameof(Item))]
    public int ItemId { get; set; }

    public PopularItem()
    {

    }

    //public static implicit operator PopularItem(Wajba.Dtos.PopularItemstoday.Popularitemdto v)
    //{
    //    throw new NotImplementedException();
    //}
}