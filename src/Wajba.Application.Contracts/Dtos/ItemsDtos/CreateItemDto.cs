namespace Wajba.Dtos.ItemsDtos;

public class CreateItemDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public Base64ImageModel? model { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public bool IsFeatured { get; set; }
    [Required]
    public int status { get; set; }
    [Required]
    public int ItemType { get; set; }
    [Required]
    public string Note { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal? TaxValue { get; set; }
    [Required]
    public int CategoryId { get; set; }

    public List<int> BranchIds { get; set; } = new List<int>();
}
