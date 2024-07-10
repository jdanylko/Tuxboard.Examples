using Microsoft.AspNetCore.Mvc;

namespace WidgetToolbar.Pages.Shared.Components.AdvancedLayoutDialog;

[ViewComponent(Name = "addwidgetdialog")]
public class AddWidgetDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AddWidgetModel model)
    {
        return View(model);
    }
}