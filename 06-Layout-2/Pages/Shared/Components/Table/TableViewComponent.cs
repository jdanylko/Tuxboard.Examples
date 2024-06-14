using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace Layout_2.Pages.Shared.Components.Table;

[ViewComponent(Name = "table")]
public class TableViewComponent : ViewComponent
{
    // EXTEND: Hook - Can use any Context for your application.
    // private readonly IMyDbContext _context;

    //public TableViewComponent(IMyDbContext context)
    //{
    //    _context = context;
    //}

    public IViewComponentResult Invoke(WidgetPlacement placement)
    {
        var model = new TableModel
        {
            Placement = placement,
            Products =
            [
                new() { Id = 1, Title = "Product1", Price = new decimal(15.00) },
                new() { Id = 2, Title = "Product2", Price = new decimal(45.00) },
                new() { Id = 3, Title = "Product3", Price = new decimal(120.00) }
            ]
        };

        return View("Default", model);
    }
}
