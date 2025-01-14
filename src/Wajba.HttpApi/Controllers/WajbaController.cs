global using Wajba.Localization;
global using Volo.Abp.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc;

namespace Wajba.Controllers;

//[Route("api/[controller]")]
//[ApiController]
public abstract class WajbaController : AbpControllerBase
{

    protected WajbaController()
    {
        LocalizationResource = typeof(WajbaResource);
    }
}
