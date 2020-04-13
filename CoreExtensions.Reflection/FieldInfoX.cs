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
	}
}
