using CreatingWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Services;

public interface IRoleDashboardService
{
    Task<DashboardDefault> GetDashboardTemplateByRoleAsync(TuxboardUser user);
}