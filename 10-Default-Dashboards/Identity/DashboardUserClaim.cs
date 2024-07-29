using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

[Table("AspNetUserClaims")]
public class DashboardUserClaim: IdentityUserClaim<Guid> { }