using Microsoft.AspNetCore.Identity;

namespace AsyncWidgets.Identity;

public class TuxboardUser : IdentityUser<Guid>
{
    public virtual ICollection<TuxboardUserClaim> Claims { get; set; } = new HashSet<TuxboardUserClaim>();
    public virtual ICollection<TuxboardUserLogin> Logins { get; set; } = default!;
    public virtual ICollection<TuxboardUserToken> Tokens { get; set; } = default!;
    public virtual ICollection<TuxboardUserRole> UserRoles { get; set; } = default!;
}