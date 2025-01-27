global using Volo.Abp.Application.Dtos;

namespace Wajba.Dtos.WajbaUserBranchContract;

public class WajbaUserBranchDto : EntityDto<int>
{
    public int WajbaUserId { get; set; }
    public int BranchId { get; set; }
}
