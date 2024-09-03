using Microsoft.AspNetCore.Mvc;

namespace CreatingWidgets.Pages.Shared.Components.AdvancedLayoutDialog;

[ViewComponent(Name = "advancedlayoutdialog")]
public class AdvancedLayoutDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AdvancedLayoutModel model)
    {
        return View(model);
    }
}