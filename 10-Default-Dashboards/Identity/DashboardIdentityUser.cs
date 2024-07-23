using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardIdentityUser : IdentityUser<Guid>
{
    public DashboardIdentityUser() => Id = Guid.NewGuid();
    public DashboardIdentityUser(string name) : this() => UserName = name;
}