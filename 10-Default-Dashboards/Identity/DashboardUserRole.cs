using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUserRole : IdentityUserRole<Guid>
{
    //[Key]
    //public override Guid RoleId { get; set; }
    //[Key]
    //public override Guid UserId { get; set; }

    public virtual DashboardUser User { get; set; } = default!;
    public virtual DashboardRole Role { get; set; } = default!;
}