using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Pages.Shared.Components.Tuxbar;

[ViewComponent(Name = "tuxbar")]
public class TuxbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Dashboard model)
    {
        return model == null 
            ? Content(string.Empty) 
            : View(model);
    }
}
