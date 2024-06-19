using Tuxboard.Core.Domain.Entities;

namespace Models;

public class AddWidgetRequest
{
    public Guid WidgetId { get; set; }
}

public static class LayoutRowExtensions
{
    public static WidgetPlacement CreateFromWidget(this LayoutRow layoutRow, Widget widget) =>
        new()
        {
            WidgetPlacementId = Guid.NewGuid(),
            LayoutRowId = layoutRow.LayoutRowId,
            WidgetId = widget.WidgetId,
            ColumnIndex = 1,
            WidgetIndex = layoutRow.WidgetPlacements != null && layoutRow.WidgetPlacements.Any()
                ? layoutRow.WidgetPlacements.Count + 1
                : 1,
            Collapsed = false,
            UseSettings = true,
            UseTemplate = true
        };
}
