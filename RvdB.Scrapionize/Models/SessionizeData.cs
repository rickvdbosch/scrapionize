namespace Scrapionize.Models
{
	/// <summary>
	/// Model that holds the Sessionize CFP data
	/// </summary>
	public class SessionizeData
	{
		/// <summary>Name of the event.</summary>
		public string EventName { get; set; }

		/// <summary>Start date of the event.</summary>
		public string EventStartDate { get; set; }

		/// <summary>End date of the event.</summary>
		public string EventEndDate { get; set; }

		/// <summary>URL of the event.</summary>
		public string EventUrl { get; set; }

		/// <summary>Start date of the CFP.</summary>
		public string CfpStartDate { get; set; }

		/// <summary>End date of the CFP.</summary>
		public string CfpEndDate { get; set; }

		/// <summary>Location of the event.</summary>
		public string Location { get; set; }

		/// <summary>Arrangements around travel for speakers.</summary>
		public string Travel { get; set; }

		/// <summary>Arrangements around accomodation for speakers.</summary>
		public string Accomodation { get; set; }

		/// <summary>Arrangements around the event fee for speakers.</summary>
		public string EventFee { get; set; }
	}
}