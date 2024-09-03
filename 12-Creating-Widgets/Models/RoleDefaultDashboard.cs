using CreatingWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Models;

public class RoleDefaultDashboard
{
    public virtual Guid DefaultDashboardId { get; set; }
    public virtual Guid RoleId { get; set; }

    public virtual DashboardDefault DefaultDashboard  { get; set; } = default!;
    public virtual TuxboardRole Role { get; set; } = default!;
}