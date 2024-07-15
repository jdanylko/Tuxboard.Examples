namespace _09_User_Dashboard.Models;

public class WidgetStateRequest
{
    public Guid WidgetPlacementId { get; set; }
    public bool Collapsed { get; set; }
}