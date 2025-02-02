﻿global using Volo.Abp.Application.Dtos;

namespace Wajba.Dtos.BranchContract;

public class BranchDto : EntityDto<int>
{
    public string Name { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
    //public int CompanyId { get; set; }
    public BranchDto()
    {
     
    }
}
public class GetBranchInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}