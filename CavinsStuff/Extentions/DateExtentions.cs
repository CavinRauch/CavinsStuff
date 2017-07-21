using System;
using System.Diagnostics;
using System.Linq;

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
		/// First day of given year
		/// </summary>
		/// <example>
		/// 2017-02-21 13:12:32PM => 2017-01-01 13:12:32PM
		/// </example>
		public static DateTime FirstDayOfYear(this DateTime date) => new DateTime(date.Year, 1, 1);

		/// <summary>
		/// Last day of given year
		/// </summary>
		/// <example>
		/// 2017-02-21 13:12:32PM => 2017-12-31 13:12:32PM
		/// </example>
		public static DateTime LastDayOfYear(this DateTime date) => new DateTime(date.Year + 1, 1, 1).AddDays(-1);

		/// <summary>
		/// First day of given month
		/// </summary>
		/// <example>
		/// 2017-02-21 13:12:32PM => 2017-02-01 13:12:32PM
		/// </example>
		public static DateTime FirstDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);

		/// <summary>
		/// Last day of given month. 
		/// NOTE: Includes Logic for leap years
		/// </summary>
		/// <example>
		/// 2017-02-21 13:12:32PM => 2017-02-28 13:12:32PM
		/// </example>
		public static DateTime LastDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

		/// <summary>
		/// Start of the day
		/// </summary>
		/// <example>
		///	2017-02-21 13:12:32PM => 2017-02-21 12:00:00AM 
		/// </example>
		public static DateTime StartOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

		/// <summary>
		/// End of the day
		/// </summary>
		/// <example>
		/// 2017-02-21 13:12:32PM => 2017-02-21 11:59:59PM  
		/// </example>
		public static DateTime EndOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

		/// <summary>
		/// Checks if the date matches the day of the month
		/// </summary>
		/// <param name="date">Date</param>
		/// <param name="dayOfMonth">Day of the month as an <see cref="int"/></param>
		public static bool IsDayOfMonth(this DateTime date, int dayOfMonth)
		{
			return (date.Day == dayOfMonth);
		}

		/// <summary>
		/// Checks if the date matches the day of the month
		/// </summary>
		/// <param name="date">Date</param>
		/// <param name="dayOfMonth">Day of the month as an <see cref="int"/></param>
		public static bool IsDayOfMonth(this DateTime date, params int[] dayOfMonth)
		{
			return dayOfMonth.Contains(date.Day);
		}

		/// <summary>
		/// Gets the amount of days between this date and the end Date
		/// </summary>
		/// <param name="date">Starting date</param>
		/// <param name="endDate">End date</param>
		public static int DaysBetween(this DateTime date, DateTime endDate)
		{
			var smallest = date < endDate ? date : endDate;
			var largest = date > endDate ? date : endDate;
			return (largest - smallest).Days;
		}

		/// <summary>
		/// Gets the amount of Months between this date and the end Date
		/// </summary>
		/// <param name="date">Starting date</param>
		/// <param name="endDate">End date</param>
		public static int MonthsBetween(this DateTime date, DateTime endDate)
		{
			var smallest = date < endDate ? date : endDate;
			var largest = date > endDate ? date : endDate;
			return (((largest.Year - smallest.Year) * 12) + largest.Month - smallest.Month);
		}

		/// <summary>
		/// Gets the amount of years between this date and the end Date
		/// </summary>
		/// <param name="date">Starting date</param>
		/// <param name="endDate">End date</param>
		public static int YearsBetween(this DateTime date, DateTime endDate)
		{
			var smallest = date < endDate ? date : endDate;
			var largest = date > endDate ? date : endDate;
			return (largest.Year - smallest.Year);
		}
	}
}
