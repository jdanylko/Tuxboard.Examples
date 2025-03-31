using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Pages.Shared.Components.TuxboardTemplate;

[ViewComponent(Name = "tuxboardtemplate")]
public class TuxboardTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Dashboard<int> model)
    {
        return model == null
            ? Content(string.Empty)
            : View(model);
    }
}
