/// <summary>
/// TranslationUnitHelper.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class TranslationUnitHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="interfaceName"></param>
        public static void AddInterface(this ClassDeclarationTranslationUnit translationUnit, string interfaceName)
        {
            translationUnit.AddImplementedInterface(IdentifierTranslationUnit.Create(interfaceName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="method"></param>
        public static void AddEmptyMethod(this ClassDeclarationTranslationUnit translationUnit, string method)
        {
            ITranslationUnit methodDeclaration = MethodDeclarationTranslationUnit.Create(
                VisibilityToken.Public, IdentifierTranslationUnit.Void, IdentifierTranslationUnit.Create(method));

            translationUnit.AddMethodDeclaration(methodDeclaration);
        }
    }
}
