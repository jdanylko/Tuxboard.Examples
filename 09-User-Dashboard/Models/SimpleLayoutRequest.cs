namespace _09_User_Dashboard.Models;

public class SimpleLayoutRequest
{
    public Guid LayoutRowId { get; set; }
    public int LayoutTypeId { get; set; }

    public SimpleLayoutRequest(Guid layoutRowId, int layoutTypeId)
    {
        LayoutRowId = layoutRowId;
        LayoutTypeId = layoutTypeId;
    }
}