using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Dto;

namespace CreatingWidgets.Pages.Shared.Components.WidgetDialog;

[ViewComponent(Name="WidgetDialog")]
public class WidgetDialogViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<WidgetDto> widgets)
    {
        return View(widgets);
    }
}