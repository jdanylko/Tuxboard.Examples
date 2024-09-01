using Microsoft.AspNetCore.Identity;

namespace DefaultWidgets.Identity;

public class TuxboardUserClaim : IdentityUserClaim<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}