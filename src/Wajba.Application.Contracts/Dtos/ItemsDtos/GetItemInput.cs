namespace Wajba.Dtos.ItemsDtos;

public class GetItemInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public int? CategoryId { get; set; }
    public int? ItemType { get; set; }
    public bool? IsFeatured { get; set; }
    public bool? IsDeleted { get; set; }
    public int? Status { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinTaxValue { get; set; }
    public decimal? MaxTaxValue { get; set; }
    public int? BranchId { get; set; }
    public int? ItemId { get; set; }
    
}