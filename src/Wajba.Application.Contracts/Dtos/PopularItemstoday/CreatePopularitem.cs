namespace Wajba.Dtos.PopularItemstoday;

public class CreatePopularitem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public IFormFile? ImgFile { get; set; }
    public decimal preprice { get; set; }
    public decimal currentprice { get; set; }
    public string Description { get; set; }
    public int BranchId { get; set; }
}
