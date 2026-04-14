using CreatingWidgets.Data.Context;
using CreatingWidgets.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Services;

public class WidgetRoleService(
    ITuxboardRoleDbContext context,
    UserManager<TuxboardUser> userManager,
    RoleManager<TuxboardRole> roleManager)
    : IWidgetRoleService
{
    public async Task<List<Widget>> GetWidgetsByRoleAsync(TuxboardUser user)
    {
        // Give them something at least.
        var result = await GetDefaultWidgetsAsync();

        var roleName = await GetRoles(user);
        if (string.IsNullOrEmpty(roleName))
        {
            return result;
        }

        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
            return result;

        return await context.WidgetRoles
            .Include(e=> e.Widget)
            .Where(e => e.RoleId == role.Id)
            .Select(r=> r.Widget)
            .ToListAsync();
    }

    public async Task<List<Widget>> GetDefaultWidgetsAsync() =>
        // Set up your own GroupName like "Standard" or something.
        await context.Widgets
            .Where(e => e.GroupName == "Example") 
            .ToListAsync();

    private async Task<string> GetRoles(TuxboardUser user)
    {
        // *COULD* have more than one role; we just want the first one.
        var roles = await userManager.GetRolesAsync(user);
        return (roles.Count == 1
            ? roles.FirstOrDefault()
            : string.Empty)!;
    }
}