using Tuxboard.Core.Infrastructure.Models;

namespace Tuxbar.Web.Pages.Shared.Components.GeneralInfo;

public class GeneralInfoModel : WidgetModel
{
    public int Percentage { get; set; }
    public string Icon { get; set; } = string.Empty;
}