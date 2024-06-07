namespace Layout_1.Pages.Shared.Components.SimpleLayoutDialog;

public record struct LayoutTypeDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Layout { get; set; }
    public bool Selected { get; set; }
}