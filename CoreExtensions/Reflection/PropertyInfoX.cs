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
		/// 
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static bool IsEnumerableType(this PropertyInfo property)
		{
			return (!typeof(string).Equals(property.PropertyType) &&
				typeof(IEnumerable).IsAssignableFrom(property.PropertyType));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static PropertyInfo GetFastProperty(this object obj, string name)
			=> obj.GetType().GetProperty(name ?? string.Empty);

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
			if (info == null) return null;
			return info.PropertyType
				.GetMethod(method ?? string.Empty, args)
				?.Invoke(info.GetValue(obj), attr);
		}
	}
}
