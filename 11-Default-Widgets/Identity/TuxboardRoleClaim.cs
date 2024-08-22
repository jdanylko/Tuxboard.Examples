using Microsoft.AspNetCore.Identity;

namespace DefaultWidgets.Identity;

public class TuxboardRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual TuxboardRole Role { get; set; } = default!;
}