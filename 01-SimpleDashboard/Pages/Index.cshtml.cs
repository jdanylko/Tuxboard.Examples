using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Services;

namespace SimpleDashboard.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDashboardService<Guid> _service;
    private readonly TuxboardConfig _config;

    public Dashboard<Guid> Dashboard { get; set; } = null!;

    public IndexModel(
        ILogger<IndexModel> logger,
        IDashboardService<Guid> service,
        IOptions<TuxboardConfig> options)
    {
        _logger = logger;
        _service = service;
        _config = options.Value;
    }

    public async Task OnGet()
    {
        Dashboard = await _service.GetDashboardAsync(_config);
    }
}