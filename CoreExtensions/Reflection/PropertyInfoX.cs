using System;
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
		public static bool IsIEnumerableType(this PropertyInfo property)
        {
			return !typeof(string).Equals(property.PropertyType) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType);
		}

		/// <summary>
		/// Checks if the property is of <see cref="Enum"/> type.
		/// </summary>
		/// <param name="obj">Object to check.</param>
		/// <param name="property">Name of the property to check.</param>
		/// <returns>True if property is enum; false otherwise.</returns>
		public static bool IsPropertyOfEnumType(this object obj, string property)
        {
			return obj.GetType().GetProperty(property)?.PropertyType.IsEnum ?? false;
		}

		/// <summary>
		/// Returns all <see cref="Enum"/> names of the property provided.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="property">Name of the enumerable property.</param>
		/// <returns>Array of strings.</returns>
		public static string[] GetPropertyEnumerableTypes(this object obj, string property)
        {
			return obj.GetType().GetProperty(property)?.PropertyType.GetEnumNames() ?? Array.Empty<string>();
		}

		/// <summary>
		/// Gets <see cref="PropertyInfo"/> of the object by name provided.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="name">Name of the <see cref="PropertyInfo"/> to get.</param>
		/// <returns><see cref="PropertyInfo"/> with the name provided.</returns>
		public static PropertyInfo? GetFastProperty(this object obj, string name)
        {
			return obj.GetType().GetProperty(name);
		}

		/// <summary>
		/// Gets value of <see cref="PropertyInfo"/> of the object by name provided.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="name">Name of the <see cref="PropertyInfo"/> to get value from.</param>
		/// <returns>Value of the <see cref="PropertyInfo"/> with the name provided.</returns>
		public static object? GetFastPropertyValue(this object obj, string name)
        {
			return obj.GetType().GetProperty(name)?.GetValue(obj);
		}

		/// <summary>
		/// Gets method of <see cref="PropertyInfo"/> of the object by names and types provided.
		/// </summary>
		/// <param name="property">This property to get method from.</param>
		/// <param name="method">Name of the method to get.</param>
		/// <param name="args">Argument types of the method.</param>
		/// <returns>MethodInfo found from arguments passed.</returns>
		public static MethodInfo? GetFastMethod(this PropertyInfo property, string method, Type[] args)
        {
			return property.PropertyType.GetMethod(method, args);
		}

		/// <summary>
		/// Gets method of <see cref="PropertyInfo"/> of the object by names and types provided.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="property">This property to get method from.</param>
		/// <param name="method">Name of the method to get.</param>
		/// <param name="args">Argument types of the method.</param>
		/// <returns>MethodInfo found from arguments passed.</returns>
		public static MethodInfo? GetFastMethod(this object obj, string property, string method, Type[] args)
        {
			return obj.GetType()
				.GetProperty(property)?.PropertyType
				.GetMethod(method, args);
		}

		/// <summary>
		/// Invokes method found by method name, object and property info passed.
		/// </summary>
		/// <param name="property">This property to get method from.</param>
		/// <param name="obj">Object to parse.</param>
		/// <param name="method">Name of the method to get.</param>
		/// <param name="args">Argument types of the method.</param>
		/// <param name="attr">Arguments passed to the invokable method.</param>
		/// <returns>Result object from invokation of a method.</returns>
		public static object? FastMethodInvoke(this PropertyInfo property, object? obj, string method, Type[] args, object?[]? attr)
        {
			return property.PropertyType.GetMethod(method, args)?.Invoke(obj, attr);
		}

		/// <summary>
		/// Invokes method found by method name, object and property info passed.
		/// </summary>
		/// <param name="obj">Object to parse.</param>
		/// <param name="property">Name of the property to get.</param>
		/// <param name="method">Name of the method to get.</param>
		/// <param name="args">Argument types of the method.</param>
		/// <param name="attr">Arguments passed to the invokable method.</param>
		/// <returns>Result object from invokation of a method.</returns>
		public static object? FastMethodInvoke(this object obj, string property, string method, Type[] args, object?[]? attr)
		{
			var propertyInfo = obj.GetType().GetProperty(property);

			return propertyInfo?.PropertyType
				.GetMethod(method, args)
				?.Invoke(propertyInfo.GetValue(obj), attr);
		}
	}
}
