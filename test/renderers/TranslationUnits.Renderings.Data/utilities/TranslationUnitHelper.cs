/// <summary>
/// TranslationUnitHelper.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
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
        /// <param name="className"></param>
        /// <returns></returns>
        public static ClassDeclarationTranslationUnit AddClass(this ModuleTranslationUnit translationUnit, string className)
        {
            var classTranslationUnit = ClassDeclarationTranslationUnit.Create(
                VisibilityToken.Public, IdentifierTranslationUnit.Create(className), null);
            translationUnit.AddClass(classTranslationUnit);
            return classTranslationUnit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="className"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static ClassDeclarationTranslationUnit AddClass(this ModuleTranslationUnit translationUnit, string className, string baseClassName)
        {
            var classTranslationUnit = ClassDeclarationTranslationUnit.Create(
                VisibilityToken.Public, IdentifierTranslationUnit.Create(className), IdentifierTranslationUnit.Create(baseClassName));
            translationUnit.AddClass(classTranslationUnit);
            return classTranslationUnit;
        }

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
        /// <param name="interfaceName"></param>
        public static void AddExtendedInterface(this InterfaceDeclarationTranslationUnit translationUnit, string interfaceName)
        {
            translationUnit.AddExtendedInterface(IdentifierTranslationUnit.Create(interfaceName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodDeclarationTranslationUnit AddMethod(this ClassDeclarationTranslationUnit translationUnit, string returnType, string name)
        {
            var methodTranslationUnit = MethodDeclarationTranslationUnit.Create(VisibilityToken.Public, 
                returnType != null ? TypeIdentifierTranslationUnit.Create(returnType) : null, 
                IdentifierTranslationUnit.Create(name));
            translationUnit.AddMethodDeclaration(methodTranslationUnit);
            return methodTranslationUnit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static MethodDeclarationTranslationUnit AddEmptyMethod(this ClassDeclarationTranslationUnit translationUnit, string method)
        {
            var methodDeclaration = MethodDeclarationTranslationUnit.Create(
                VisibilityToken.Public, TypeIdentifierTranslationUnit.Void, IdentifierTranslationUnit.Create(method));
            translationUnit.AddMethodDeclaration(methodDeclaration);
            return methodDeclaration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static MethodSignatureDeclarationTranslationUnit AddSignature(this InterfaceDeclarationTranslationUnit translationUnit, string signature)
        {
            var methodSignatureDeclaration = MethodSignatureDeclarationTranslationUnit.Create(
                VisibilityToken.Public, TypeIdentifierTranslationUnit.Void, IdentifierTranslationUnit.Create(signature));
            translationUnit.AddSignature(methodSignatureDeclaration);
            return methodSignatureDeclaration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static VariableDeclarationTranslationUnit AddVariable(this MethodDeclarationTranslationUnit translationUnit, string type, string name)
        {
            var variableDeclaration = VariableDeclarationTranslationUnit.Create(
                type != null ? TypeIdentifierTranslationUnit.Number : null, IdentifierTranslationUnit.Create(name));
            translationUnit.AddStatement(variableDeclaration);
            return variableDeclaration;
        }
    }
}
