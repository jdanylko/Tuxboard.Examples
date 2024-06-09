using Tuxboard.Core.Domain.Entities;

namespace Layout_1.Pages.Shared.Components.SimpleLayoutDialog;

public static class LayoutTypeExtension
{
    public static LayoutTypeDto ToDto(this LayoutType type, string defaultValue) =>
        new()
        {
            Id = type.LayoutTypeId,
            Title = type.Title,
            Layout = type.Layout,
            Selected = type.LayoutTypeId.Equals(defaultValue)
        };
}