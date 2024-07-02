using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace WidgetToolbar.Pages.Shared.Components.AdvancedLayoutDialog;

[ViewComponent(Name = "advancedlayoutrow")]
public class AdvancedLayoutRowViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(LayoutRow model)
    {
        return View(model);
    }
}
