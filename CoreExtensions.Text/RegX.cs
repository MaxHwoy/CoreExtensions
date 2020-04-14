using System;
using System.Text;
using System.Text.RegularExpressions;



namespace CoreExtensions.Text
{
	public static class RegX
	{
		public static bool IsHexString(this string value)
		{
			// needs more work
			return new Regex(@"^0x?[0-9a-fA-F]{1,}$").IsMatch(value ?? string.Empty);
		}

		public static string GetQuotedString(this string value)
		{
			var match = new Regex("[\"]{1}[^\n]*[\"]{1}").Match(value ?? string.Empty);
			if (match.Success) return match.Value.Trim('\"');
			else return string.Empty;
		}
	}
}
