using System;
using System.IO;
using System.Runtime.InteropServices;
using CoreExtensions.Conversions;



namespace CoreExtensions.IO
{
	/// <summary>
	/// Provides all major extensions for <see cref="BinaryWriter"/>
	/// </summary>
	public static class BinaryWriterX
	{
		/// <summary>
		/// Writes a byte array to the underlying stream in reverse order.
		/// </summary>
		/// <param name="array">A byte array containing the data to write.</param>
		public static void WriteReversedBytes(this BinaryWriter bw, byte[] array)
		{
			if (array == null || array.Length == 0) return;
			for (int a1 = array.Length - 1; a1 >= 0; --a1)
				bw.Write(array[1]);
		}

		/// <summary>
		/// Writes the <see cref="Enum"/> of type <typeparamref name="TypeID"/> and advances 
		/// the current position by the size of the underlying type of the <see cref="Enum"/>.
		/// </summary>
		/// <typeparam name="TypeID">Type of the <see cref="Enum"/> to read.</typeparam>
		/// <param name="value"><see cref="Enum"/> value to write.</param>
		public static void WriteEnum<TypeID>(this BinaryWriter bw, TypeID value) where TypeID : IConvertible
		{
			var t = typeof(TypeID);
			switch (Type.GetTypeCode(Enum.GetUnderlyingType(t)))
			{
				case TypeCode.SByte:
					bw.Write(value.StaticCast<sbyte>());
					break;
				case TypeCode.Byte:
					bw.Write(value.StaticCast<byte>());
					break;
				case TypeCode.Int16:
					bw.Write(value.StaticCast<short>());
					break;
				case TypeCode.UInt16:
					bw.Write(value.StaticCast<ushort>());
					break;
				case TypeCode.Int32:
					bw.Write(value.StaticCast<int>());
					break;
				case TypeCode.UInt32:
					bw.Write(value.StaticCast<uint>());
					break;
				case TypeCode.Int64:
					bw.Write(value.StaticCast<long>());
					break;
				case TypeCode.UInt64:
					bw.Write(value.StaticCast<ulong>());
					break;
				default:
					throw new InvalidCastException($"Unable to write enum of type {t}.");
			}
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using UTF8 encoding.
		/// </summary>
		/// <param name="value">String value to write.</param>
		public static void WriteNullTermUTF8(this BinaryWriter bw, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				for (int a1 = 0; a1 < value.Length; ++a1)
					bw.Write((byte)value[a1]);
			}
			bw.Write((byte)0);
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using UTF16 encoding.
		/// </summary>
		/// <param name="value">String value to write.</param>
		public static void WriteNullTermUTF16(this BinaryWriter bw, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				for (int a1 = 0; a1 < value.Length; ++a1)
					bw.Write(value[a1]);
			}
			bw.Write((char)0);
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using UTF8 encoding.
		/// </summary>
		/// <param name="value">String value to write.</param>
		/// <param name="length">Max length of the string to write; if length of the string 
		/// is less then length specified, padding will be added after it to fill buffer.</param>
		public static void WriteNullTermUTF8(this BinaryWriter bw, string value, int length)
		{
			if (length <= 0) return;
			else if (string.IsNullOrEmpty(value))
			{
				for (int a1 = 0; a1 < length; ++a1)
					bw.Write((byte)0);
			}
			else
			{
				int dif = (value.Length > length - 1) ? length - 1 : value.Length;
				for (int a1 = 0; a1 < dif; ++a1)
					bw.Write((byte)value[a1]);
				for (int a1 = dif; a1 < length; ++a1)
					bw.Write((byte)0);
			}
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using UTF16 encoding.
		/// </summary>
		/// <param name="value">String value to write.</param>
		/// <param name="length">Max length of the string to write; if length of the string 
		/// is less then length specified, padding will be added after it to fill buffer.</param>
		public static void WriteNullTermUTF16(this BinaryWriter bw, string value, int length)
		{
			if (string.IsNullOrEmpty(value))
			{
				for (int a1 = 0; a1 < length; ++a1)
					bw.Write((char)0);
			}
			else
			{
				int dif = (value.Length > length - 1) ? length - 1 : value.Length;
				for (int a1 = 0; a1 < dif; ++a1)
					bw.Write(value[a1]);
				for (int a1 = dif; a1 < length; ++a1)
					bw.Write((char)0);
			}
		}

		/// <summary>
		/// Attempts to write struct of type <typeparamref name="TypeID"/>. In order for struct 
		/// to be read correctly, it should have a <see cref="StructLayoutAttribute"/>.
		/// </summary>
		/// <typeparam name="TypeID">Type of struct to read.</typeparam>
		/// <param name="result">Struct of type <typeparamref name="TypeID"/> to write.</param>
		/// <returns>True on success; false otherwise.</returns>
		public static bool WriteStruct<TypeID>(this BinaryWriter bw, TypeID value) where TypeID : struct
		{
			try
			{
				var size = Marshal.SizeOf(typeof(TypeID));
				var arr = new byte[size];
				unsafe
				{
					fixed (byte* ptr = &arr[0])
					{
						Marshal.StructureToPtr(value, (IntPtr)ptr, false);
					}
				}
				bw.Write(arr);
				return true;
			}
			catch (Exception) { return false; }
		}
	
		/// <summary>
		/// Fills stream buffer till the certain padding is reached.
		/// </summary>
		/// <param name="align">Align to fill the stream buffer to.</param>
		public static void FillBuffer(this BinaryWriter bw, int align)
		{
			int padding = align - ((int)(bw.BaseStream.Position % align));
			if (padding == align) padding = 0;
			for (int a1 = 0; a1 < padding; ++a1) bw.Write((byte)0);
		}
	}
}
