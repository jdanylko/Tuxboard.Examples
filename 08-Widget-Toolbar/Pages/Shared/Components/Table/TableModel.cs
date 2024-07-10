using Tuxboard.Core.Infrastructure.Models;

namespace WidgetToolbar.Pages.Shared.Components.Table;

public class TableModel : WidgetModel
{
    public List<Product> Products { get; set; } = new();
}