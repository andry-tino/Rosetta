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
    }
}
