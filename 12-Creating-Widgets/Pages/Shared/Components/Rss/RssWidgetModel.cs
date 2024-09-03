using System.ServiceModel.Syndication;
using Tuxboard.Core.Infrastructure.Models;

namespace CreatingWidgets.Pages.Shared.Components.Rss;

public class RssWidgetModel : WidgetModel
{
    public SyndicationFeed Feed { get; set; } = null!;
    public int Limit { get; set; } = 10;
}