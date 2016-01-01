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
                VisibilityToken.Public, Lexems.StringType, "EmptyMethodWithReturn");

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
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1"),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.StringType, "variable2"),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(null, "variable3")
                });

            return translationUnit.Translate();
        }
    }
}
