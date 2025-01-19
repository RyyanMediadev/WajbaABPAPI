namespace Wajba.Mapping;

public class CompanyMapping:Profile
{
    public CompanyMapping()
    {
        CreateMap<Company, CompanyDto>();
        CreateMap<CreateUpdateComanyDto, Company>();
    }
}
