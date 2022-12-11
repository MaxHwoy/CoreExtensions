using System;
using System.Runtime.CompilerServices;

namespace CoreExtensions.Conversions
{
    /// <summary>
    /// Provides all major extensions to cast objects.
    /// </summary>
	public static class CastX
	{
        /// <summary>
        /// Special dynamic memory allocated casting function. Attempts to cast memory to any type specified.
        /// </summary>
        /// <param name="source">Object passed to be casted.</param>
        /// <param name="dest">Type to be converted to.</param>
        /// <returns>Dynamically allocated object of type specified, if fails, returns null.</returns>
        public static dynamic DynamicCast(dynamic source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }

        /// <summary>
        /// Fast way to cast memory of one object to another. Does not guarantee to return exact copy.
        /// </summary>
        /// <param name="result">Object to be casted memory into.</param>
        /// <param name="source">Object to be casted memory from.</param>
        public static void MemoryCast(this object result, object source)
        {
            var sourceType = source.GetType();
            var resultType = result.GetType();

            foreach (var fieldInfo in sourceType.GetFields())
            {
                var resultField = resultType.GetField(fieldInfo.Name);
                
                if (resultField == null)
                {
                    continue;
                }
                
                resultField.SetValue(result, fieldInfo.GetValue(source));
            }

            foreach (var propertyInfo in sourceType.GetProperties())
            {
                var resultProperty = resultType.GetProperty(propertyInfo.Name);
                
                if (resultProperty == null)
                {
                    continue;
                }
                
                resultProperty.SetValue(result, propertyInfo.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Casts any type passed to any primitive type specified.
        /// </summary>
        /// <param name="value">Object passed to be casted.</param>
        /// <param name="oftype">Primitive type to be converted to.</param>
        /// <returns>Casted value of the passed object.</returns>
        public static object ReinterpretCast(this object value, Type oftype)
        {
            return Convert.ChangeType(value, oftype);
        }

        /// <summary>
        /// Casts any object to any type specified. Throws exception in case cast fails.
        /// </summary>
        /// <typeparam name="TypeID">Type to be converted to.</typeparam>
        /// <param name="value">Object passed to be casted.</param>
        /// <returns>Casted value of type specified. If casting fails, exception will be thrown.</returns>
        public static TypeID? StaticCast<TypeID>(this IConvertible? value) where TypeID : IConvertible
        {
            return (TypeID?)Convert.ChangeType(value, typeof(TypeID));
        }

        /// <summary>
        /// Reinterprets pointer to the unmanaged object passed and returns instance of new unmanaged 
        /// type specified.
        /// </summary>
        /// <typeparam name="T">Unmanaged type to reinterpret.</typeparam>
        /// <typeparam name="S">Unmanaged type to cast to.</typeparam>
        /// <param name="value">Value to reinterpret.</param>
        /// <returns>New unmanaged instance casted from object passed.</returns>
        public static unsafe S ReinterpretCast<T, S>(T value)
            where T : unmanaged
            where S : unmanaged
		{
            return Unsafe.As<T, S>(ref value);
		}
    }
}
