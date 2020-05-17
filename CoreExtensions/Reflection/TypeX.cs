using System;

namespace CoreExtensions.Reflection
{
    /// <summary>
    /// Provides all major extensions for <see cref="Type"/>.
    /// </summary>
	public static class TypeX
	{
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
                    return true;
            }

            if (thistype.IsGenericType && thistype.GetGenericTypeDefinition() == intertype)
                return true;

            var basetype = thistype.BaseType;
            return basetype == null
                ? false
                : basetype.IsFromGenericInterface(intertype);
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
            return basetype == null
                ? false
                : basetype.IsGenericType && basetype.GetGenericTypeDefinition() == classtype
                    ? true :
                    IsFromGenericClass(basetype, classtype);
        }

        /// <summary>
        /// Checks whether given <see cref="Type"/> is a generic definition itself.
        /// </summary>
        /// <param name="giventype"></param>
        /// <param name="generictype"><see cref="Type"/> of generic class to check.</param>
        /// <returns>True if given type is a generic definition itself; false otherwise.</returns>
        public static bool IsGenericItself(this Type giventype, Type generictype)
            => giventype.IsGenericType && giventype.GetGenericTypeDefinition() == generictype;
    }
}
