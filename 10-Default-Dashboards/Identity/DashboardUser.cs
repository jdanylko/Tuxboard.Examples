using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUser : IdentityUser<Guid>
{
    public virtual ICollection<DashboardUserClaim> Claims { get; set; } = default!;
    public virtual ICollection<DashboardUserLogin> Logins { get; set; } = default!;
    public virtual ICollection<DashboardUserToken> Tokens { get; set; } = default!;
    public virtual ICollection<DashboardUserRole> UserRoles { get; set; } = default!;
}