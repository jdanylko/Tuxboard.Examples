using Microsoft.AspNetCore.Mvc;

namespace AsyncWidgets.Pages.Shared.Components.AdvancedLayoutDialog;

[ViewComponent(Name = "advancedlayoutdialog")]
public class AdvancedLayoutDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AdvancedLayoutModel model)
    {
        return View(model);
    }
}