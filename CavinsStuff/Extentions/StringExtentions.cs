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

		/// <summary>
		/// Performs a super fast case incensitive string replace
		/// </summary>
		/// <param name="original">The original string</param>
		/// <param name="pattern">The string to search for</param>
		/// <param name="replacement">The string that will replace the search string</param>
		/// <returns>The original text with all replacements done</returns>
		public static string ReplaceCI(this string original, string pattern, string replacement)
		{
			int count, position0, position1;
			count = position0 = position1 = 0;
			string upperString = original.ToUpper();
			string upperPattern = pattern.ToUpper();
			int inc = (original.Length / pattern.Length) * (replacement.Length - pattern.Length);
			char[] chars = new Char[original.Length + Math.Max(0, inc)];

			while ((position1 = upperString.IndexOf(upperPattern, position0)) != -1)
			{
				for (int i = position0; i < position1; ++i) chars[count++] = original[i];
				for (int i = 0; i < replacement.Length; ++i) chars[count++] = replacement[i];
				position0 = position1 + pattern.Length;
			}

			if (position0 == 0) return original;
			for (int i = position0; i < original.Length; ++i) chars[count++] = original[i];
			return new string(chars, 0, count);
		}

	}
}
