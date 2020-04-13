namespace CoreExtensions.Native
{
    /// <summary>
    /// Provides all major extensions for inverting bits of integer values.
    /// </summary>
	public static class InvertX
	{
        /// <summary>
        /// Inverts all bits of an 8-bit signed byte value.
        /// </summary>
        /// <param name="value">Signed byte value to invert.</param>
        /// <returns>Inverted signed byte value.</returns>
        public static sbyte NegateBits(this sbyte value)
        {
            sbyte result = 0;
            for (int a1 = 0; a1 < 8; ++a1)
                result |= (sbyte)((((value >> a1) & 1) ^ 1) << a1);
            return result;
        }

        /// <summary>
        /// Inverts all bits of an 8-bit unsigned byte value.
        /// </summary>
        /// <param name="value">Unigned byte value to invert.</param>
        /// <returns>Inverted unsigned byte value.</returns>
        public static byte NegateBits(this byte value)
        {
            byte result = 0;
            for (int a1 = 0; a1 < 8; ++a1)
                result |= (byte)((((value >> a1) & 1) ^ 1) << a1);
            return result;
        }

        /// <summary>
        /// Inverts all bits of a 16-bit char value.
        /// </summary>
        /// <param name="value">Char value to invert.</param>
        /// <returns>Inverted char value.</returns>
        public static char NegateBits(this char value) => (char)((ushort)value).NegateBits();

        /// <summary>
        /// Inverts all bits of a 16-bit 2-byte signed integer value.
        /// </summary>
        /// <param name="value">2-byte signed integer value to invert.</param>
        /// <returns>Inverted 2-byte signed integer value.</returns>
        public static short NegateBits(this short value)
        {
            short result = 0;
            for (int a1 = 0; a1 < 16; ++a1)
                result |= (short)((((value >> a1) & 1) ^ 1) << a1);
            return result;
        }

        /// <summary>
        /// Inverts all bits of a 16-bit 2-byte unsigned integer value.
        /// </summary>
        /// <param name="value">2-byte unsigned integer value to invert.</param>
        /// <returns>Inverted 2-byte unsigned integer value.</returns>
        public static ushort NegateBits(this ushort value)
        {
            ushort result = 0;
            for (int a1 = 0; a1 < 16; ++a1)
                result |= (ushort)((((value >> a1) & 1) ^ 1) << a1);
            return result;
        }

        /// <summary>
        /// Inverts all bits of a 32-bit 4-byte signed integer value.
        /// </summary>
        /// <param name="value">4-byte signed integer value to invert.</param>
        /// <returns>Inverted 4-byte signed integer value.</returns>
        public static int NegateBits(this int value)
        {
            int result = 0;
            for (int a1 = 0; a1 < 32; ++a1)
                result |= (((value >> a1) & 1) ^ 1) << a1;
            return result;
        }

        /// <summary>
        /// Inverts all bits of a 32-bit 4-byte unsigned integer value.
        /// </summary>
        /// <param name="value">4-byte unsigned integer value to invert.</param>
        /// <returns>Inverted 4-byte unsigned integer value.</returns>
        public static uint NegateBits(this uint value)
        {
            uint result = 0;
            for (int a1 = 0; a1 < 32; ++a1)
                result |= (((value >> a1) & 1) ^ 1) << a1;
            return result;
        }

        /// <summary>
        /// Inverts all bits of a 64-bit 8-byte signed integer value.
        /// </summary>
        /// <param name="value">8-byte signed integer value to invert.</param>
        /// <returns>Inverted 8-byte signed integer value.</returns>
        public static long NegateBits(this long value)
        {
            long result = 0;
            for (int a1 = 0; a1 < 64; ++a1)
                result |= (long)(((value >> a1) & 1) ^ 1) << a1;
            return result;
        }

        /// <summary>
        /// Inverts all bits of a 64-bit 8-byte unsigned integer value.
        /// </summary>
        /// <param name="value">8-byte unsigned integer value to invert.</param>
        /// <returns>Inverted 8-byte unsigned integer value.</returns>
        public static ulong NegateBits(this ulong value)
        {
            ulong result = 0;
            for (int a1 = 0; a1 < 64; ++a1)
                result |= (ulong)(((value >> a1) & 1) ^ 1) << a1;
            return result;
        }
    }
}
