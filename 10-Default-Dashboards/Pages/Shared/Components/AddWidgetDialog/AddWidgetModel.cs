using Tuxboard.Core.Domain.Dto;

namespace DefaultDashboards.Pages.Shared.Components.AddWidgetDialog;

public class AddWidgetModel
{
    public List<WidgetDto> Widgets { get; set; } = new();
}