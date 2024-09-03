using Microsoft.AspNetCore.Identity;

namespace CreatingWidgets.Identity;

public class TuxboardUserLogin : IdentityUserLogin<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}