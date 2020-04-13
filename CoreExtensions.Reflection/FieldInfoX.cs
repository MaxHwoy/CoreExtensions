using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions.Reflection
{
	public static class FieldInfoX
	{
		public static bool IsEnumerableType(this FieldInfo field)
		{
			return (!typeof(string).Equals(field.FieldType) &&
				typeof(IEnumerable).IsAssignableFrom(field.FieldType));
		}

		public static FieldInfo GetFastProperty(this object obj, string name)
			=> obj.GetType().GetField(name ?? string.Empty);

		public static MethodInfo GetFastMethod(this FieldInfo field, string method, Type[] args)
			=> field.FieldType.GetMethod(method ?? string.Empty, args);

		public static MethodInfo GetFastMethod(this object obj, string field, string method, Type[] args)
			=> obj.GetType().GetField(field ?? string.Empty)?.FieldType
				.GetMethod(method ?? string.Empty, args);

		public static object FastMethodInvoke(this FieldInfo field, object obj, string method,
			Type[] args, object[] attr)
			=> field.FieldType.GetMethod(method ?? string.Empty, args)?.Invoke(obj, attr);

		public static object FastMethodInvoke(this object obj, string field, string method,
			Type[] args, object[] attr)
		{
			var info = obj.GetType().GetField(field ?? string.Empty);
			if (info == null) return null;
			return info.FieldType
				.GetMethod(method ?? string.Empty)
				?.Invoke(info.GetValue(obj), attr);
		}
	}
}
