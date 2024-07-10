using Tuxboard.Core.Domain.Entities;

namespace WidgetToolbar.Pages.Shared.Components.SimpleLayoutDialog;

public static class LayoutTypeExtension
{
    public static LayoutTypeDto ToDto(this LayoutType type, int defaultValue) =>
        new()
        {
            Id = type.LayoutTypeId,
            Title = type.Title,
            Layout = type.Layout,
            Selected = type.LayoutTypeId.Equals(defaultValue)
        };
}