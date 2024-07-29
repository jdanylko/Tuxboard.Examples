using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUser : IdentityUser<Guid>
{
    public virtual ICollection<DashboardUserClaim> Claims { get; set; }
    public virtual ICollection<DashboardUserLogin> Logins { get; set; }
    public virtual ICollection<DashboardUserToken> Tokens { get; set; }
    public virtual ICollection<DashboardUserRole> UserRoles { get; set; }
}
