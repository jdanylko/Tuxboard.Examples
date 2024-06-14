using Tuxboard.Core.Infrastructure.Models;

namespace Layout_2.Pages.Shared.Components.Table;

public class TableModel : WidgetModel
{
    public List<Product> Products { get; set; } = new();
}