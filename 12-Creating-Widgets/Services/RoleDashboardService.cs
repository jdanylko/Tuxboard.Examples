using CreatingWidgets.Data.Context;
using CreatingWidgets.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Data.Extensions;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Services;

public class RoleDashboardService(
    ITuxboardRoleDbContext context,
    UserManager<TuxboardUser> userManager,
    RoleManager<TuxboardRole> roleManager)
    : IRoleDashboardService
{
    public async Task<bool> DashboardExistsForAsync(Guid userId) => 
        await context.DashboardExistsForAsync(userId);

    public async Task<DashboardDefault> GetDashboardTemplateByRoleAsync(TuxboardUser user)
    {
        DashboardDefault defaultDashboard = null!;

        var roleName = await GetRoles(user);
        if (string.IsNullOrEmpty(roleName))
        {
            defaultDashboard = await context.GetDashboardTemplateForAsync();
        }

        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
            return defaultDashboard ?? await context.GetDashboardTemplateForAsync();

        var roleDashboard = await context.RoleDefaultDashboards
            .FirstOrDefaultAsync(e => e.RoleId == role.Id);
        if (roleDashboard != null)
        {
            defaultDashboard =
                (await context.GetDashboardDefaultAsync(roleDashboard.DefaultDashboardId))
                ?? null!;
        }

        return defaultDashboard ?? await context.GetDashboardTemplateForAsync();
    }

    private async Task<string> GetRoles(TuxboardUser user)
    {
        // *COULD* have more than one role; we just want the first one.
        var roles = await userManager.GetRolesAsync(user);
        return (roles.Count == 1
            ? roles.FirstOrDefault()
            : string.Empty)!;
    }
}