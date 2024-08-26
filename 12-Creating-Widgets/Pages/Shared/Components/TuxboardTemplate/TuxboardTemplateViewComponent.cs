﻿using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace DefaultWidgets.Pages.Shared.Components.TuxboardTemplate;

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
