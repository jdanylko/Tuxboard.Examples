using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Dto;

namespace AsyncWidgets.Pages.Shared.Components.WidgetDialog;

[ViewComponent(Name="WidgetDialog")]
public class WidgetDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<WidgetDto> widgets)
    {
        return View(widgets);
    }
}