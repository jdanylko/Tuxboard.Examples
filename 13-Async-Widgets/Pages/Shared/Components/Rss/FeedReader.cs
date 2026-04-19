using System.ServiceModel.Syndication;
using System.Xml;

namespace AsyncWidgets.Pages.Shared.Components.Rss;

public class FeedReader
{
    private readonly Uri _rssFeed;

    public FeedReader(Uri rssFeed)
    {
        _rssFeed = rssFeed;
    }

    public SyndicationFeed Get()
    {
        using (var reader = XmlReader.Create(_rssFeed.AbsoluteUri))
        {
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            
            return feed;
        }
    }
}