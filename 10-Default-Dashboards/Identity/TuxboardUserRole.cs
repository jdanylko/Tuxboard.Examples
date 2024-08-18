using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class TuxboardUserRole : IdentityUserRole<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
    public virtual TuxboardRole Role { get; set; } = default!;
}