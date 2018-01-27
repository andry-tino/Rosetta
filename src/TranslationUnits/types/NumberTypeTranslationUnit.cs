/// <summary>
/// NumberTypeTranslationUnit.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering native numeric type.
    /// </summary>
    public class NumberTypeTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberTypeTranslationUnit"/> class.
        /// </summary>
        protected NumberTypeTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static NumberTypeTranslationUnit Create() => new NumberTypeTranslationUnit();

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate() => Lexems.NumberType;
    }
}
