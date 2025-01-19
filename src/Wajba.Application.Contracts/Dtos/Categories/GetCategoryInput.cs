namespace Wajba.Dtos.Categories;

public class GetCategoryInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
    public int? BranchId { get; set; }
}
