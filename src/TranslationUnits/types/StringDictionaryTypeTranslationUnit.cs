/// <summary>
/// StringDictionaryTypeTranslationUnit.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering native dictionary (string keys) type.
    /// </summary>
    public class StringDictionaryTypeTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringDictionaryTypeTranslationUnit"/> class.
        /// </summary>
        protected StringDictionaryTypeTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StringDictionaryTypeTranslationUnit Create() => new StringDictionaryTypeTranslationUnit();

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate() => $"{{ [id: {Lexems.StringType}]: {Lexems.AnyType} }}";
    }
}
