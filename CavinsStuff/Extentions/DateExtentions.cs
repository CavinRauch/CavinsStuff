using System;
using System.Diagnostics;

namespace CavinsStuff.Extentions
{
	/// <summary>
	/// Contains all date extentions.
	/// </summary>
	public static class DateExtentions
	{
		/// <summary>
		/// Sets the dates to their defaults.
		/// </summary>
		/// <param name="defaultFromDate">Defaults to DateTime.MinValue if null</param>
		/// <param name="defaultToDate">Defaults to DateTime.MaxValue if null</param>
		public static void DefaultDatesIfNull(ref DateTime? fromDate, ref DateTime? toDate, DateTime? defaultFromDate = null, DateTime? defaultToDate = null)
		{
			try
			{
				if (!fromDate.HasValue)
					fromDate = defaultFromDate.HasValue ? defaultFromDate : DateTime.MinValue;

				if (!toDate.HasValue)
					toDate = defaultToDate.HasValue ? defaultToDate : DateTime.MaxValue;
			}
			catch (Exception ex)
			{
				while (ex.InnerException != null) ex = ex.InnerException;
				Debug.WriteLine($"Helper Error at DefaultDatesIfNull(). line:13");
				Debug.WriteLine($"Server returned: {ex.Message}");
			}
		}

		/// <summary>
		/// Returns the first day of the month.
		/// </summary>
		public static DateTime FirstDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);

		/// <summary>
		/// Returns the first day of the year.
		/// </summary>
		public static DateTime FirstDayOfYear(this DateTime date) => new DateTime(date.Year, 1, 1);

		/// <summary>
		/// Returns the last day of the month.
		/// </summary>
		public static DateTime LastDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

		/// <summary>
		/// Returns the last day of the year.
		/// </summary>
		public static DateTime LastDayOfYear(this DateTime date) => new DateTime(date.Year + 1, 1, 1).AddDays(-1);
	}
}
