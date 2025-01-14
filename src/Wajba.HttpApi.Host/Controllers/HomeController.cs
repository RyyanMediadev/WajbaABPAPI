global using Volo.Abp.AspNetCore.Mvc;

namespace Wajba.Controllers;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
