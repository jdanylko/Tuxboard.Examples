using Tuxboard.Core.Infrastructure.Models;

namespace DefaultWidgets.Pages.Shared.Components.GeneralInfo;

public class GeneralInfoModel : WidgetModel
{
    public int Percentage { get; set; }
    public string Icon { get; set; } = string.Empty;
}