using DefaultWidgets.Data.Context;
using DefaultWidgets.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Domain.Entities;

namespace DefaultWidgets.Services;

public class WidgetRoleService : IWidgetRoleService
{
    private readonly ITuxboardRoleDbContext _context;
    private readonly UserManager<TuxboardUser> _userManager;
    private readonly RoleManager<TuxboardRole> _roleManager;

    public WidgetRoleService(ITuxboardRoleDbContext context,
        UserManager<TuxboardUser> userManager,
        RoleManager<TuxboardRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<Widget>> GetWidgetsByRoleAsync(TuxboardUser user)
    {
        // Give them something at least.
        var result = await GetDefaultWidgetsAsync();

        var roleName = await GetRoles(user);
        if (string.IsNullOrEmpty(roleName))
        {
            return result;
        }

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
            return result;

        return await _context.WidgetRoles
            .Include(e=> e.Widget)
            .Where(e => e.RoleId == role.Id)
            .Select(r=> r.Widget)
            .ToListAsync();
    }

    public async Task<List<Widget>> GetDefaultWidgetsAsync() =>
        // Set up your own GroupName like "Standard" or something.
        await _context.Widgets
            .Where(e => e.GroupName == "Example") 
            .ToListAsync();

    private async Task<string> GetRoles(TuxboardUser user)
    {
        // *COULD* have more than one role; we just want the first one.
        var roles = await _userManager.GetRolesAsync(user);
        return (roles.Count == 1
            ? roles.FirstOrDefault()
            : string.Empty)!;
    }
}