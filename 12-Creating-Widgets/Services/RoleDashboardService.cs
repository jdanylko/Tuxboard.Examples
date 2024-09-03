using CreatingWidgets.Data.Context;
using CreatingWidgets.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Data.Extensions;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Services;

public class RoleDashboardService : IRoleDashboardService
{
    private readonly ITuxboardRoleDbContext _context;
    private readonly UserManager<TuxboardUser> _userManager;
    private readonly RoleManager<TuxboardRole> _roleManager;

    public RoleDashboardService(ITuxboardRoleDbContext context,
        UserManager<TuxboardUser> userManager,
        RoleManager<TuxboardRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> DashboardExistsForAsync(Guid userId)
    {
        return await _context.DashboardExistsForAsync(userId);
    }

    public async Task<DashboardDefault> GetDashboardTemplateByRoleAsync(TuxboardUser user)
    {
        DashboardDefault defaultDashboard = null!;

        var roleName = await GetRoles(user);
        if (string.IsNullOrEmpty(roleName))
        {
            defaultDashboard = await _context.GetDashboardTemplateForAsync();
        }

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
            return defaultDashboard ?? await _context.GetDashboardTemplateForAsync();

        var roleDashboard = await _context.RoleDefaultDashboards
            .FirstOrDefaultAsync(e => e.RoleId == role.Id);
        if (roleDashboard != null)
        {
            defaultDashboard =
                (await _context.GetDashboardDefaultAsync(roleDashboard.DefaultDashboardId))
                ?? null!;
        }

        return defaultDashboard ?? await _context.GetDashboardTemplateForAsync();
    }

    private async Task<string> GetRoles(TuxboardUser user)
    {
        // *COULD* have more than one role; we just want the first one.
        var roles = await _userManager.GetRolesAsync(user);
        return (roles.Count == 1
            ? roles.FirstOrDefault()
            : string.Empty)!;
    }
}