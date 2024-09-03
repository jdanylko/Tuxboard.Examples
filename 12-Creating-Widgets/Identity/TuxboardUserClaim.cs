using Microsoft.AspNetCore.Identity;

namespace CreatingWidgets.Identity;

public class TuxboardUserClaim : IdentityUserClaim<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}