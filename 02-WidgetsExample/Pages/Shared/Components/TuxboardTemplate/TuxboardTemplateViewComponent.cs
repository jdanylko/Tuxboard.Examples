using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace WidgetsExample.Pages.Shared.Components.TuxboardTemplate;

[ViewComponent(Name="tuxboardtemplate")]
public class TuxboardTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Dashboard<Guid> model)
    {
        return View(model);
    }
}
