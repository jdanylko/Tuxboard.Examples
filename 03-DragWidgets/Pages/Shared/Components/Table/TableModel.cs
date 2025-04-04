﻿using Tuxboard.Core.Infrastructure.Models;

namespace DragWidgets.Web.Pages.Shared.Components.Table;

public class TableModel : WidgetModel
{
    public List<Product> Products { get; set; } = new();
}