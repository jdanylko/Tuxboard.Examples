using DefaultWidgets.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultWidgets.Identity;

public class TuxboardRoleStore(TuxboardRoleDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<TuxboardRole, TuxboardRoleDbContext, Guid, TuxboardUserRole, TuxboardRoleClaim>(context, describer);
