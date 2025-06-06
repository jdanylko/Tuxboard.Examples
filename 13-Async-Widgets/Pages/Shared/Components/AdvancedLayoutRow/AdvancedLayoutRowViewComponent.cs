using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace AsyncWidgets.Pages.Shared.Components.AdvancedLayoutRow;

[ViewComponent(Name = "advancedlayoutrow")]
public class AdvancedLayoutRowViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(LayoutRow model)
    {
        return View(model);
    }
}
