﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;



namespace CoreExtensions.Conversions
{
	/// <summary>
	/// Provides all major extensions for <see cref="IEnumerable{T}"/>.
	/// </summary>
	public static class EnumerableX
	{
		/// <summary>
		/// Gets copy of <see cref="IEnumerable{T}"/> of type <typeparamref name="T2"/> 
		/// converted to type <typeparamref name="T1"/>.
		/// </summary>
		/// <typeparam name="T1"><see cref="Type"/> of the copied 
		/// <see cref="IEnumerable{T}"/>.</typeparam>
		/// <typeparam name="T2"><see cref="Type"/> of the <see cref="IEnumerable{T}"/> 
		/// provided.</typeparam>
		/// <param name="value"><see cref="IEnumerable{T}"/> to copy.</param>
		/// <returns><see cref="IEnumerable{T}"/> of type <typeparamref name="T1"/>.</returns>
		public static IEnumerable<T1> GetEnumerableCopy<T1, T2>(this IEnumerable<T2> value)
			where T1 : IConvertible where T2 : IConvertible
		{
			if (value is IEnumerable enumerator)
			{
				foreach (T2 obj in enumerator)
					yield return obj.StaticCast<T1>();
			}
		}

		/// <summary>
		/// Gets copy of <see cref="IEnumerable{T}"/> of type <typeparamref name="T2"/> 
		/// converted to type <typeparamref name="T1"/> as <see cref="List{T}"/>.
		/// </summary>
		/// <typeparam name="T1"><see cref="Type"/> of the copied 
		/// <see cref="IEnumerable{T}"/>.</typeparam>
		/// <typeparam name="T2"><see cref="Type"/> of the <see cref="IEnumerable{T}"/> 
		/// provided.</typeparam>
		/// <param name="value"><see cref="IEnumerable{T}"/> to copy.</param>
		/// <returns><see cref="List{T}"/> of type <typeparamref name="T1"/>.</returns>
		public static List<T1> GetListCopy<T1, T2>(this IEnumerable<T2> value)
			where T1 : IConvertible where T2 : IConvertible
			=> value.GetEnumerableCopy<T1, T2>().ToList();

		/// <summary>
		/// Gets copy of <see cref="IEnumerable{T}"/> of type <typeparamref name="T2"/> 
		/// converted to type <typeparamref name="T1"/> as an array.
		/// </summary>
		/// <typeparam name="T1"><see cref="Type"/> of the copied 
		/// <see cref="IEnumerable{T}"/>.</typeparam>
		/// <typeparam name="T2"><see cref="Type"/> of the <see cref="IEnumerable{T}"/> 
		/// provided.</typeparam>
		/// <param name="value"><see cref="IEnumerable{T}"/> to copy.</param>
		/// <returns>Array of type <typeparamref name="T1"/>.</returns>
		public static T1[] GetArrayCopy<T1, T2>(this IEnumerable<T2> value)
			where T1 : IConvertible where T2 : IConvertible
			=> value.GetEnumerableCopy<T1, T2>().ToArray();

		public static IEnumerable<string> FindAllWithSubstring(this IEnumerable<string> e, string match)
		{
			foreach (var str in e)
				if (str.Contains(match))
					yield return str;
		}

		public static TypeID PrimitiveAverage<TypeID>(this IEnumerable<TypeID> value) 
			where TypeID : IConvertible
		{
			decimal total = 0;
			decimal count = 0;
			if (value == null || value.Count() == 0) return default;
			switch (Type.GetTypeCode(typeof(TypeID)))
			{
				case TypeCode.Boolean:
					foreach (var obj in value)
					{
						var _ = (bool)obj.ReinterpretCast(typeof(bool));
						total += _ ? 1 : 0;
						++count;
					}
					bool boolresult = (total / count) >= (decimal)0.5;
					return boolresult.StaticCast<TypeID>();

				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					foreach (var obj in value)
					{
						total += (decimal)obj.ReinterpretCast(typeof(decimal));
						++count;
					}
					decimal result = total / count;
					return result.StaticCast<TypeID>();

				default:
					return default;
			}
		}
	}
}
