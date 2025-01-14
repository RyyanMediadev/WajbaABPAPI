global using Volo.Abp.AspNetCore.Mvc;

namespace Wajba.Controllers;

public class HomeController : WajbaController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
