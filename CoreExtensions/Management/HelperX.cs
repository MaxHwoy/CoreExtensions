using System;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions.Management
{
	/// <summary>
	/// Static helper class with methods.
	/// </summary>
	public static class HelperX
	{
		/// <summary>
		/// Bounds value passed inbetween maximum and minimum limits.
		/// </summary>
		/// <typeparam name="T"><see cref="Type"/> of value to compare. This type should inherit 
		/// from <see cref="IComparable"/> interface.</typeparam>
		/// <param name="value">An <see cref="IComparable"/> value to bound.</param>
		/// <param name="min">Minimum value to bound to.</param>
		/// <param name="max">Maximum value to bound to.</param>
		/// <returns>Bound value passed.</returns>
		public static T Bound<T>(T value, T min, T max) where T : IComparable
		{
			value = (value.CompareTo(max) > 0) ? max : value;
			value = (value.CompareTo(min) < 0) ? min : value;
			return value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int RoundUpToPowerOfTwo(int value)
		{
			value--;
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return value + 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int RoundDownToPowerOfTwo(int value)
		{
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return value - (value >> 1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int RoundToNearestPowerOfTwo(int value)
		{
			int num = RoundUpToPowerOfTwo(value);
			int num2 = RoundDownToPowerOfTwo(value);
			int num3 = Math.Abs(num - value);
			int num4 = Math.Abs(value - num2);
			return num4 < num3 ? num2 : num;
		}
	}
}
