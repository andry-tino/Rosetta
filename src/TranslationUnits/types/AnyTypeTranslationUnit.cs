/// <summary>
/// AnyTypeTranslationUnit.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering native any (object) type.
    /// </summary>
    public class AnyTypeTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnyTypeTranslationUnit"/> class.
        /// </summary>
        protected AnyTypeTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AnyTypeTranslationUnit Create() => new AnyTypeTranslationUnit();

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate() => Lexems.AnyType;
    }
}
