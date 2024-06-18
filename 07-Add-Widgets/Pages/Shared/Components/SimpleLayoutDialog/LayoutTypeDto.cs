namespace Add_Widgets.Pages.Shared.Components.SimpleLayoutDialog;

public record struct LayoutTypeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Layout { get; set; }
    public bool Selected { get; set; }
}