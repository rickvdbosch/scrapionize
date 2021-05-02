using System;
using System.Globalization;
using System.Linq;

using HtmlAgilityPack;

using RvdB.Scrapionize.Interfaces;
using RvdB.Scrapionize.Models;

namespace RvdB.Scrapionize
{
	public class Scraper : IScraper
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
			var descendants = doc.DocumentNode.Descendants().ToList();

			result.EventName = descendants.Where(d => d.HasClass(Constants.NAME_TITLE)).SelectMany(d => d.Descendants(Constants.NAME_HEADING4)).FirstOrDefault()?.InnerText;
			var contentRows = descendants.Where(d => d.HasClass(Constants.NAME_CONTENT)).ToList();
			var leftColumn = contentRows.ElementAt(1);
			var rightColumn = contentRows.ElementAt(2);
			
			var leftHeaders = leftColumn.Descendants(Constants.NAME_HEADING2).ToList();
			result.EventStartDate = ParseSessionizeDate(leftHeaders.ElementAt(0).InnerText);
			result.EventEndDate = ParseSessionizeDate(leftHeaders.ElementAt(1).InnerText);
			// TODO: parse location
			result.Location = string.Join(Constants.CARRIAGERETURN_LINEFEED, leftHeaders.ElementAt(2).Descendants(Constants.NAME_SPAN).Select(d => d.InnerText.Trim()));
			result.EventUrl = leftHeaders.ElementAt(3).Descendants(Constants.NAME_LINK).Single().Attributes[Constants.NAME_HREF].Value;

			var rightHeaders2 = rightColumn.Descendants(Constants.NAME_HEADING2).ToList();
			result.CfpStartDate = ParseSessionizeDate(rightHeaders2.ElementAt(0).InnerText);
			result.CfpEndDate = ParseSessionizeDate(rightHeaders2.ElementAt(1).InnerText);

			var rightHeaders3 = rightColumn.Descendants(Constants.NAME_HEADING3).ToList();
			rightHeaders3 = rightHeaders3.Skip(rightHeaders3.Count() - 3).ToList();
			result.Travel = rightHeaders3.ElementAt(0).NextSibling.NextSibling.InnerText;
			result.Accommodation = rightHeaders3.ElementAt(1).NextSibling.NextSibling.InnerText;
			result.EventFee = rightHeaders3.ElementAt(2).NextSibling.NextSibling.InnerText;

			return result;
		}

		private static DateTime ParseSessionizeDate(string date)
		{
			return DateTime.ParseExact(date, Constants.DATE_FORMAT, CultureInfo.InvariantCulture);
		}
	}
}
