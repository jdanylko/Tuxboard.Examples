﻿using DefaultDashboards.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DefaultDashboards.Pages.Shared.Components.SimpleLayoutDialog;

[ViewComponent(Name = "simplelayoutdialog")]
public class SimpleLayoutDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<LayoutTypeDto> layouts)
    {
        return View(new SimpleLayoutModel{Layouts = layouts});
    }
}