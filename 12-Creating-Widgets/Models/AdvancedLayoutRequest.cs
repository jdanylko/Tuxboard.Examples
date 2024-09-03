namespace CreatingWidgets.Models;

public class AdvancedLayoutRequest
{
    public Guid TabId { get; set; }
    public List<AdvancedLayoutItem> LayoutList { get; set; } = new();
}