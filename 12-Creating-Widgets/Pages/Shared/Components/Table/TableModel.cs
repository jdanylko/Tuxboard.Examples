using Tuxboard.Core.Infrastructure.Models;

namespace CreatingWidgets.Pages.Shared.Components.Table;

public class TableModel : WidgetModel
{
    public List<Product> Products { get; set; } = new();
}