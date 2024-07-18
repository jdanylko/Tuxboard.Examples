using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.Identity.Client;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Interfaces;
using Tuxboard.Core.Infrastructure.Models;
using Models;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Tuxboard.Core.Data.Context;
using Extensions;

namespace Layout_1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDashboardService _service;
    private readonly TuxboardConfig _config;

    public Dashboard Dashboard { get; set; } = null!;

    public IndexModel(
        ILogger<IndexModel> logger,
        IDashboardService service,
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

    public async Task<IActionResult> OnPostSaveWidgetPosition([FromBody] PlacementParameter model)
    {
        var placement = await _service.SaveWidgetPlacementAsync(model);

        if (placement == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                $"Widget Placement (id:{model.PlacementId}) was NOT saved.");
        }

        return new OkObjectResult("Widget Placement was saved.");
    }

    public async Task<IActionResult> OnPostRefresh()
    {
        var dashboard = await _service.GetDashboardAsync(_config);

        return ViewComponent("tuxboardtemplate", dashboard);
    }

    /* Dialogs */
    public async Task<IActionResult> OnPostSimpleLayoutDialog()
    {
        var dashboard = await _service.GetDashboardAsync(_config);
        var layouts = dashboard.GetCurrentTab().GetLayouts().FirstOrDefault();
        var currentLayout = layouts?.LayoutRows.FirstOrDefault();

        var layoutTypes = await _service.GetLayoutTypesAsync();
        var result = layoutTypes.Select(e => e.ToDto(currentLayout.LayoutTypeId)).ToList();

        return ViewComponent("simplelayoutdialog", result);
    }

    public async Task<IActionResult> OnPostSaveSimpleLayoutAsync([FromBody] SimpleLayoutRequest request)
    {
        var dashboard = await _service.GetDashboardAsync(_config);

        // Since we only have a single layout for this example, we can grab the first one.
        var tab = dashboard.GetCurrentTab();
        var layouts = tab.GetLayouts().FirstOrDefault();
        
        // LayoutRows have a LayoutType. If we change the LayoutType, the LayoutRow will update on next reload.
        var currentLayoutRow = layouts?.LayoutRows?.FirstOrDefault();
        if (currentLayoutRow != null)
        {
            await _service.ChangeLayoutRowToAsync(currentLayoutRow, request.LayoutTypeId);
        }

        // Refresh
        dashboard = await _service.GetDashboardAsync(_config);

        return ViewComponent("tuxboardtemplate", dashboard);
    }
}