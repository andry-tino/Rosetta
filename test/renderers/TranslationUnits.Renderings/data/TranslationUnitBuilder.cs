/// <summary>
/// TranslationUnitBuilder.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public static class TranslationUnitBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildClassTranslationUnit(VisibilityToken visibility, string name, string baseClassName)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ClassDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create(name), 
                baseClassName == null ? null : IdentifierTranslationUnit.Create(baseClassName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildMethodTranslationUnit(VisibilityToken visibility, string returnType, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return MethodDeclarationTranslationUnit.Create(
                visibility, returnType == null ? 
                null : IdentifierTranslationUnit.Create(name),
                IdentifierTranslationUnit.Create(name));
        }
    }
}
