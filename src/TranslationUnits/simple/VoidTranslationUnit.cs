/// <summary>
/// VoidTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit which translates into an empty string.
    /// </summary>
    public class VoidTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        private VoidTranslationUnit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static VoidTranslationUnit Create()
        {
            return new VoidTranslationUnit();
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            return string.Empty;
        }
    }
}
