using System;

using Xunit;

using Scrapionize;

namespace Tests
{
    public class ScraperTests
    {
        [Fact]
        public void WithValidSessionizeUrl_Scrape_ReturnsSessionizeData()
        {
            var scraper = new Scraper();

            var result = scraper.Scrape(new Uri("https://sessionize.com/build-stuff-2021-lithuania"));
        }
    }
}
