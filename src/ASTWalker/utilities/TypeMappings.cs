/// <summary>
/// TypeMappings.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Utilities
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// A class describing the result of the mapping process.
    /// </summary>
    public sealed class MappingResult
    {
        /// <summary>
        /// Gets a value indicating whether a mapping was actually applied or not.
        /// </summary>
        public bool MappingApplied { get; set; }

        /// <summary>
        /// Gets the mapping result.
        /// If no mapping is found, this contains a <see cref="TypeIdentifierTranslationUnit"/> wrapping the original value.
        /// </summary>
        public ITranslationUnit MappedType { get; set; }
    }

    /// <summary>
    /// Maps system (Mscorlib.dll, part of .NET) types to TypeScript types.
    /// </summary>
    public static class TypeMappings
    {
        /// <summary>
        /// Maps a type into another.
        /// </summary>
        /// <param name="originalType">The original type (fully qualified name).</param>
        /// <returns>The new type, or the original type if no mapping is possible.</returns>
        public static MappingResult MapType(this string originalType)
        {
            if (IsVoid(originalType))
            {
                return new MappingResult() { MappedType = VoidTypeTranslationUnit.Create(), MappingApplied = true };
            }

            if (IsString(originalType))
            {
                return new MappingResult() { MappedType = StringTypeTranslationUnit.Create(), MappingApplied = true };
            }

            if (IsInt(originalType))
            {
                return new MappingResult() { MappedType = NumberTypeTranslationUnit.Create(), MappingApplied = true };
            }

            if (IsDouble(originalType))
            {
                return new MappingResult() { MappedType = NumberTypeTranslationUnit.Create(), MappingApplied = true };
            }

            if (IsFloat(originalType))
            {
                return new MappingResult() { MappedType = NumberTypeTranslationUnit.Create(), MappingApplied = true };
            }

            if (IsBool(originalType))
            {
                return new MappingResult() { MappedType = BooleanTypeTranslationUnit.Create(), MappingApplied = true };
            }

            if (IsObject(originalType))
            {
                return new MappingResult() { MappedType = AnyTypeTranslationUnit.Create(), MappingApplied = true };
            }

            // No type could be found
            return new MappingResult() { MappedType = TypeIdentifierTranslationUnit.Create(originalType), MappingApplied = false };
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
