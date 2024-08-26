using DefaultWidgets.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DefaultWidgets.Pages.Shared.Components.SimpleLayoutDialog;

[ViewComponent(Name = "simplelayoutdialog")]
public class SimpleLayoutDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<LayoutTypeDto> layouts)
    {
        return View(new SimpleLayoutModel{Layouts = layouts});
    }
}