namespace Wajba.Dtos.PopularItemstoday;

public class Popularitemdto : EntityDto<int>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal PrePrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public int Status { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public List<int> BranchId { get; set; } = new List<int>();
    public int ItemId { get; set; }

}