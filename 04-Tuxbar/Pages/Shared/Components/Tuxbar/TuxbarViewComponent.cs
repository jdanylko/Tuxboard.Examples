using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace Tuxbar.Pages.Shared.Components.Tuxbar;

[ViewComponent(Name = "tuxbar")]
public class TuxbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Dashboard model)
    {
        return View(model);
    }
}
