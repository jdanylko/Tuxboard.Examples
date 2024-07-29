using System.ComponentModel.DataAnnotations.Schema;
using DefaultDashboards.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tuxboard.Core.Data.Context;
using Tuxboard.Core.Data.Extensions;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Data.Context;

public class DashboardIdentityDbContext(DbContextOptions<DashboardIdentityDbContext> options) 
    : IdentityDbContext<DashboardUser, DashboardRole, Guid,
    IdentityUserClaim<Guid>, IdentityUserRole<Guid>, DashboardUserLogin,
    DashboardRoleClaim, DashboardUserToken>(options)
{
    public virtual DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AspNetUserClaims").HasKey(x => x.Id);

        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AspNetUserRoles")
            .HasKey(x => new { x.UserId, x.RoleId });

        modelBuilder.Entity<DashboardUser>(b =>
        {
            // Each User can have many UserClaims
            b.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            b.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            b.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<DashboardRole>(b =>
        {
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        });
    }
}




public class RoleDefaultDashboard
{
    public Guid RoleId { get; set; }
    public Guid DefaultDashboardId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public IdentityRole<Guid> Role { get; set; } = null!;
    [ForeignKey(nameof(DefaultDashboardId))]
    public DashboardDefault DefaultDashboard { get; set; } = null!;
}

public interface ITuxboardRoleDbContext : ITuxDbContext
{
    DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }
}

public class RoleDefaultDashboardConfiguration : IEntityTypeConfiguration<RoleDefaultDashboard>
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
    UserManager<DashboardUser> userManager,
    RoleManager<DashboardRole> roleManager) : IRoleDashboardService
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
                defaultDashboard = (await context.DashboardDefaults
                    .Include(e => e.DashboardDefaultWidgets)
                        .ThenInclude(e => e.Widget)
                    .Include(e => e.Layout)
                        .ThenInclude(f => f.LayoutRows)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.DefaultId == roleDashboard.DefaultDashboardId) ?? null)!;
            }
        }

        defaultDashboard ??= await context.GetDashboardTemplateForAsync();

        return defaultDashboard;
    }

    private async Task<string> GetRoles(DashboardUser user)
    {
        // *COULD* have more than one role; we just want the first one.
        var roles = await userManager.GetRolesAsync(user);
        return (roles.Count == 1
            ? roles.FirstOrDefault()
            : string.Empty)!;
    }
}
