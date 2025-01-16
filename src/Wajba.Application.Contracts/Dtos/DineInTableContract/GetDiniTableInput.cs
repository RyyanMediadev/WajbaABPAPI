namespace Wajba.Dtos.DineInTableContract;

public class GetDiniTableInput: PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
    public int? Size { get; set; }
    public string? Status {  get; set; }
    public int? BranchId { get; set; }
}
