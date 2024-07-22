using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Data.Context;

namespace DefaultDashboards.Context;

public interface IRoleTuxboardContext : ITuxDbContext
{
    DbSet<RoleDashboard> RoleDashboard { get; set; }
}