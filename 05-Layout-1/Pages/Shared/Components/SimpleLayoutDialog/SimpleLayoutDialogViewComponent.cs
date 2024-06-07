using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace Layout_1.Pages.Shared.Components.SimpleLayoutDialog;

[ViewComponent(Name = "simplelayoutdialog")]
public class SimpleLayoutDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<LayoutType> layouts)
    {
        return View(new SimpleLayoutModel{Layouts = layouts});
    }
}