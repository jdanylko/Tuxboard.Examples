using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace DefaultWidgets.Pages.Shared.Components.LayoutTemplate;

[ViewComponent(Name = "layouttemplate")]
public class LayoutTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Layout layout)
    {
        return View(layout);
    }
}
