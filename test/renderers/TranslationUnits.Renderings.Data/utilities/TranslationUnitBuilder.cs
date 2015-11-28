/// <summary>
/// TranslationUnitBuilder.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class TranslationUnitBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildModuleTranslationUnit(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ModuleTranslationUnit.Create(IdentifierTranslationUnit.Create(name));
        }

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
        /// <param name="statements"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildMethodTranslationUnit(VisibilityToken visibility, string returnType, string name, ITranslationUnit[] statements = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MethodDeclarationTranslationUnit translationUnit =  MethodDeclarationTranslationUnit.Create(
                visibility, returnType == null ? null : IdentifierTranslationUnit.Create(returnType),
                IdentifierTranslationUnit.Create(name));

            if (statements != null)
            {
                foreach (ITranslationUnit statement in statements)
                {
                    translationUnit.AddStatement(statement);
                }
            }

            return translationUnit;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildMemberTranslationUnit(VisibilityToken visibility, string type, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return FieldDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create(type), IdentifierTranslationUnit.Create(name));
        }
    }
}
