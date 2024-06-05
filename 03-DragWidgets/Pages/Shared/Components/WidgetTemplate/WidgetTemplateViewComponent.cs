﻿using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace DragWidgets.Web.Pages.Shared.Components.WidgetTemplate;

[ViewComponent(Name = "widgettemplate")]
public class WidgetTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement)
    {
        return View(placement);
    }
}