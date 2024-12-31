global using Volo.Abp.Application.Services;

namespace Wajba.Dtos.BranchContract;

public interface IBranchAppService : ICrudAppService<
BranchDto,
int,
PagedAndSortedResultRequestDto,
CreateBranchDto>
{
}
