using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace _09_User_Dashboard.Pages.Shared.Components.WidgetTemplate;

[ViewComponent(Name = "widgettemplate")]
public class WidgetTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement)
    {
        return View(placement);
    }
}
