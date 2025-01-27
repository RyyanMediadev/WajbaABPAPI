using Wajba.Dtos.CustomerContract;
using Wajba.Dtos.WajbaUserBranchContract;
using Wajba.Dtos.WajbaUsersContract;
using Wajba.Models.WajbaUserDomain;

namespace Wajba.Mapping;

public class WajbaUserBranchMapping : Profile
{
    public WajbaUserBranchMapping()
    {
        CreateMap<WajbaUserBranch, WajbaUserBranchCreateDto>();
        CreateMap<WajbaUserBranch, WajbaUserBranchDto>();
      


    }
}
