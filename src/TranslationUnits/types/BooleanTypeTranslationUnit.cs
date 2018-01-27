/// <summary>
/// BooleanTypeTranslationUnit.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering native boolean type.
    /// </summary>
    public class BooleanTypeTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanTypeTranslationUnit"/> class.
        /// </summary>
        protected BooleanTypeTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static BooleanTypeTranslationUnit Create() => new BooleanTypeTranslationUnit();

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate() => Lexems.BooleanType;
    }
}
