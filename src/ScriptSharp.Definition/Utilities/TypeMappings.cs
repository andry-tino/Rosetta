/// <summary>
/// TypeMappings.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Utilities
{
    using System;

    using Rosetta.AST.Utilities;
    using Rosetta.Translation;

    /// <summary>
    /// Type mappings for ScriptSharp Mscorlib
    /// </summary>
    public static class TypeMappings
    {
        private const string ScriptSharpMscorlibDictionaryTypeFullName = "System.Collections.Dictionary";
        private const string ScriptSharpMscorlibArrayListTypeFullName = "System.Collections.ArrayList";

        /// <summary>
        /// Maps a type into another.
        /// </summary>
        /// <param name="originalType">The original type.</param>
        /// <returns>The new type, or the original type if no mapping is possible.</returns>
        public static MappingResult MapType(this string originalType)
        {
            if (IsDictionary(originalType))
            {
                return new MappingResult() { MappedType = StringDictionaryTypeTranslationUnit.Create(), MappingApplied = true };
            }

            if (IsArrayList(originalType))
            {
                return new MappingResult() { MappedType = AnyArrayTypeTranslationUnit.Create(), MappingApplied = true };
            }

            // No type could be found
            return new MappingResult() { MappedType = TypeIdentifierTranslationUnit.Create(originalType), MappingApplied = false };
        }

        private static bool IsDictionary(string originalType) => originalType == ScriptSharpMscorlibDictionaryTypeFullName;

        private static bool IsArrayList(string originalType) => originalType == ScriptSharpMscorlibArrayListTypeFullName;
    }
}
