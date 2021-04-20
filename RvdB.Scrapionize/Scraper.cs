using System;
using System.Linq;

using HtmlAgilityPack;

using RvdB.Scrapionize.Interfaces;
using RvdB.Scrapionize.Models;

namespace RvdB.Scrapionize
{
	public class Scraper : IScraper
	{
		#region Constants

		private const string CARRIAGERETURN_LINEFEED = "\r\n";
		private const string NAME_TITLE = "ibox-title";
		private const string NAME_CONTENT = "ibox-content";
		private const string NAME_HEADING2 = "h2";
		private const string NAME_HEADING3 = "h3";
		private const string NAME_HEADING4 = "h4";
		private const string NAME_HREF = "href";
		private const string NAME_LINK = "a";
		private const string NAME_SPAN = "span";

		#endregion

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

			result.EventName = descendants.Where(d => d.HasClass(NAME_TITLE)).SelectMany(d => d.Descendants(NAME_HEADING4)).FirstOrDefault()?.InnerText;
			var contentRows = descendants.Where(d => d.HasClass(NAME_CONTENT));
			var leftColumn = contentRows.ElementAt(1);
			var rightColumn = contentRows.ElementAt(2);
			
			var leftHeaders = leftColumn.Descendants(NAME_HEADING2);
			result.CfpStartDate = leftHeaders.ElementAt(0).InnerText;
			result.CfpEndDate = leftHeaders.ElementAt(1).InnerText;
			// TODO: parse location
			result.Location = string.Join(CARRIAGERETURN_LINEFEED, leftHeaders.ElementAt(2).Descendants(NAME_SPAN).Select(d => d.InnerText.Trim()));
			result.EventUrl = leftHeaders.ElementAt(3).Descendants(NAME_LINK).Single().Attributes[NAME_HREF].Value;

			var rightHeaders2 = rightColumn.Descendants(NAME_HEADING2);
			result.EventStartDate = rightHeaders2.ElementAt(0).InnerText;
			result.EventEndDate = rightHeaders2.ElementAt(1).InnerText;

			var rightHeaders3 = rightColumn.Descendants(NAME_HEADING3);
			rightHeaders3 = rightHeaders3.Skip(rightHeaders3.Count() - 3);
			result.Travel = rightHeaders3.ElementAt(0).NextSibling.NextSibling.InnerText;
			result.Accomodation = rightHeaders3.ElementAt(1).NextSibling.NextSibling.InnerText;
			result.EventFee = rightHeaders3.ElementAt(2).NextSibling.NextSibling.InnerText;

			return result;
		}
	}
}
