using System;

namespace CoreExtensions.Reflection
{
    /// <summary>
    /// Provides all major extensions for <see cref="Type"/>.
    /// </summary>
	public static class TypeX
	{
        /// <summary>
        /// Returns default value for a <see cref="TypeCode"/> given.
        /// </summary>
        /// <param name="typeCode"><see cref="TypeCode"/> to return default value of.</param>
        /// <returns>Default value of <see cref="TypeCode"/> as an <see cref="Object"/>.</returns>
        public static object? GetDefaultValue(TypeCode typeCode)
        {
            return typeCode switch
            {
                TypeCode.Empty => null,
                TypeCode.Object => new object(),
                TypeCode.DBNull => DBNull.Value,
                TypeCode.Boolean => false,
                TypeCode.Char => '\0',
                TypeCode.SByte => (sbyte)0,
                TypeCode.Byte => Byte.MinValue,
                TypeCode.Int16 => (short)0,
                TypeCode.UInt16 => UInt16.MinValue,
                TypeCode.Int32 => 0,
                TypeCode.UInt32 => UInt32.MinValue,
                TypeCode.Int64 => 0L,
                TypeCode.UInt64 => UInt64.MinValue,
                TypeCode.Single => 0.0f,
                TypeCode.Double => 0.0,
                TypeCode.Decimal => Decimal.Zero,
                TypeCode.DateTime => new DateTime(),
                TypeCode.String => String.Empty,
                _ => null,
            };
        }

        /// <summary>
        /// Retrieves an array of the values of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/> type to get values of.</typeparam>
        /// <returns>Array of enum values.</returns>
        public static T[]? GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)) as T[];
        }

        /// <summary>
        /// Check whether given <see cref="Type"/> is inherited from specified generic 
        /// interface.
        /// </summary>
        /// <param name="thistype"></param>
        /// <param name="intertype"><see cref="Type"/> of generic interface to check.</param>
        /// <returns>True if given type is inherited from specified generic interface; 
        /// false otherwise.</returns>
        public static bool IsFromGenericInterface(this Type thistype, Type intertype)
        {
            foreach (var inter in thistype.GetInterfaces())
            {
                if (inter.IsGenericType && inter.GetGenericTypeDefinition() == intertype)
                {
                    return true;
                }
            }

            if (thistype.IsGenericType && thistype.GetGenericTypeDefinition() == intertype)
            {
                return true;
            }

            var basetype = thistype.BaseType;

            return basetype is not null && basetype.IsFromGenericInterface(intertype);
        }

        /// <summary>
        /// Check whether given <see cref="Type"/> is inherited from specified generic 
        /// class.
        /// </summary>
        /// <param name="thistype"></param>
        /// <param name="classtype"><see cref="Type"/> of generic class to check.</param>
        /// <returns>True if given type is inherited from specified generic class; 
        /// false otherwise.</returns>
        public static bool IsFromGenericClass(this Type thistype, Type classtype)
        {
            var basetype = thistype.BaseType;

            return basetype is not null && ((basetype.IsGenericType && basetype.GetGenericTypeDefinition() == classtype) || IsFromGenericClass(basetype, classtype));
        }

        /// <summary>
        /// Checks whether given <see cref="Type"/> is a generic definition itself.
        /// </summary>
        /// <param name="giventype"></param>
        /// <param name="generictype"><see cref="Type"/> of generic class to check.</param>
        /// <returns>True if given type is a generic definition itself; false otherwise.</returns>
        public static bool IsGenericItself(this Type giventype, Type generictype)
        {
            return giventype.IsGenericType && giventype.GetGenericTypeDefinition() == generictype;
        }
    }
}
