using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual DashboardRole Role { get; set; } = default!;
}