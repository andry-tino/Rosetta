/// <summary>
/// IdentifierTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Interface for describing compound translation elements.
    /// </summary>
    public class IdentifierTranslationUnit : ITranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierTranslationUnit"/> class.
        /// </summary>
        private IdentifierTranslationUnit()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static IdentifierTranslationUnit Create(VisibilityToken visibility, string name, string baseClassName)
        {
            return new IdentifierTranslationUnit()
            {
                
            };
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            return "";
        }
    }
}
