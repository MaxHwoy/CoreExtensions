using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions.Reflection
{
	public static class PropertyInfoX
	{
		public static bool IsEnumerableType(this PropertyInfo property)
		{
			return (!typeof(string).Equals(property.PropertyType) &&
				typeof(IEnumerable).IsAssignableFrom(property.PropertyType));
		}

		public static PropertyInfo GetFastProperty(this object obj, string name)
			=> obj.GetType().GetProperty(name ?? string.Empty);

		public static MethodInfo GetFastMethod(this PropertyInfo property, string method, Type[] args)
			=> property.PropertyType.GetMethod(method ?? string.Empty, args);

		public static MethodInfo GetFastMethod(this object obj, string property, string method, Type[] args)
			=> obj.GetType().GetProperty(property ?? string.Empty)?.PropertyType
				.GetMethod(method ?? string.Empty, args);

		public static object FastMethodInvoke(this PropertyInfo property, object obj, string method,
			Type[] args, object[] attr)
			=> property.PropertyType.GetMethod(method ?? string.Empty, args)?.Invoke(obj, attr);

		public static object FastMethodInvoke(this object obj, string property, string method,
			Type[] args, object[] attr)
		{
			var info = obj.GetType().GetProperty(property ?? string.Empty);
			if (info == null) return null;
			return info.PropertyType
				.GetMethod(method ?? string.Empty)
				?.Invoke(info.GetValue(obj), attr);
		}
	}
}
