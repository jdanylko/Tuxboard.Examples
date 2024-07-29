using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardRole : IdentityRole<Guid>
{
    public virtual ICollection<DashboardUserRole> UserRoles { get; set; } = default!;
    public virtual ICollection<DashboardRoleClaim> RoleClaims { get; set; } = default!;
}
