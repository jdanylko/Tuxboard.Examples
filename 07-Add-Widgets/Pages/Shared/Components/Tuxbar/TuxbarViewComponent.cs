using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace Add_Widgets.Pages.Shared.Components.Tuxbar;

[ViewComponent(Name = "tuxbar")]
public class TuxbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Dashboard<Guid> model)
    {
        return View(model);
    }
}
