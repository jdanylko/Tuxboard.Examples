using CreatingWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Models;

public class WidgetRole
{
    public virtual Guid WidgetId { get; set; }
    public virtual Guid RoleId { get; set; }

    public virtual Widget Widget { get; set; } = default!;
    public virtual TuxboardRole Role { get; set; } = default!;
}