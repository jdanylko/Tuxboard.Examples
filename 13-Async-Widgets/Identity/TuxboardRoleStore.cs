using AsyncWidgets.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AsyncWidgets.Identity;

public class TuxboardRoleStore: RoleStore<TuxboardRole, TuxboardRoleDbContext, Guid, TuxboardUserRole, TuxboardRoleClaim>
{
    public TuxboardRoleStore(TuxboardRoleDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
