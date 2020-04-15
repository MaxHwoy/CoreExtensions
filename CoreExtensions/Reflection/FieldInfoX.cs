using System;
using System.Reflection;
using System.Collections;



namespace CoreExtensions.Reflection
{
	/// <summary>
	/// Represents all major extensions for <see cref="FieldInfo"/>.
	/// </summary>
	public static class FieldInfoX
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public static bool IsEnumerableType(this FieldInfo field)
		{
			return (!typeof(string).Equals(field.FieldType) &&
				typeof(IEnumerable).IsAssignableFrom(field.FieldType));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static FieldInfo GetFastProperty(this object obj, string name)
			=> obj.GetType().GetField(name ?? string.Empty);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="field"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static MethodInfo GetFastMethod(this FieldInfo field, string method, Type[] args)
			=> field.FieldType.GetMethod(method ?? string.Empty, args);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="field"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static MethodInfo GetFastMethod(this object obj, string field, string method, Type[] args)
			=> obj.GetType().GetField(field ?? string.Empty)?.FieldType
				.GetMethod(method ?? string.Empty, args);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="field"></param>
		/// <param name="obj"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <param name="attr"></param>
		/// <returns></returns>
		public static object FastMethodInvoke(this FieldInfo field, object obj, string method,
			Type[] args, object[] attr)
			=> field.FieldType.GetMethod(method ?? string.Empty, args)?.Invoke(obj, attr);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="field"></param>
		/// <param name="method"></param>
		/// <param name="args"></param>
		/// <param name="attr"></param>
		/// <returns></returns>
		public static object FastMethodInvoke(this object obj, string field, string method,
			Type[] args, object[] attr)
		{
			var info = obj.GetType().GetField(field ?? string.Empty);
			if (info == null) return null;
			return info.FieldType
				.GetMethod(method ?? string.Empty, args)
				?.Invoke(info.GetValue(obj), attr);
		}
	}
}
