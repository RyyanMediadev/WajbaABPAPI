global using Wajba.Dtos.BranchContract;
global using Wajba.Models.BranchDomain;
using NUglify.Helpers;
using System;
using Wajba.Dtos.WajbaUserBranchContract;

namespace Wajba.WajbaUserBranchService;

[RemoteService(false)]
public class WajbaUserBranchAppService : ApplicationService
{
    private readonly IRepository<WajbaUserBranch, int> _WajbaUserBranchRepository;

    public WajbaUserBranchAppService(IRepository<WajbaUserBranch, int> WajbaUserBranchRepositry)
    {
        _WajbaUserBranchRepository = WajbaUserBranchRepositry;
    }
    public async Task<WajbaUserBranchCreateDto> CreateAsync(WajbaUserBranchCreateDto input)
    {
        WajbaUserBranch WajbaUserBranch = new WajbaUserBranch
        {
            WajbaUserId = input.WajbaUserId,
            BranchId = input.WajbaUserId,

        };
        var insertedBranch = await _WajbaUserBranchRepository.InsertAsync(WajbaUserBranch, true);
        return ObjectMapper.Map<WajbaUserBranch, WajbaUserBranchCreateDto>(WajbaUserBranch);
    }

    //public async Task<BranchDto> UpdateAsync(int id, UpdateBranchDto input)
    //{
    //    var branch = await _branchRepository.GetAsync(id);
    //    if (branch == null)
    //        throw new EntityNotFoundException(typeof(Branch), id);
    //    branch.Name = input.Name;
    //    branch.Longitude = input.Longitude;
    //    branch.Latitude = input.Latitude;
    //    branch.Email = input.Email;
    //    branch.Phone = input.Phone;
    //    branch.City = input.City;
    //    branch.State = input.State;
    //    branch.ZipCode = input.ZipCode;
    //    branch.Address = input.Address;
    //    branch.Status = input.Status;
    //    branch.LastModificationTime = DateTime.UtcNow;
    //    var updatedBranch = await _branchRepository.UpdateAsync(branch, true);
    //    return ObjectMapper.Map<Branch, BranchDto>(updatedBranch);
    //}

    //public async Task<BranchDto> GetByIdAsync(int id)
    //{
    //    var branch = await _branchRepository.GetAsync(id);
    //    if (branch == null)
    //        throw new EntityNotFoundException(typeof(Branch), id);
    //    return ObjectMapper.Map<Branch, BranchDto>(branch);
    //}

    public async Task<PagedResultDto<WajbaUserBranchDto>> GetListAsync(WajbaUserBranchDto input)
    {
        var items = await _WajbaUserBranchRepository.GetQueryableAsync();


        var Count = await AsyncExecuter.CountAsync(items);


        return new PagedResultDto<WajbaUserBranchDto>(
            Count,
            ObjectMapper.Map<List<WajbaUserBranch>, List<WajbaUserBranchDto>>((List<WajbaUserBranch>)items)
        );
    }

    //public async Task DeleteAsync(int id)
    //{
    //    if (await _branchRepository.FindAsync(id) == null)
    //        throw new EntityNotFoundException(typeof(Branch), id);
    //    await _branchRepository.DeleteAsync(id,true);
    //}
}
