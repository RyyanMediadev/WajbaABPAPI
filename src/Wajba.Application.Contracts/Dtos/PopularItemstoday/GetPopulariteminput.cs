namespace Wajba.Dtos.PopularItemstoday;

public class GetPopulariteminput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
    public int? status { get; set; }
    public string? Description { get; set; }
    public int? Currentprice { get; set; }
    public int? prePrice { get; set; }
    public int? branchid { get; set; }
    public string? createdAtStart { get; set; }
    public string? createdAtEnd { get; set; }
}