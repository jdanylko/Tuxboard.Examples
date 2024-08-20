using Microsoft.AspNetCore.Identity;

namespace DefaultWidgets.Identity;

public class TuxboardUserLogin : IdentityUserLogin<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}