using System;
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
	}
}
