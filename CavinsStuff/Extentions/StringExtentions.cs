using System;

namespace CavinsStuff.Extentionsm
{
	/// <summary>
	/// Contains all string extentions.
	/// </summary>
	public static class StringExtentions
	{
		/// <summary>
		/// Returns the specified length of characters starting from the right
		/// </summary>
		/// <param name="intialString"></param>
		/// <param name="length">Length of characters to return</param>
		/// <returns></returns>
		public static string Right(this string intialString, int length)
		{
			//Check if the value is valid
			if (string.IsNullOrEmpty(intialString))
			{
				//Set valid empty string as string could be null
				intialString = string.Empty;
			}
			else if (intialString.Length > length && length > 0)
			{
				//Make the string no longer than the max length
				intialString = intialString.Substring(intialString.Length - length, length);
			}

			//Return the string
			return intialString;
		}

		/// <summary>
		/// Returns the base64encoded text
		/// </summary>
		/// <param name="encoding"></param>
		/// <param name="text">Text to be encoded</param>
		/// <returns></returns>
		public static string ToBase64(this System.Text.Encoding encoding, string text)
		{
			if (text == null) throw new ArgumentNullException(nameof(text));

			byte[] textAsBytes = encoding.GetBytes(text);
			return Convert.ToBase64String(textAsBytes);
		}

		/// <summary>
		/// Checks whether the string is null, string.Empty or whitespace 
		/// </summary>
		public static bool IsNullOrEmpty(this string text)
		{
			return string.IsNullOrEmpty(text);
		}

		/// <summary>
		/// Checks whether the string can parse into a guid
		/// </summary>
		public static bool IsGuid(this string text)
		{
			Guid convertedGuid;
			return Guid.TryParse(text, out convertedGuid);
		}

		/// <summary>
		/// Tries to parse string to Guid
		/// </summary>
		/// <exception cref="InvalidCastException">Failed to cast string</exception>
		public static Guid ToGuid(this string text)
		{
			Guid convertedGuid;
			var success = Guid.TryParse(text, out convertedGuid);
			if (!success) throw new InvalidCastException($"Failed casting {text} to Guid");
			else return convertedGuid;
		}
	}
}
