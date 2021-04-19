using System;
using System.Linq;

using HtmlAgilityPack;

using Scrapionize.Models;

namespace Scrapionize
{
	public class Scraper
	{
		/// <summary>
		/// Gets all of the Sessionize data from the passed in URL.
		/// </summary>
		/// <param name="url"><see cref="Uri"/> pointing towards the Sessionize page.</param>
		/// <returns>A <see cref="SessionizeData"/> instance containing the data for the provided <paramref name="url"/>.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is null.</exception>
		public SessionizeData Scrape(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentNullException(nameof(url));
			}

			var result = new SessionizeData();

			var doc = new HtmlWeb().Load(url);
			var descendants = doc.DocumentNode.Descendants();

			result.EventName = descendants.Where(d => d.HasClass("ibox-title")).SelectMany(d => d.Descendants("h4")).FirstOrDefault()?.InnerText;
			var contentRows = descendants.Where(d => d.HasClass("ibox-content"));
			var leftColumn = contentRows.ElementAt(1);
			var rightColumn = contentRows.ElementAt(2);
			
			var leftHeaders = leftColumn.Descendants("h2");
			result.CfpStartDate = leftHeaders.ElementAt(0).InnerText;
			result.CfpEndDate = leftHeaders.ElementAt(1).InnerText;
			// TODO: parse location
			result.Location = string.Join("\r\n", leftHeaders.ElementAt(2).Descendants("span").Select(d => d.InnerText.Trim()));
			result.EventUrl = leftHeaders.ElementAt(3).Descendants("a").Single().Attributes["href"].Value;

			var rightHeaders2 = rightColumn.Descendants("h2");
			result.EventStartDate = rightHeaders2.ElementAt(0).InnerText;
			result.EventEndDate = rightHeaders2.ElementAt(1).InnerText;

			var rightHeaders3 = rightColumn.Descendants("h3");
			rightHeaders3 = rightHeaders3.Skip(rightHeaders3.Count() - 3);
			result.Travel = rightHeaders3.ElementAt(0).NextSibling.NextSibling.InnerText;
			result.Accomodation = rightHeaders3.ElementAt(1).NextSibling.NextSibling.InnerText;
			result.EventFee = rightHeaders3.ElementAt(2).NextSibling.NextSibling.InnerText;

			return new SessionizeData();
		}
	}
}
