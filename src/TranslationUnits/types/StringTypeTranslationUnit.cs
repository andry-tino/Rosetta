/// <summary>
/// StringTypeTranslationUnit.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering native string type.
    /// </summary>
    public class StringTypeTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringTypeTranslationUnit"/> class.
        /// </summary>
        protected StringTypeTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StringTypeTranslationUnit Create() => new StringTypeTranslationUnit();

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate() => Lexems.StringType;
    }
}
