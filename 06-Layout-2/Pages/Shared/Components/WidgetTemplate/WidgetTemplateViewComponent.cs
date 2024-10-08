﻿using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace Layout_2.Pages.Shared.Components.WidgetTemplate;

[ViewComponent(Name = "widgettemplate")]
public class WidgetTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement)
    {
        return View(placement);
    }
}
