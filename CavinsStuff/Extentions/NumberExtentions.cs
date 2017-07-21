using System;
using System.Linq;

namespace CavinsStuff.Extentions
{
	/// <summary>
	/// Contains all int extentions.
	/// </summary>
	public static class NumberExtentions
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

		/// <summary>
		/// Converts the decimal to a currency string. 
		/// eg:12345.234  = R 12 345.23
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <param name="symbol">Symbol to prepend</param>
		/// <returns></returns>
		public static String ToCurrency(this decimal value, string symbol = "R", bool prependSymbol = true)
		{
			var item = Math.Round(value, 2);

			var splitup = item.ToString().Split('.');

			var result = "";

			var count = 0;
			foreach (var character in splitup.FirstOrDefault().Reverse())
			{
				if (count++ % 3 == 0)
					result += " ";

				result += character;
			}
			var chars = result.Reverse().ToArray();
			result = (new string(chars)).Trim();

			if (splitup.Count() > 1)
			{
				result += "." + splitup.Last();
			}

			return prependSymbol ? $"{symbol} {result}" : $"{result} {symbol}";
		}

	}
}
