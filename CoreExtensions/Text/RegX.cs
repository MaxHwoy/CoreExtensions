﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoreExtensions.Text
{
	/// <summary>
	/// Provides all major extensions for <see cref="Regex"/> and <see cref="String"/>.
	/// </summary>
	public static class RegX
	{
		/// <summary>
		/// Determines whether given string can be a hexadecimal digit of type 0x[...].
		/// </summary>
		/// <returns>True if string can be a hexadecimal digit; false otherwise.</returns>
		public static bool IsHexString(this string? value)
		{
            if (value is null || value.Length < 3)
            {
                return false;
            }

            if (value[0] != '0')
            {
                return false;
            }

            if (value[1] != 'x' && value[1] != 'X')
            {
                return false;
            }

            for (int i = 2; i < value.Length; ++i)
            {
                char c = value[i];

                if (('0' <= c && c <= '9') ||
                    ('A' <= c && c <= 'F') ||
                    ('a' <= c && c <= 'f'))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to convert hexadecimal string to an unsigned integer value. Hexadecimal
        /// string should start with '0x' or '0X' value.
        /// </summary>
        /// <param name="value">String value to attempt to parse.</param>
        /// <param name="result">Unsigned integer value converted from the string value passed.</param>
        /// <returns>True if conversion was successful; false if string was not a hexadecimal
        /// string and/or contained characters that could not be converted.</returns>
        public static bool TryHexStringToUInt32(this string? value, out uint result)
        {
            result = 0;

            if (value is null || value.Length < 3)
            {
                return false;
            }

            if (value[0] != '0')
            {
                return false;
            }

            if (value[1] != 'x' && value[1] != 'X')
            {
                return false;
            }

            for (int i = 2, k = value.Length - 3; i < value.Length; ++i, --k)
            {
                char c = value[i];
                uint mult = (uint)(1 << (k << 2));

                if ('0' <= c && c <= '9')
                {
                    result += (uint)(c - '0') * mult;
                    continue;
                }

                if ('A' <= c && c <= 'F')
                {
                    result += (uint)(c - 'A' + 10) * mult;
                    continue;
                }

                if ('a' <= c && c <= 'f')
                {
                    result += (uint)(c - 'a' + 10) * mult;
                    continue;
                }

                result = 0;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts unsigned integer passed to its hexadecimal string representation that
        /// starts with '0x'. Faster than <see cref="UInt32.ToString()"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="toLower">True if all 'a'-'f' characters should be lowercase in the
        /// string representation; false if all 'A'-'F' should be uppercase instead.</param>
        /// <returns>Hexadecimal string representation of an unsigned integer value.</returns>
        public static string FastToHexString(this uint value, bool toLower)
        {
            var array = new char[10] { '0', 'x', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };

            if (toLower)
            {
                for (int i = 0, k = 9; i < 8; ++i, --k)
                {
                    var bit = (value >> (i << 2)) & 0x0F;

                    if (bit < 10)
                    {
                        array[k] = (char)(0x30 + bit);
                    }
                    else
                    {
                        array[k] = (char)(0x57 + bit);
                    }
                }
            }
            else
            {
                for (int i = 0, k = 9; i < 8; ++i, --k)
                {
                    var bit = (value >> (i << 2)) & 0x0F;

                    if (bit < 10)
                    {
                        array[k] = (char)(0x30 + bit);
                    }
                    else
                    {
                        array[k] = (char)(0x37 + bit);
                    }
                }
            }

            return new string(array);
        }

        /// <summary>
        /// Gets first quoted string from the given string.
        /// </summary>
        /// <returns>First quoted string.</returns>
        public static string GetQuotedString(this string value)
		{
			for (int i = 0; i < value.Length; ++i)
            {
                if (value[i] == '"')
                {
                    for (int k = i + 1; k < value.Length; ++k)
                    {
                        if (value[k] == '"')
                        {
                            int length = k - i - 1;

                            return length == 0
                                ? String.Empty
                                : value.Substring(i + 1, length);
                        }
                    }

                    return String.Empty;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Splits string by whitespace and quotation marks.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of strings.</returns>
        public static IEnumerable<string> SmartSplitString(this string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                yield break;
            }

            bool inQuote = false;

            int k = 0;

            for (int i = 0; i < value.Length; ++i)
            {
                char v = value[i];

                if (v == '"')
                {
                    int length = i - k;

                    if (length > 0)
                    {
                        yield return value.Substring(k, length);
                    }

                    inQuote = !inQuote;

                    k = i + 1;
                }
                else if (v == ' ' && !inQuote)
                {
                    int length = i - k;

                    if (length > 0)
                    {
                        yield return value.Substring(k, length);
                    }

                    k = i + 1;
                }
            }

            if (k < value.Length && !inQuote)
            {
                yield return value[k..];
            }
        }

        /// <summary>
        /// Gets array of bytes of from the current string provided.
        /// </summary>
        /// <param name="value">String to convert to array of bytes.</param>
        /// <returns>Array of bytes of the string.</returns>
        public static byte[] GetBytes(this string? value)
		{
            if (String.IsNullOrEmpty(value))
            {
                return Array.Empty<byte>();
            }
            else
            {
                var result = new byte[value.Length];

                for (int i = 0; i < value.Length; ++i)
                {
                    result[i] = (byte)value[i];
                }

                return result;
            }
        }

        /// <summary>
        /// Gets string from array of bytes using UTF8 encoding.
        /// </summary>
        /// <param name="array">Array of bytes to convert to string.</param>
        /// <returns>String from array of bytes.</returns>
        public static string? GetString(this byte[]? array)
		{
            if (array is null)
            {
                return null;
            }

            string result = String.Empty;

            for (int i = 0; i < array.Length; ++i)
            {
                result += (char)array[i];
            }

            return result;
        }

        /// <summary>
        /// Gets HashCode of the string; if string is null, returns String.Empty HashCode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>HashCode of the string.</returns>
        public static int GetSafeHashCode(this string? value)
        {
            return String.IsNullOrEmpty(value)
                ? String.Empty.GetHashCode()
                : value.GetHashCode();
        }

        /// <summary>
        /// Returns object of type <typeparamref name="TypeID"/> from the byte array provided.
        /// </summary>
        /// <param name="array">Array of bytes to convert.</param>
        /// <returns>Object of type <typeparamref name="TypeID"/>.</returns>
        public static TypeID? ToObject<TypeID>(this byte[] array) where TypeID : IConvertible
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
        public static byte[]? GetMemory(this IConvertible value)
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
        public static IEnumerable<string> SplitByLength(this string str, int size)
        {
            return Enumerable.Range(0, str.Length / size).Select(i => str.Substring(i * size, size));
        }
    }
}
