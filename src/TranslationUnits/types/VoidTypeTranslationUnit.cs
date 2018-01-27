/// <summary>
/// VoidTypeTranslationUnit.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering native void type.
    /// </summary>
    public class VoidTypeTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidTypeTranslationUnit"/> class.
        /// </summary>
        protected VoidTypeTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static VoidTypeTranslationUnit Create() => new VoidTypeTranslationUnit();

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate() => Lexems.VoidReturnType;
    }
}
