global using Wajba.Dtos.CompanyContact;
global using Wajba.Models.CompanyDomain;

namespace Wajba.CompanyService;

[RemoteService(false)]
public class CompanyAppService : ApplicationService
{
    private readonly IRepository<Company, int> _repository;
    public CompanyAppService(IRepository<Company, int> repository)
    {
        _repository = repository;
    }
    public async Task<CompanyDto> CreateAsync(CreateUpdateComanyDto input)
    {
        Company company = new Company()
        {
            Address = input.Address,
            City = input.City,
            CountryCode = input.CountryCode,
            Email = input.Email,
            Phone = input.Phone,
            Name = input.Name,
            State = input.State,
            ZipCode = input.ZipCode,
            WebsiteURL = input.WebsiteURL,
        };
        Company company1 = await _repository.InsertAsync(company, true);
        return ObjectMapper.Map<Company, CompanyDto>(company1);
    }
    public async Task<CompanyDto> UpdateAsync(CreateUpdateComanyDto input)
    {
        Company company = await _repository.FirstOrDefaultAsync();
        if (company == null)
            throw new Exception("Not Found");
        company.Address = input.Address;
        company.City = input.City;
        company.CountryCode = input.CountryCode;
        company.Email = input.Email;
        company.Phone = input.Phone;
        company.Name = input.Name;
        company.State = input.State;
        company.ZipCode = input.ZipCode;
        company.WebsiteURL = input.WebsiteURL;
        company.LastModificationTime = DateTime.UtcNow;
        Company company1 = await _repository.UpdateAsync(company, true);
        return ObjectMapper.Map<Company, CompanyDto>(company1);
    }
    public async Task<CompanyDto> GetByIdAsync()
    {
        Company company = await _repository.FirstOrDefaultAsync();
        if (company == null)
            throw new Exception("Not Found");
        return ObjectMapper.Map<Company, CompanyDto>(company);
    }
    /*
    public async Task<PagedResultDto<CompanyDto>> GetListAsync(GetComanyInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var items = await AsyncExecuter.ToListAsync(
            queryable
                .OrderBy(input.Sorting ?? nameof(Company.Name))
                .PageBy(input.SkipCount, input.MaxResultCount)
        );
        return new PagedResultDto<CompanyDto>(
            totalCount,
            ObjectMapper.Map<List<Company>, List<CompanyDto>>(items)
        );

    }*/
    public async Task DeleteAsync(int id)
    {
        if (await _repository.FindAsync(id) == null)
            throw new Exception("Not Found");
        await _repository.DeleteAsync(id);
    }
}