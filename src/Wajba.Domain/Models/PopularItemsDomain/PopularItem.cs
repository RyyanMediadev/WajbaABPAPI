
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
    [ForeignKey(nameof(Branch))]
    public int BranchId { get; set; }
    public Branch Branch { get; set; }
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