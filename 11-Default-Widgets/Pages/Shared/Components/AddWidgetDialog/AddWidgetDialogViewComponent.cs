using Microsoft.AspNetCore.Mvc;

namespace DefaultWidgets.Pages.Shared.Components.AddWidgetDialog;

[ViewComponent(Name = "addwidgetdialog")]
public class AddWidgetDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AddWidgetModel model)
    {
        return View(model);
    }
}