using _09_User_Dashboard.Dto;
using Microsoft.AspNetCore.Mvc;

namespace _09_User_Dashboard.Pages.Shared.Components.SimpleLayoutDialog;

[ViewComponent(Name = "simplelayoutdialog")]
public class SimpleLayoutDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<LayoutTypeDto> layouts)
    {
        return View(new SimpleLayoutModel{Layouts = layouts});
    }
}