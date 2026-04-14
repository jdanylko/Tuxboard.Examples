namespace _09_User_Dashboard.Models;

public class SimpleLayoutRequest(Guid layoutRowId, int layoutTypeId)
{
    public Guid LayoutRowId { get; set; } = layoutRowId;
    public int LayoutTypeId { get; set; } = layoutTypeId;
}