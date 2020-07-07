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
			return new Regex(@"^0x[0-9a-fA-F]{1,}$").IsMatch(value ?? String.Empty);
		}

		/// <summary>
		/// Gets first quoted string from the given string.
		/// </summary>
		/// <returns>First quoted string.</returns>
		public static string GetQuotedString(this string value)
		{
			var match = new Regex("[\"]{1}[^\n]*[\"]{1}").Match(value ?? String.Empty);
            return match.Success ? match.Value.Trim('\"') : String.Empty;
        }

		/// <summary>
		/// Splits string by whitespace and quotation marks.
		/// </summary>
		/// <returns><see cref="IEnumerable{T}"/> of strings.</returns>
		public static IEnumerable<string> SmartSplitString(this string value)
		{
			if (String.IsNullOrWhiteSpace(value)) yield break;
			var result = Regex.Split(value, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
			foreach (var str in result)
			{

                if (String.IsNullOrEmpty(str)) continue;
                yield return str.StartsWith('\"') && str.EndsWith('\"') ? str[1..^1] : str;
            
            }
		}

		/// <summary>
		/// Gets array of bytes of from the current string provided.
		/// </summary>
        /// <param name="value">String to convert to array of bytes.</param>
		/// <returns>Array of bytes of the string.</returns>
		public static byte[] GetBytes(this string value)
		{
            var result = new byte[value.Length];
            for (int i = 0; i < value.Length; ++i) result[i] = (byte)value[i];
            return result;
        }

        /// <summary>
        /// Gets string from array of bytes using UTF8 encoding.
        /// </summary>
        /// <param name="array">Array of bytes to convert to string.</param>
        /// <returns>String from array of bytes.</returns>
        public static string GetString(this byte[] array)
		{
            if (array == null) return null;
            string result = String.Empty;
            for (int i = 0; i < array.Length; ++i) result += (char)array[i];
            return result;
        }

        /// <summary>
        /// Gets HashCode of the string; if string is null, returns String.Empty HashCode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>HashCode of the string.</returns>
        public static int GetSafeHashCode(this string value) =>
            String.IsNullOrEmpty(value) ? String.Empty.GetHashCode() : value.GetHashCode();

        /// <summary>
        /// Returns object of type <typeparamref name="TypeID"/> from the byte array provided.
        /// </summary>
        /// <param name="array">Array of bytes to convert.</param>
        /// <returns>Object of type <typeparamref name="TypeID"/>.</returns>
        public static TypeID ToObject<TypeID>(this byte[] array) where TypeID : IConvertible
        {
            return Type.GetTypeCode(typeof(TypeID)) switch
            {
                TypeCode.Boolean => (TypeID)(object)BitConverter.ToBoolean(array, 0),
                TypeCode.Byte => (TypeID)(object)array[0],
                TypeCode.SByte => (TypeID)(object)array[0],
                TypeCode.Char => (TypeID)(object)(char)BitConverter.ToInt16(array, 0),
                TypeCode.Int16 => (TypeID)(object)BitConverter.ToInt16(array, 0),
                TypeCode.UInt16 => (TypeID)(object)BitConverter.ToUInt16(array, 0),
                TypeCode.Int32 => (TypeID)(object)BitConverter.ToInt32(array, 0),
                TypeCode.UInt32 => (TypeID)(object)BitConverter.ToUInt32(array, 0),
                TypeCode.Int64 => (TypeID)(object)BitConverter.ToInt64(array, 0),
                TypeCode.UInt64 => (TypeID)(object)BitConverter.ToUInt64(array, 0),
                TypeCode.Single => (TypeID)(object)BitConverter.ToSingle(array, 0),
                TypeCode.Double => (TypeID)(object)BitConverter.ToDouble(array, 0),
                TypeCode.String => (TypeID)(object)Encoding.UTF8.GetString(array),
                _ => default
            };
        }

        /// <summary>
        /// Gets memory of object of type <see cref="IConvertible"/> as a byte array.
        /// </summary>
        /// <param name="value">Value which memory should be returned.</param>
        /// <returns>Memory of the value passed as a byte array.</returns>
        public static byte[] GetMemory(this IConvertible value)
        {
            return Type.GetTypeCode(value.GetType()) switch
            {
                TypeCode.Boolean => BitConverter.GetBytes((bool)value),
                TypeCode.Byte => new byte[1] { (byte)value },
                TypeCode.SByte => new byte[1] { (byte)value },
                TypeCode.Char => BitConverter.GetBytes((char)value),
                TypeCode.Int16 => BitConverter.GetBytes((short)value),
                TypeCode.UInt16 => BitConverter.GetBytes((ushort)value),
                TypeCode.Int32 => BitConverter.GetBytes((int)value),
                TypeCode.UInt32 => BitConverter.GetBytes((uint)value),
                TypeCode.Int64 => BitConverter.GetBytes((long)value),
                TypeCode.UInt64 => BitConverter.GetBytes((ulong)value),
                TypeCode.Single => BitConverter.GetBytes((float)value),
                TypeCode.Double => BitConverter.GetBytes((double)value),
                TypeCode.String => Encoding.UTF8.GetBytes((string)value),
                _ => null
            };
        }
    
        /// <summary>
        /// Splits string into substrings with length specified.
        /// </summary>
        /// <param name="str">This string to split.</param>
        /// <param name="size">Size of each splitted substring.</param>
        /// <returns><see cref="IEnumerable{T}"/> of substrings.</returns>
        public static IEnumerable<string> SplitByLength(this string str, int size) =>
            Enumerable.Range(0, str.Length / size)
                .Select(i => str.Substring(i * size, size));
    }
}
