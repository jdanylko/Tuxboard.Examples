using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Context;

public class RoleDashboard
{
    public Guid DashboardDefaultId { get; set; }
    public string RoleId { get; set; }

    public IdentityRole Role { get; set; } = default!;
    public DashboardDefault DashboardDefault { get; set; } = default!;
}
