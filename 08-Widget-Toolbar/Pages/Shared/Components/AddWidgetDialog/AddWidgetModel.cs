using Tuxboard.Core.Domain.Dto;

namespace WidgetToolbar.Pages.Shared.Components.AdvancedLayoutDialog;

public class AddWidgetModel
{
    public List<WidgetDto> Widgets { get; set; } = new();
}