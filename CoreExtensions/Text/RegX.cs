using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CoreExtensions.Conversions;



namespace CoreExtensions.Text
{
	/// <summary>
	/// Provides all major extensions for <see cref="Regex"/>.
	/// </summary>
	public static class RegX
	{
		/// <summary>
		/// Determines whether given string can be a hexadecimal digit of type 0x[...].
		/// </summary>
		/// <returns>True if string can be a hexadecimal digit; false otherwise.</returns>
		public static bool IsHexString(this string value)
		{
			return new Regex(@"^0x[0-9a-fA-F]{1,}$").IsMatch(value ?? string.Empty);
		}

		/// <summary>
		/// Gets first quoted string from the given string.
		/// </summary>
		/// <returns>First quoted string.</returns>
		public static string GetQuotedString(this string value)
		{
			var match = new Regex("[\"]{1}[^\n]*[\"]{1}").Match(value ?? string.Empty);
			if (match.Success) return match.Value.Trim('\"');
			else return string.Empty;
		}

		/// <summary>
		/// Splits string by whitespace and quotation marks.
		/// </summary>
		/// <returns><see cref="IEnumerable{T}"/> of strings.</returns>
		public static IEnumerable<string> SmartSplitString(this string value)
		{
			if (string.IsNullOrWhiteSpace(value)) yield break;
			var result = Regex.Split(value, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
			foreach (var str in result)
			{
				if (str.StartsWith('\"') && str.EndsWith('\"'))
					yield return str.Substring(1, str.Length - 2);
				else
					yield return str;
			}
		}

		/// <summary>
		/// Gets array of bytes of from the current string provided.
		/// </summary>
		/// <returns>Array of bytes of the string.</returns>
		public static byte[] GetBytes(this string value) => Encoding.UTF8.GetBytes(value);

		/// <summary>
		/// Gets memory of the object as an array of bytes. Object must be either 
		/// <see cref="IConvertible"/> or <see cref="IEnumerable"/>.
		/// </summary>
		/// <returns>Memory of the object as an array of bytes.</returns>
		public static byte[] GetMemory(this object value)
		{
			try
			{
				if (value is IConvertible convertible)
				{
					return Type.GetTypeCode(value.GetType()) switch
					{
						TypeCode.Boolean => BitConverter.GetBytes(convertible.StaticCast<bool>()),
						TypeCode.Byte => BitConverter.GetBytes(convertible.StaticCast<byte>()),
						TypeCode.SByte => BitConverter.GetBytes(convertible.StaticCast<sbyte>()),
						TypeCode.Int16 => BitConverter.GetBytes(convertible.StaticCast<short>()),
						TypeCode.UInt16 => BitConverter.GetBytes(convertible.StaticCast<ushort>()),
						TypeCode.Int32 => BitConverter.GetBytes(convertible.StaticCast<int>()),
						TypeCode.UInt32 => BitConverter.GetBytes(convertible.StaticCast<uint>()),
						TypeCode.Int64 => BitConverter.GetBytes(convertible.StaticCast<long>()),
						TypeCode.UInt64 => BitConverter.GetBytes(convertible.StaticCast<ulong>()),
						TypeCode.Single => BitConverter.GetBytes(convertible.StaticCast<float>()),
						TypeCode.Double => BitConverter.GetBytes(convertible.StaticCast<double>()),
						TypeCode.String => convertible.StaticCast<string>().GetBytes(),
						_ => null
					};
				}
				else if (value is IEnumerable enumerable) return enumerable.Cast<byte>().ToArray();
				else return null;
			}
			catch (Exception) { return null; }
		}
	}
}
