using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace Layout_1.Pages.Shared.Components.Tuxbar;

[ViewComponent(Name = "tuxbar")]
public class TuxbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Dashboard model)
    {
        return View(model);
    }
}
