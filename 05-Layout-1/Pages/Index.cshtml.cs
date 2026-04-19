using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Models;
using Models;
using Extensions;
using Tuxboard.Core.Infrastructure.Services;

namespace Layout_1.Pages;

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

    public async Task<IActionResult> OnPostSaveWidgetPosition([FromBody] PlacementParameter model)
    {
        var placement = await service.SaveWidgetPlacementAsync(model);

        if (placement == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                $"Widget Placement (id:{model.PlacementId}) was NOT saved.");
        }

        return new OkObjectResult("Widget Placement was saved.");
    }

    public async Task<IActionResult> OnPostRefresh()
    {
        var dashboard = await service.GetDashboardAsync(_config);

        return ViewComponent("tuxboardtemplate", dashboard);
    }

    /* Dialogs */
    public async Task<IActionResult> OnPostSimpleLayoutDialog()
    {
        var dashboard = await service.GetDashboardAsync(_config);
        var layouts = dashboard.GetCurrentTab().GetLayouts().FirstOrDefault();
        var currentLayout = layouts?.LayoutRows.FirstOrDefault();

        var layoutTypes = await service.GetLayoutTypesAsync();
        var result = layoutTypes.Select(e => e.ToDto(currentLayout.LayoutTypeId)).ToList();

        return ViewComponent("simplelayoutdialog", result);
    }

    public async Task<IActionResult> OnPostSaveSimpleLayoutAsync([FromBody] SimpleLayoutRequest request)
    {
        var dashboard = await service.GetDashboardAsync(_config);

        // Since we only have a single layout for this example, we can grab the first one.
        var tab = dashboard.GetCurrentTab();
        var layouts = tab.GetLayouts().FirstOrDefault();
        
        // LayoutRows have a LayoutType. If we change the LayoutType, the LayoutRow will update on next reload.
        var currentLayoutRow = layouts?.LayoutRows?.FirstOrDefault();
        if (currentLayoutRow != null)
        {
            await service.ChangeLayoutRowToAsync(currentLayoutRow, request.LayoutTypeId);
        }

        // Refresh
        dashboard = await service.GetDashboardAsync(_config);

        return ViewComponent("tuxboardtemplate", dashboard);
    }
}