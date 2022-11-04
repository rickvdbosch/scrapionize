using System;

using Microsoft.Extensions.DependencyInjection;

using RvdB.Scrapionize.Extensions;
using RvdB.Scrapionize.Interfaces;

using Xunit;

namespace RvdB.Scrapionize.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void WithNullIServiceCollection_AddScrapionize_ThrowsArgumentNullException()
        {
            IServiceCollection serviceCollection = null;
            
            var exception = Assert.Throws<ArgumentNullException>(serviceCollection.AddScrapionize);
            Assert.Equal("services", exception.ParamName);
        }

        [Fact]
        public void WithValidIServiceCollection_AddScrapionize_AddsScrapionize()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScrapionize();
            Assert.Single(serviceCollection, s => s.ServiceType == typeof(IScraper));
        }
    }
}
