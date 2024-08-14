using DefaultDashboards.Identity;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Models;

public class RoleDefaultDashboard
{
    public virtual Guid DefaultDashboardId { get; set; }
    public virtual Guid RoleId { get; set; }

    public virtual DashboardDefault DefaultDashboard  { get; set; } = default!;
    public virtual DashboardRole Role { get; set; } = default!;
}