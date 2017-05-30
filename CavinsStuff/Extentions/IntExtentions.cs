namespace CavinsStuff.Extentions
{
	/// <summary>
	/// Contains all int extentions.
	/// </summary>
	public class IntExtentions
	{
		/// <summary>
		/// Takes a value from a range and maps it to a value with the same postition in a new range.
		/// </summary>
		/// <example>
		/// Start Range: 0 - 100
		/// Value: 20		/// 
		/// New Range: 0 - 200
		/// Value: 40
		/// </example>
		/// <param name="originalRangeStart">Start of the Original Range</param>
		/// <param name="originalRangeEnd">End of the Original Range</param>
		/// <param name="newRangeStart">Start of the New Range</param>
		/// <param name="newRangeEnd">End of the New Range</param>
		/// <param name="value">Value to be mapped</param>
		/// <returns>Mapped int value</returns>
		public static int MapToNewRange(int originalRangeStart, int originalRangeEnd, int newRangeStart, int newRangeEnd, int value)
		{
			double scale = (double)(newRangeEnd - newRangeStart) / (originalRangeEnd - originalRangeStart);
			return (int)(newRangeStart + ((value - originalRangeStart) * scale));
		}
	}
}
