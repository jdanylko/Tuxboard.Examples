using System.ComponentModel.DataAnnotations.Schema;
using DefaultDashboards.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tuxboard.Core.Data.Context;
using Tuxboard.Core.Data.Extensions;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Context;

public class DashboardIdentityDbContext(DbContextOptions<DashboardIdentityDbContext> options)
    : IdentityDbContext<DashboardUser, DashboardRole,
        Guid, DashboardUserClaim, DashboardUserRole, DashboardUserLogin,
        DashboardRoleClaim, DashboardUserToken>(options)
{
}

public class RoleDefaultDashboard
{
    public Guid RoleId { get; set; }
    public Guid DefaultDashboardId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public DashboardRole Role { get; set; } = null!;
    [ForeignKey(nameof(DefaultDashboardId))]
    public DashboardDefault DefaultDashboard { get; set; } = null!;
}

public interface ITuxboardRoleDbContext: ITuxDbContext
{
    DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }
}

public class TuxboardRoleDbContext : TuxDbContext, ITuxboardRoleDbContext
{
    public TuxboardRoleDbContext(DbContextOptions<TuxDbContext> options, IConfiguration config) 
        : base(options, config)
    {
    }

    public DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleDefaultDashboardConfiguration());
    }
}

public class RoleDefaultDashboardConfiguration: IEntityTypeConfiguration<RoleDefaultDashboard>
{
    public void Configure(EntityTypeBuilder<RoleDefaultDashboard> builder)
    {
        builder.ToTable("RoleDashboardDefault", "dbo");
        
        builder.HasKey(d => new { d.RoleId, d.DefaultDashboardId });
    }
}

public interface IRoleDashboardService
{
    Task<DashboardDefault> GetDashboardTemplateByRoleAsync(DashboardUser user);
    Task<bool> DashboardExistsForAsync(Guid userId);
}

public class RoleDashboardService(
    ITuxboardRoleDbContext context, 
    DashboardUserManager userManager, 
    DashboardRoleManager roleManager) : IRoleDashboardService
{
    public async Task<bool> DashboardExistsForAsync(Guid userId)
    {
        return await context.DashboardExistsForAsync(userId);
    }

    public async Task<DashboardDefault> GetDashboardTemplateByRoleAsync(DashboardUser user)
    {
        DashboardDefault defaultDashboard = null!;

        var roleName = await GetRoles(user);
        if (string.IsNullOrEmpty(roleName))
        {
            defaultDashboard = await context.GetDashboardTemplateForAsync();
        }

        var role = await roleManager.FindByNameAsync(roleName);
        if (role != null)
        {
            var roleDashboard = await context.RoleDefaultDashboards
                .FirstOrDefaultAsync(e => e.RoleId == role.Id);
            if (roleDashboard != null)
            {
                defaultDashboard = await context.DashboardDefaults
                    .Include(e => e.DashboardDefaultWidgets)
                        .ThenInclude(e => e.Widget)
                    .Include(e => e.Layout)
                        .ThenInclude(f => f.LayoutRows)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.DefaultId == roleDashboard.DefaultDashboardId);
            }
        }

        defaultDashboard ??= await context.GetDashboardTemplateForAsync();

        return defaultDashboard;
    }

    private async Task<string> GetRoles(DashboardUser user)
    {
        // *COULD* have more than one role; we just want the first one.
        return (await userManager.GetRolesAsync(user)).FirstOrDefault();
    }
}
