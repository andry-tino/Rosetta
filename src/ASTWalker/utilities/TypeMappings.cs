/// <summary>
/// TypeMappings.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Utilities
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Maps system (MsCoreLib) types to TypeScript types.
    /// </summary>
    public static class TypeMappings
    {
        /// <summary>
        /// Maps a type into another.
        /// </summary>
        /// <param name="originalType">The original type.</param>
        /// <returns>The new type, or the original type if no mapping is possible.</returns>
        public static string MapType(this string originalType)
        {
            if (IsVoid(originalType))
            {
                return Lexems.VoidReturnType;
            }

            if (IsString(originalType))
            {
                return Lexems.StringType;
            }

            if (IsInt(originalType))
            {
                return Lexems.NumberType;
            }

            if (IsDouble(originalType))
            {
                return Lexems.NumberType;
            }

            if (IsFloat(originalType))
            {
                return Lexems.NumberType;
            }

            if (IsBool(originalType))
            {
                return Lexems.BooleanType;
            }

            if (IsObject(originalType))
            {
                return Lexems.AnyType;
            }

            // No type could be found
            return originalType;
        }

        private static bool IsVoid(string originalType) => originalType == typeof(void).FullName || originalType.ToLower().Contains("void");

        private static bool IsString(string originalType) => originalType == typeof(string).FullName || originalType.ToLower().Contains("string");

        private static bool IsInt(string originalType) => 
            originalType == typeof(int).FullName || 
            originalType == typeof(System.Int16).FullName || 
            originalType == typeof(System.Int32).FullName || 
            originalType == typeof(System.Int64).FullName || 
            originalType == typeof(System.IntPtr).FullName || 
            originalType == typeof(System.UInt16).FullName || 
            originalType == typeof(System.UInt32).FullName ||
            originalType == typeof(System.UInt64).FullName || 
            originalType == typeof(System.UIntPtr).FullName || 
            originalType.ToLower().Contains("int");

        private static bool IsDouble(string originalType) => originalType == typeof(double).FullName || originalType.ToLower().Contains("double");

        private static bool IsFloat(string originalType) => originalType == typeof(float).FullName || originalType.ToLower().Contains("float");

        private static bool IsBool(string originalType) => originalType == typeof(bool).FullName || originalType.ToLower().Contains("bool");

        private static bool IsObject(string originalType) => originalType == typeof(object).FullName || originalType.ToLower().Contains("object");
    }
}
