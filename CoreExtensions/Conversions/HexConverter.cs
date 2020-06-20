using System;
using System.Globalization;
using System.ComponentModel;



namespace CoreExtensions.Conversions
{
	/// <summary>
	/// Provides converting methods for hexadecimal numbers prefixed and not prefixed with "0x".
	/// </summary>
	public class HexConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the given type to the
		/// type of this converter, using the specified context.
		/// </summary>
		/// <param name="context">An <seealso cref="ITypeDescriptorContext"/> that provides 
		/// a format context.</param>
		/// <param name="sourceType">A <see cref="Type"/> that represents the type you want 
		/// to convert from.</param>
		/// <returns>True if this converter can perform the conversion; otherwise, false.</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

		/// <summary>
		/// Converts the given object to the type of this converter, using the specified
		/// context and culture information.
		/// </summary>
		/// <param name="context">An <seealso cref="ITypeDescriptorContext"/> that provides 
		/// a format context.</param>
		/// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, 
            object value)
        {
			return value is string s
				? Convert.ToUInt32(s, 16)
                : base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Converts the given value object to the specified type, using the specified context
		/// and culture information.
		/// </summary>
		/// <param name="context">An <seealso cref="ITypeDescriptorContext"/> that provides 
		/// a format context.</param>
		/// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <param name="destinationType">The <see cref="Type"/> to convert the value parameter to.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, 
            object value, Type destinationType)
        {
			return destinationType == typeof(string)
				? $"0x{(uint)value:X8}"
				: base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
