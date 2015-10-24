/// <summary>
/// Renderer.Class.cs
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
        [RenderingResource("SimpleEmptyClass.ts")]
        public string RenderSimpleEmptyClass()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildClassTranslationUnit(
                VisibilityToken.Public, "SimpleEmptyClass", null);

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyClassWithInheritance.ts")]
        public string RenderEmptyClassWithInheritance()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildClassTranslationUnit(
                VisibilityToken.Public, "EmptyClassWithInheritance", "BaseClass");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyClassWithOneInterface.ts")]
        public string RenderEmptyClassWithOneInterface()
        {
            var translationUnit = TranslationUnitBuilder.BuildClassTranslationUnit(
                VisibilityToken.Public, "EmptyClassWithOneInterface", null)
                as ClassDeclarationTranslationUnit;

            translationUnit.AddInterface("Interface1");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyClassWithManyInterface.ts")]
        public string RenderEmptyClassWithManyInterface()
        {
            var translationUnit = TranslationUnitBuilder.BuildClassTranslationUnit(
                VisibilityToken.Public, "EmptyClassWithManyInterface", null)
                as ClassDeclarationTranslationUnit;

            translationUnit.AddInterface("Interface1");
            translationUnit.AddInterface("Interface2");
            translationUnit.AddInterface("Interface3");

            return translationUnit.Translate();
        }
    }
}
