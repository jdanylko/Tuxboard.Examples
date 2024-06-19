using Tuxboard.Core.Domain.Dto;

namespace Add_Widgets.Pages.Shared.Components.AdvancedLayoutDialog;

public class AddWidgetModel
{
    public List<WidgetDto> Widgets { get; set; } = new();
}