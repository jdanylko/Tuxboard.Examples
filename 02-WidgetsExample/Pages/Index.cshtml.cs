using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Services;

namespace WidgetsExample.Pages;

public class IndexModel(
    ILogger<IndexModel> logger,
    IDashboardService<Guid> service,
    IOptions<TuxboardConfig> options)
    : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;
    private readonly TuxboardConfig _config = options.Value;

    public Dashboard<Guid> Dashboard { get; set; } = null!;

    public async Task OnGet()
    {
        Dashboard = await service.GetDashboardAsync(_config);
    }
}