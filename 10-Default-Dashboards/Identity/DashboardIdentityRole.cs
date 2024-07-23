using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardIdentityRole : IdentityRole<Guid>
{
    public DashboardIdentityRole() => Id = Guid.NewGuid();
    public DashboardIdentityRole(string name) : this() => Name = name;
}