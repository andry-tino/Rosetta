/// <summary>
/// AnyArrayTypeTranslationUnit.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering native `any[]` type.
    /// </summary>
    public class AnyArrayTypeTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnyArrayTypeTranslationUnit"/> class.
        /// </summary>
        protected AnyArrayTypeTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AnyArrayTypeTranslationUnit Create() => new AnyArrayTypeTranslationUnit();

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate() => $"{Lexems.AnyType}[]";
    }
}
