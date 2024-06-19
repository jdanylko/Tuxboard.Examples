using Tuxboard.Core.Domain.Entities;

namespace Add_Widgets.Pages.Shared.Components.AdvancedLayoutDialog;

public class AdvancedLayoutModel
{
    public List<LayoutRow> LayoutRows { get; set; } = new();
    public List<LayoutType> LayoutTypes { get; set; } = new();
}