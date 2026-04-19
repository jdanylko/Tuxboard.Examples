namespace DefaultDashboards.Models;

public class SimpleLayoutRequest(Guid layoutRowId, int layoutTypeId)
{
    public Guid LayoutRowId { get; set; } = layoutRowId;
    public int LayoutTypeId { get; set; } = layoutTypeId;
}