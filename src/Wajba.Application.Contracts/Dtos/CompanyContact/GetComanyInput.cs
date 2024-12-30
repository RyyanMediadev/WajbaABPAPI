namespace Wajba.Dtos.CompanyContact;

public class GetComanyInput: PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
