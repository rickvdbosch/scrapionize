using System;

using Xunit;

using RvdB.Scrapionize;

namespace Tests
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
            // TODO Implement actual asserts
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
