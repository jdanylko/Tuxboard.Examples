using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Pages.Shared.Components.GeneralInfo;

[ViewComponent(Name = "generalinfo")]
public class GeneralInfoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement)
    {
        var infoViewModel = new GeneralInfoModel
        {
            Placement = placement,
            Percentage = 90,
            Icon = "fas fa-cogs fa-5x p-3"
        };

        return View("Default", infoViewModel);
    }
}