using DefaultWidgets.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultWidgets.Identity;

public class TuxboardRoleStore: RoleStore<TuxboardRole, TuxboardRoleDbContext, Guid, TuxboardUserRole, TuxboardRoleClaim>
{
    public TuxboardRoleStore(TuxboardRoleDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
