using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Identity;

public class tbDefaultDashboard : DashboardDefault
{
    public virtual ICollection<DashboardRole> Roles { get; set; } = default!;
}