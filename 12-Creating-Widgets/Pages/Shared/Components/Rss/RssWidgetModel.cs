using System.ServiceModel.Syndication;
using Tuxboard.Core.Infrastructure.Models;

namespace DefaultWidgets.Pages.Shared.Components.Rss;

public class RssWidgetModel : WidgetModel
{
    public SyndicationFeed Feed { get; set; } = null!;
}