global using Wajba.PopularItemServices;

namespace Wajba.Controllers;

public class PopularItemController : WajbaController
{
    private readonly PopularItemAppservice _popularItemAppservice;

    public PopularItemController(PopularItemAppservice popularItemAppservice)
    {
        _popularItemAppservice = popularItemAppservice;
    }
}
