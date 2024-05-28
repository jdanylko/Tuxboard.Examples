using Microsoft.AspNetCore.Mvc;

namespace Tuxbar.Web.Pages.Shared.Components.Tuxbar;

[ViewComponent(Name="tuxbar")]
public class TuxbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
