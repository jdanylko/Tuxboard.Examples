using Microsoft.AspNetCore.Mvc;

namespace _09_User_Dashboard.Pages.Shared.Components.AdvancedLayoutDialog;

[ViewComponent(Name = "advancedlayoutdialog")]
public class AdvancedLayoutDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AdvancedLayoutModel model)
    {
        return View(model);
    }
}