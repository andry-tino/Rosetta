/// <summary>
/// Properties.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Properties
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleEmptyProperty.ts")]
        public string RenderSimpleEmptyProperty()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildPropertyTranslationUnit(
                VisibilityToken.Public, "int", "SimpleProperty");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleProperty.ts")]
        public string RenderSimpleProperty()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildPropertyTranslationUnit(
                VisibilityToken.Public,
                "int",
                "SimpleProperty",
                new ITranslationUnit[] {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1"),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.StringType, "variable2")
                },
                new ITranslationUnit[] {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1"),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.StringType, "variable2")
                });

            return translationUnit.Translate();
        }
    }
}
