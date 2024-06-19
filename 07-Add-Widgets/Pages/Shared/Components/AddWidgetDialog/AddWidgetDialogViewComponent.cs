using Microsoft.AspNetCore.Mvc;

namespace Add_Widgets.Pages.Shared.Components.AdvancedLayoutDialog;

[ViewComponent(Name = "addwidgetdialog")]
public class AddWidgetDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AddWidgetModel model)
    {
        return View(model);
    }
}