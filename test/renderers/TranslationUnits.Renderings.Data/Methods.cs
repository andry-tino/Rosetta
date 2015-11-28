/// <summary>
/// Methods.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Methods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleEmptyMethod.ts")]
        public string RenderSimpleEmptyMethod()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                VisibilityToken.Public, null, "SimpleEmptyMethod");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyMethodWithReturn.ts")]
        public string RenderEmptyMethodWithReturn()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                VisibilityToken.Public, "string", "EmptyMethodWithReturn");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleMethod.ts")]
        public string RenderSimpleMethod()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                VisibilityToken.Public,
                null,
                "SimpleMethod",
                new ITranslationUnit[] {
                    VariableDeclarationTranslationUnit.Create(IdentifierTranslationUnit.Create("int"), IdentifierTranslationUnit.Create("variable1")),
                    VariableDeclarationTranslationUnit.Create(IdentifierTranslationUnit.Create("string"), IdentifierTranslationUnit.Create("variable2")),
                    VariableDeclarationTranslationUnit.Create(null, IdentifierTranslationUnit.Create("variable3"))
                });

            return translationUnit.Translate();
        }
    }
}
