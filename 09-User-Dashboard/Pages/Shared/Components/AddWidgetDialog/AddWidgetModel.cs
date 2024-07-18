using Tuxboard.Core.Domain.Dto;

namespace _09_User_Dashboard.Pages.Shared.Components.AddWidgetDialog;

public class AddWidgetModel
{
    public List<WidgetDto> Widgets { get; set; } = new();
}