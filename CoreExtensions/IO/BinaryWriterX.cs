using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CoreExtensions.IO
{
	/// <summary>
	/// Provides all major extensions for <see cref="BinaryWriter"/>
	/// </summary>
	public static class BinaryWriterX
	{
		/// <summary>
		/// Fills stream buffer till the certain padding is reached.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="alignment">Align to fill the stream buffer to.</param>
		/// <param name="alignValue">The alignment byte value to writer.</param>
		public static void AlignBuffer(this BinaryWriter bw, int alignment, byte alignValue)
		{
			int padding = alignment - ((int)(bw.BaseStream.Position % alignment));

			if (padding != alignment)
			{
				Span<byte> array = padding > 0x200 ? new byte[padding] : stackalloc byte[padding];

				Unsafe.InitBlockUnaligned(ref array[0], alignValue, (uint)padding);

				bw.Write(array);
			}
		}

		/// <summary>
		/// Aligns writer to the alignment of power of 2 specified.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="alignment">Alignment to which the writer should be aligned (is a power of 2).</param>
		/// <param name="alignValue">The alignment byte value to writer.</param>
		public static void AlignWriterPow2(this BinaryWriter bw, int alignment, byte alignValue)
		{
			int padding = alignment - ((int)bw.BaseStream.Position & (alignment - 1));

			if (padding != alignment)
			{
				Span<byte> array = padding > 0x200 ? new byte[padding] : stackalloc byte[padding];

				Unsafe.InitBlockUnaligned(ref array[0], alignValue, (uint)padding);

				bw.Write(array);
			}
		}

		/// <summary>
		/// Writes a byte array to the underlying stream in reverse order.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="array">A byte array containing the data to write.</param>
		public static void WriteReversedBytes(this BinaryWriter bw, byte[]? array)
		{
			if (array is null || array.Length == 0)
            {
				return;
            }

			Span<byte> reversed = array.Length > 0x200 ? new byte[array.Length] : stackalloc byte[array.Length];

			array.CopyTo(reversed);

			MemoryExtensions.Reverse(reversed);

			bw.Write(reversed);
		}

		/// <summary>
		/// Writes the <see cref="Enum"/> of type <typeparamref name="T"/> and advances 
		/// the current position by the size of the underlying type of the <see cref="Enum"/>.
		/// </summary>
		/// <typeparam name="T">Type of the <see cref="Enum"/> to read.</typeparam>
		/// <param name="bw"></param>
		/// <param name="value"><see cref="Enum"/> value to write.</param>
		public static void WriteEnum<T>(this BinaryWriter bw, T value) where T : Enum
		{
			Span<byte> array = stackalloc byte[Unsafe.SizeOf<T>()];

			Unsafe.WriteUnaligned(ref array[0], value);

			bw.BaseStream.Write(array);
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using ASCII encoding.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="value">String value to write.</param>
		public static void WriteNullTermASCII(this BinaryWriter bw, string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				bw.Write(Byte.MinValue);
			}
			else
			{
				Span<byte> array = value.Length > 0x200 ? new byte[value.Length + 1] : stackalloc byte[value.Length + 1];

				for (int i = 0; i < value.Length; ++i)
				{
					array[i] = (byte)value[i];
				}

				array[value.Length] = 0;

				bw.Write(array);
			}
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using UTF16 encoding.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="value">String value to write.</param>
		public static void WriteNullTermUTF16(this BinaryWriter bw, string value)
		{
			if (!String.IsNullOrEmpty(value))
			{
				bw.Write(value.AsSpan());
			}

			bw.Write(Char.MinValue);
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using ASCII encoding.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="value">String value to write.</param>
		/// <param name="maxLength">Max length of the string to write; if length of the string 
		/// is less then length specified, padding will be added after it to fill buffer.</param>
		public static void WriteNullTermASCII(this BinaryWriter bw, string value, int maxLength)
		{
			if (maxLength <= 0)
			{
				return;
			}

			Span<byte> array = maxLength > 0x200 ? new byte[maxLength] : stackalloc byte[maxLength];

			if (String.IsNullOrEmpty(value))
			{
				array[0] = 0;
			}
			else
			{
				int trueMax = (value.Length > maxLength - 1) ? maxLength - 1 : value.Length;

				for (int i = 0; i < trueMax; ++i)
				{
					array[i] = (byte)value[i];
				}

				array[trueMax] = 0;
			}

			bw.Write(array);
		}

		/// <summary>
		/// Writes a C-Style null-terminated string that using UTF16 encoding.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="value">String value to write.</param>
		/// <param name="maxLength">Max length of the string to write; if length of the string 
		/// is less then length specified, padding will be added after it to fill buffer.</param>
		public static void WriteNullTermUTF16(this BinaryWriter bw, string value, int maxLength)
		{
			if (maxLength <= 0)
            {
				return;
            }

			Span<char> array = maxLength > 0x200 ? new char[maxLength] : stackalloc char[maxLength];

			if (String.IsNullOrEmpty(value))
			{
				array[0] = Char.MinValue;
			}
			else
			{
				int trueMax = (value.Length > maxLength - 1) ? maxLength - 1 : value.Length;

				value.AsSpan().CopyTo(array[..trueMax]);

				array[trueMax] = Char.MinValue;
			}

			bw.Write(array);
		}

		/// <summary>
		/// Fills stream buffer till the certain padding is reached.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="alignment">Align to fill the stream buffer to.</param>
		public static void FillBuffer(this BinaryWriter bw, int alignment)
		{
			bw.AlignWriterPow2(alignment, 0);
		}

		/// <summary>
		/// Fills stream buffer till the certain padding is reached (power of 2).
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="alignment">Align to fill the stream buffer to (power of 2).</param>
		public static void FillBufferPow2(this BinaryWriter bw, int alignment)
        {
			bw.AlignWriterPow2(alignment, 0);
        }
	
		/// <summary>
		/// Writes amount of bytes specified.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="value">Byte value to write.</param>
		/// <param name="count">Amount of bytes to write.</param>
		public static void WriteBytes(this BinaryWriter bw, byte value, int count)
		{
			Span<byte> array = count > 0x200 ? new byte[count] : stackalloc byte[count];

			Unsafe.InitBlockUnaligned(ref array[0], value, (uint)count);

			bw.Write(array);
		}

		/// <summary>
		/// Writes unmanaged value type.
		/// </summary>
		/// <typeparam name="T">Unmanaged type to write.</typeparam>
		/// <param name="bw"></param>
		/// <param name="value">Instance of unmanaged type to write.</param>
		public static unsafe void WriteUnmanaged<T>(this BinaryWriter bw, T value) where T : unmanaged
		{
			int size = Unsafe.SizeOf<T>();

			Span<byte> array = size > 0x200 ? new byte[size] : stackalloc byte[size];

			Unsafe.WriteUnaligned(ref array[0], value);

			bw.BaseStream.Write(array);
		}

		/// <summary>
		/// Attempts to write struct of type <typeparamref name="T"/>. In order for struct
		/// to be read correctly, it should have a <see cref="StructLayoutAttribute"/>.
		/// </summary>
		/// <typeparam name="T">Type of struct to write.</typeparam>
		/// <param name="bw"></param>
		/// <param name="value">Struct of type <typeparamref name="T"/> to write.</param>
		public static void WriteStruct<T>(this BinaryWriter bw, T value) where T : struct
		{
			unsafe
			{
				int size = Marshal.SizeOf<T>();

				Span<byte> array = size > 0x200 ? new byte[size] : stackalloc byte[size];

				Marshal.StructureToPtr(value, new IntPtr(Unsafe.AsPointer(ref array[0])), false);

				bw.BaseStream.Write(array);
			}
		}
	}
}
