using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace _09_User_Dashboard.Pages.Shared.Components.AdvancedLayoutRow;

[ViewComponent(Name = "advancedlayoutrow")]
public class AdvancedLayoutRowViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(LayoutRow model)
    {
        return View(model);
    }
}
