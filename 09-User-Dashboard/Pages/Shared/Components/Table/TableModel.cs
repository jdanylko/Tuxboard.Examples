using Tuxboard.Core.Infrastructure.Models;

namespace _09_User_Dashboard.Pages.Shared.Components.Table;

public class TableModel : WidgetModel
{
    public List<Product> Products { get; set; } = new();
}