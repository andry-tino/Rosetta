/// <summary>
/// NativeTypes.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Class containing utilities for discenrining wheter a type is class, interface, struct or enum.
    /// </summary>
    public static class NativeTypes
    {
        /// <summary>
        /// Gets a value indicating whether the type is an actual class or not.
        /// </summary>
        /// <param name="typeInfo">The <see cref="ITypeInfoProxy"/> to check.</param>
        /// <returns><code>true</code> if the type is an actual class, <code>false</code> otherwise.</returns>
        /// <remarks>
        /// If the type is a class type inheriting from a native special type (System.Enum for example), 
        /// the returned value will be <code>false</code>.
        /// </remarks>
        public static bool IsNativeClassType(this ITypeInfoProxy typeInfo)
        {
            if (typeInfo.BaseType != null)
            {
                var isEnum = new EnumClass(typeInfo.BaseType).Is;

                return typeInfo.IsClass && !isEnum;
            }

            return typeInfo.IsClass;
        }

        /// <summary>
        /// Gets a value indicating whether the type is an actual enum or not.
        /// </summary>
        /// <param name="typeInfo">The <see cref="ITypeInfoProxy"/> to check.</param>
        /// <returns><code>true</code> if the type is an actual enum, <code>false</code> otherwise.</returns>
        public static bool IsNativeEnumType(this ITypeInfoProxy typeInfo)
        {
            if (typeInfo.IsClass && typeInfo.BaseType != null)
            {
                var isEnum = new EnumClass(typeInfo.BaseType).Is;

                return isEnum;
            }

            return typeInfo.IsEnum;
        }

        /// <summary>
        /// Gets a value indicating whether the type is an actual struct or not.
        /// </summary>
        /// <param name="typeInfo">The <see cref="ITypeInfoProxy"/> to check.</param>
        /// <returns><code>true</code> if the type is an actual struct, <code>false</code> otherwise.</returns>
        public static bool IsNativeStructType(this ITypeInfoProxy typeInfo)
        {
            if (typeInfo.IsClass && typeInfo.BaseType != null)
            {
                var isValueType = new ValueTypeClass(typeInfo.BaseType).Is;

                return isValueType;
            }

            return typeInfo.IsValueType && !typeInfo.IsEnum;
        }
    }
}
