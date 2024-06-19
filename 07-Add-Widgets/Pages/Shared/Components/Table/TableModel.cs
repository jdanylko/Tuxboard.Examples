using Tuxboard.Core.Infrastructure.Models;

namespace Add_Widgets.Pages.Shared.Components.Table;

public class TableModel : WidgetModel
{
    public List<Product> Products { get; set; } = new();
}