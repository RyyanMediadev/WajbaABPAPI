namespace Wajba.Dtos.PopularItemstoday;

public class Popularitemdto : EntityDto<int>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal PrePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public Status Status { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public int BranchId { get; set; }
    public int ItemId { get; set; }

}