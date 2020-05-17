﻿using System;
using System.Reflection;
using System.Collections;



namespace CoreExtensions.Reflection
{
	/// <summary>
	/// Provides all major extensions for <see cref="PropertyInfo"/>.
	/// </summary>
	public static class PropertyInfoX
	{
		/// <summary>
		/// Checks if the property is of <see cref="IEnumerable"/> type.
		/// </summary>
		/// <param name="property">Property to check.</param>
		/// <returns>True if property is of <see cref="IEnumerable"/> type; false otherwise.</returns>
		public static bool IsIEnumerableType(this PropertyInfo property) =>
			(!typeof(string).Equals(property.PropertyType) &&
				typeof(IEnumerable).IsAssignableFrom(property.PropertyType));

		/// <summary>
		/// Checks if the property is of <see cref="Enum"/> type.
		/// </summary>
		/// <param name="obj">Object to check.</param>
		/// <param name="property">Name of the property to check.</param>
		/// <returns>True if property is enum; false otherwise.</returns>
		public static bool IsPropertyOfEnumType(this object obj, string property) =>
			obj.GetType().GetProperty(property).PropertyType.IsEnum;

		/// <summary>
		/// Returns all <see cref="Enum"/> names of the property provided.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="property">Name of the enumerable property.</param>
		/// <returns>Array of strings.</returns>
		public static string[] GetPropertyEnumerableTypes(this object obj, string property) =>
			obj.GetType().GetProperty(property).PropertyType.GetEnumNames();

		/// <summary>
		/// Gets <see cref="PropertyInfo"/> of the object by name provided.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="name">Name of the <see cref="PropertyInfo"/> to get.</param>
		/// <returns><see cref="PropertyInfo"/> with the name provided.</returns>
		public static PropertyInfo GetFastProperty(this object obj, string name)
			=> obj.GetType().GetProperty(name ?? string.Empty);

		/// <summary>
		/// Gets value of <see cref="PropertyInfo"/> of the object by name provided.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="name">Name of the <see cref="PropertyInfo"/> to get value from.</param>
		/// <returns>Value of the <see cref="PropertyInfo"/> with the name provided.</returns>
		public static object GetFastPropertyValue(this object obj, string name)
			=> obj.GetType().GetProperty(name ?? string.Empty)?.GetValue(obj);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="property"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static MethodInfo GetFastMethod(this PropertyInfo property, string method, Type[] args)
			=> property.PropertyType.GetMethod(method ?? string.Empty, args);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="property"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static MethodInfo GetFastMethod(this object obj, string property, string method, Type[] args)
			=> obj.GetType().GetProperty(property ?? string.Empty)?.PropertyType
				.GetMethod(method ?? string.Empty, args);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="property"></param>
		/// <param name="obj"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <param name="attr"></param>
		/// <returns></returns>
		public static object FastMethodInvoke(this PropertyInfo property, object obj, string method,
			Type[] args, object[] attr)
			=> property.PropertyType.GetMethod(method ?? string.Empty, args)?.Invoke(obj, attr);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="property"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <param name="attr"></param>
		/// <returns></returns>
		public static object FastMethodInvoke(this object obj, string property, string method,
			Type[] args, object[] attr)
		{
			var info = obj.GetType().GetProperty(property ?? string.Empty);
			return info?.PropertyType
				.GetMethod(method ?? string.Empty, args)
				?.Invoke(info.GetValue(obj), attr);
		}
	}
}
