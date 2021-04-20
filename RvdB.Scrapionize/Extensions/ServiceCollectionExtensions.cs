using Microsoft.Extensions.DependencyInjection;

using RvdB.Scrapionize.Interfaces;

namespace RvdB.Scrapionize.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddScrapionize(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IScraper, Scraper>();

			return serviceCollection;
		}
	}
}
