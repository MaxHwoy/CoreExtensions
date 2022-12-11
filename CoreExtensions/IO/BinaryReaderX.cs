using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CoreExtensions.IO
{
    /// <summary>
    /// Provides all major extensions for <see cref="BinaryReader"/>
    /// </summary>
    public static class BinaryReaderX
    {
        /// <summary>
        /// Aligns reader to the alignment of power of 2 specified.
        /// </summary>
        /// <param name="br"></param>
        /// <param name="alignment">Alignment to which the reader should be aligned (is a power of 2).</param>
        public static void AlignReaderPow2(this BinaryReader br, int alignment)
        {
            br.BaseStream.Position = (long)((ulong)(br.BaseStream.Position + alignment - 1) & (UInt64.MaxValue - (uint)alignment + 1));
        }

        /// <summary>
        /// Reads the specified number of bytes from the current stream into a byte array 
        /// in reverse order and advances the current position by that number of bytes.
        /// </summary>
        /// <param name="br"></param>
        /// <param name="count">The number of bytes to read. This value must be 0 or a 
        /// non-negative number or an exception will occur.</param>
        /// <returns>A byte array containing data read from the underlying stream. This might be 
        /// less than the number of bytes requested if the end of the stream is reached.</returns>
        public static byte[] ReadReversedBytes(this BinaryReader br, int count)
        {
            var array = br.ReadBytes(count);

            Array.Reverse(array);

            return array;
        }

        /// <summary>
        /// Reads the <see cref="Enum"/> of type <typeparamref name="T"/> and advances 
        /// the current position by the size of the underlying type of the <see cref="Enum"/>.
        /// </summary>
        /// <typeparam name="T">Type of the <see cref="Enum"/> to read.</typeparam>
        /// <returns>Value of the <see cref="Enum"/> passed.</returns>
        public static T ReadEnum<T>(this BinaryReader br) where T : Enum
        {
            Span<byte> array = stackalloc byte[Unsafe.SizeOf<T>()];

            br.BaseStream.Read(array);

            return Unsafe.ReadUnaligned<T>(ref array[0]);
        }

        /// <summary>
        /// Reads a C-Style null-terminated string that using ASCII encoding.
        /// </summary>
        /// <returns>String with ASCII style encoding.</returns>
        public static string ReadNullTermASCII(this BinaryReader br)
        {
            var curpos = br.BaseStream.Position;

            while (br.ReadSByte() != 0)
            {
            }

            int length = (int)(br.BaseStream.Position - curpos - 1);

            if (length == 0)
            {
                return String.Empty;
            }

            br.BaseStream.Position = curpos;

            unsafe
            {
                Span<sbyte> array = length > 0x200 ? new sbyte[length] : stackalloc sbyte[length];

                for (int i = 0; i < length; ++i)
                {
                    array[i] = br.ReadSByte();
                }

                ++br.BaseStream.Position;

                return new string((sbyte*)Unsafe.AsPointer(ref array[0]), 0, length);
            }
        }

        /// <summary>
        /// Reads a C-Style null-terminated string that using UTF16 encoding.
        /// </summary>
        /// <returns>String with UTF16 style encoding.</returns>
        public static string ReadNullTermUTF16(this BinaryReader br)
        {
            var curpos = br.BaseStream.Position;

            while (br.ReadChar() != 0)
            {
            }

            int length = (int)(br.BaseStream.Position - curpos - 2);

            if (length == 0)
            {
                return String.Empty;
            }

            br.BaseStream.Position = curpos;

            unsafe
            {
                Span<char> array = length > 0x200 ? new char[length] : stackalloc char[length];

                for (int i = 0; i < length; ++i)
                {
                    array[i] = br.ReadChar();
                }

                br.BaseStream.Position += 2;

                return new string((sbyte*)Unsafe.AsPointer(ref array[0]), 0, length);
            }
        }

        /// <summary>
        /// Reads a C-Style null-terminated string that using ASCII encoding.
        /// </summary>
        /// <param name="br"></param>
        /// <param name="maxLength">Max length of the string to read.</param>
        /// <returns>String with ASCII style encoding.</returns>
        public static string ReadNullTermASCII(this BinaryReader br, int maxLength)
        {
            var endpos = br.BaseStream.Position + maxLength;

            if (maxLength == 0)
            {
                return String.Empty;
            }

            unsafe
            {
                Span<sbyte> array = maxLength > 0x200 ? new sbyte[maxLength] : stackalloc sbyte[maxLength];

                for (int i = 0; i < maxLength; ++i)
                {
                    sbyte b = br.ReadSByte();

                    if (b == 0)
                    {
                        br.BaseStream.Position = endpos;

                        if (i == 0)
                        {
                            return String.Empty;
                        }

                        return new string((sbyte*)Unsafe.AsPointer(ref array[0]), 0, i);
                    }

                    array[i] = b;
                }

                br.BaseStream.Position = endpos;

                return new string((sbyte*)Unsafe.AsPointer(ref array[0]), 0, maxLength);
            }
        }

        /// <summary>
        /// Reads a C-Style null-terminated string that using UTF16 encoding.
        /// </summary>
        /// <param name="br"></param>
        /// <param name="maxLength">Max length of the string to read.</param>
        /// <returns>String with UTF16 style encoding.</returns>
        public static string ReadNullTermUTF16(this BinaryReader br, int maxLength)
        {
            var endpos = br.BaseStream.Position + (maxLength << 1);

            if (maxLength == 0)
            {
                return String.Empty;
            }

            unsafe
            {
                Span<char> array = maxLength > 0x200 ? new char[maxLength] : stackalloc char[maxLength];

                for (int i = 0; i < maxLength; ++i)
                {
                    char b = br.ReadChar();

                    if (b == 0)
                    {
                        br.BaseStream.Position = endpos;

                        if (i == 0)
                        {
                            return String.Empty;
                        }

                        return new string((sbyte*)Unsafe.AsPointer(ref array[0]), 0, i);
                    }

                    array[i] = b;
                }

                br.BaseStream.Position = endpos;

                return new string((sbyte*)Unsafe.AsPointer(ref array[0]), 0, maxLength);
            }
        }

        /// <summary>
        /// Seeks position of the first occurence of the byte array provided.
        /// </summary>
        /// <param name="br"></param>
        /// <param name="array">Byte array to find.</param>
        /// <param name="fromstart">True if begin seeking from the start of the stream; 
        /// false otherwise.</param>
        /// <returns>Position of the first occurence of the byte array. If array was not 
        /// found, returns -1.</returns>
        public static long SeekArray(this BinaryReader br, byte[] array, bool fromstart)
        {
            if (!fromstart && array.Length > br.BaseStream.Length - br.BaseStream.Position)
            {
                return -1;
            }
            else if (fromstart && array.Length > br.BaseStream.Length)
            {
                return -1;
            }

            var pos = br.BaseStream.Position;
            var result = -1L;
            int current = 0;
            int maxmatch = array.Length;

            if (fromstart)
            {
                br.BaseStream.Position = 0;
            }

            while (current < maxmatch && br.BaseStream.Position < br.BaseStream.Length)
            {
                byte b = br.ReadByte();

                if (b == array[current])
                {
                    ++current;
                }
                else if (b != array[current] && current > 0)
                {
                    br.BaseStream.Position -= current;
                    current = 0;
                }
                else
                {
                    current = 0;
                }
            }

            if (current == maxmatch)
            {
                result = br.BaseStream.Position - current;
            }

            br.BaseStream.Position = pos;

            return result;
        }

        /// <summary>
        /// Reads unmanaged value type.
        /// </summary>
        /// <typeparam name="T">Unmanaged type to read.</typeparam>
        /// <param name="br"></param>
        /// <returns>Instance of unmanaged type provided.</returns>
        public static unsafe T ReadUnmanaged<T>(this BinaryReader br) where T : unmanaged
		{
            int size = Unsafe.SizeOf<T>();

            Span<byte> array = size > 0x200 ? new byte[size] : stackalloc byte[size];

            br.BaseStream.Read(array);

            return Unsafe.ReadUnaligned<T>(ref array[0]);
        }

        /// <summary>
        /// Reads a structure of the given type from a binary reader.
        /// </summary>
        /// <param name="br">A <see cref="BinaryReader"/> instance to read data from.</param>
        /// <typeparam name="T">The structure type. Must be a C# struct.</typeparam>
        /// <returns>A new instance of <typeparamref name="T"/> with data read from <paramref name="br"/>.</returns>
        public static T ReadStruct<T>(this BinaryReader br) where T : struct
        {
            unsafe
            {
                int size = Marshal.SizeOf<T>();

                Span<byte> array = size > 0x200 ? new byte[size] : stackalloc byte[size];

                br.BaseStream.Read(array);

                return Marshal.PtrToStructure<T>(new IntPtr(Unsafe.AsPointer(ref array[0])));
            }
        }
    }
}
