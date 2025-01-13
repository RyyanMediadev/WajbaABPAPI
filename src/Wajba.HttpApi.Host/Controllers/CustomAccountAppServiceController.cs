global using Wajba.Dtos.BranchContract;
global using Wajba.BranchService;

namespace Wajba.Controllers;

public class CustomAccountAppServiceController : AccountAppService
{
    private readonly BranchAppService _branchAppService;

    public CustomAccountAppServiceController(BranchAppService branchAppService)
    {
        _branchAppService = branchAppService;
    }

   
}