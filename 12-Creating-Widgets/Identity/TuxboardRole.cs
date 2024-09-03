using Microsoft.AspNetCore.Identity;

namespace CreatingWidgets.Identity;

public class TuxboardRole : IdentityRole<Guid>
{
    public virtual ICollection<TuxboardUserRole> UserRoles { get; set; } = default!;
    public virtual ICollection<TuxboardRoleClaim> RoleClaims { get; set; } = default!;
}