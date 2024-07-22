namespace DefaultDashboards.Models;

public class CanDeleteResponse(Guid layoutRowId, string message)
{
    public Guid LayoutRowId { get; } = layoutRowId;
    public string Message { get; } = message;
}