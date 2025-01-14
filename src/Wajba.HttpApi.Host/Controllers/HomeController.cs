global using Volo.Abp.AspNetCore.Mvc;

namespace Wajba.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController : AbpController
{
    [HttpGet]
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
