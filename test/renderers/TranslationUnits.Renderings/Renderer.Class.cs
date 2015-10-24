/// <summary>
/// ClassRenderer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    internal partial class Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("FileName.ts")]
        public string RenderSimpleClass()
        {
            VisibilityToken visibility = VisibilityToken.Public;
            ITranslationUnit translationUnit = ClassDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create("SampleClass"), null);

            return translationUnit.Translate();
        }
    }
}
