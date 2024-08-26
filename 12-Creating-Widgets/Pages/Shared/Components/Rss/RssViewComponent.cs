using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;

namespace DefaultWidgets.Pages.Shared.Components.Rss;

[ViewComponent(Name = "rss")]
public class RssViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement)
    {
        var rssFeed = new Uri("https://stackoverflow.blog/feed");

        var feed = new FeedReader(rssFeed).Get();

        // placement.WidgetSettings.FirstOrDefault(e=> e.)
        // TODO: Add extension method to WidgetPlacement
        placement.SetTitle(feed.Title.Text);

        var widgetModel = new RssWidgetModel { Placement = placement, Feed = feed };

        return View(widgetModel);
    }
}

public static class WidgetPlacementExtensions
{
    public static void SetTitle(this WidgetPlacement placement, string title)
    {
        var setting = placement.WidgetSettings.FirstOrDefault(e => e.WidgetDefault.SettingName == "widgettitle");
        if (setting != null)
        {
            setting.Value = title;
        }
    }
}