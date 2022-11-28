using System;

using Xunit;

namespace RvdB.Scrapionize.Tests
{
    public class ScraperTests
    {
        #region Constants

        private const string URL_PARAMETER_NAME = "url";

        #endregion

        [Fact]
        public void WithValidSessionizeUrl_Scrape_ReturnsSessionizeData()
        {
            var scraper = new Scraper();

            var result = scraper.Scrape(new Uri("https://sessionize.com/build-stuff-2021-lithuania"));

            Assert.NotNull(result);
            Assert.Equal("Build Stuff 2021 Lithuania", result.EventName);
            Assert.Equal("http://buildstuff.events", result.EventUrl);
        }

        [Fact]
        public void WithSessionizeUrlNull_Scrape_ThrowsArgumentNullException()
        {
            var scraper = new Scraper();

            var exception = Assert.Throws<ArgumentNullException>(() => scraper.Scrape(null));

            Assert.Equal(URL_PARAMETER_NAME, exception.ParamName);
        }
    }
}
