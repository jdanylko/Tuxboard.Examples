using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace _09_User_Dashboard.Pages.Shared.Components.TuxboardTemplate;

[ViewComponent(Name = "tuxboardtemplate")]
public class TuxboardTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Dashboard model)
    {
        return model == null
            ? Content(string.Empty)
            : View(model);
    }
}
