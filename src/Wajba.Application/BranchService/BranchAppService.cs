global using Wajba.Dtos.BranchContract;
global using Wajba.Models.BranchDomain;

namespace Wajba.BranchService;

[RemoteService(false)]
public class BranchAppService : ApplicationService
{
    private readonly IRepository<Branch, int> _branchRepository;
    public BranchAppService(IRepository<Branch, int> branchRepository)
    {
        _branchRepository = branchRepository;
    }
    public async Task<BranchDto> CreateAsync(CreateUpdateBranchDto input)
    {
        Branch branch = new Branch
        {
            Name = input.Name,
            Longitude = input.Longitude,
            Latitude = input.Latitude,
            Email = input.Email,
            Phone = input.Phone,
            City = input.City,
            State = input.State,
            ZipCode = input.ZipCode,
            Address = input.Address,
            Status = input.Status
        };
        var insertedBranch = await _branchRepository.InsertAsync(branch, true);
        return ObjectMapper.Map<Branch, BranchDto>(insertedBranch);
    }

    public async Task<BranchDto> UpdateAsync(int id, CreateUpdateBranchDto input)
    {
        var branch = await _branchRepository.GetAsync(id);
        branch.Name = input.Name;
        branch.Longitude = input.Longitude;
        branch.Latitude = input.Latitude;
        branch.Email = input.Email;
        branch.Phone = input.Phone;
        branch.City = input.City;
        branch.State = input.State;
        branch.ZipCode = input.ZipCode;
        branch.Address = input.Address;
        branch.Status = input.Status;
        branch.LastModificationTime = DateTime.UtcNow;

        var updatedBranch = await _branchRepository.UpdateAsync(branch);
        return ObjectMapper.Map<Branch, BranchDto>(updatedBranch);
    }

    public async Task<BranchDto> GetByIdAsync(int id)
    {
        var branch = await _branchRepository.GetAsync(id);
        return ObjectMapper.Map<Branch, BranchDto>(branch);
    }

    public async Task<PagedResultDto<BranchDto>> GetListAsync(GetBranchInput input)
    {
        var queryable = await _branchRepository.GetQueryableAsync();

        queryable = queryable.WhereIf(
            !string.IsNullOrWhiteSpace(input.Filter),
            b => b.Name.Contains(input.Filter) || b.City.Contains(input.Filter)
        );

        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var items = await AsyncExecuter.ToListAsync(queryable
            .OrderBy(input.Sorting ?? nameof(Branch.Name))
            .PageBy(input.SkipCount, input.MaxResultCount));

        return new PagedResultDto<BranchDto>(
            totalCount,
            ObjectMapper.Map<List<Branch>, List<BranchDto>>(items)
        );
    }

    public async Task DeleteAsync(int id)
    {
        await _branchRepository.DeleteAsync(id);
    }
}
