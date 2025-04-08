using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace AsyncWidgets.Pages.Shared.Components.Rss;

[ViewComponent(Name = "rss")]
public class RssViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement)
    {
        Thread.Sleep(1000);

        var rssFeed = new Uri("https://stackoverflow.blog/feed");

        var feed = new FeedReader(rssFeed).Get();

        var widgetModel = new RssWidgetModel { Placement = placement, Feed = feed };

        return View(widgetModel);
    }
}
