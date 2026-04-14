using System.ServiceModel.Syndication;
using System.Xml;

namespace CreatingWidgets.Pages.Shared.Components.Rss;

public class FeedReader(Uri rssFeed)
{
    public SyndicationFeed Get()
    {
        using (var reader = XmlReader.Create(rssFeed.AbsoluteUri))
        {
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            
            return feed;
        }
    }
}