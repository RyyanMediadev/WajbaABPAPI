using Microsoft.AspNetCore.Mvc;

namespace Wajba.Dtos.OffersContract;

public class GetOfferInput : PagedAndSortedResultRequestDto
{
    public string? name { get; set; }
    public int? status { get; set; }
    public DateTime? startDate { get; set; }
    public DateTime? endDate { get; set; }
}
