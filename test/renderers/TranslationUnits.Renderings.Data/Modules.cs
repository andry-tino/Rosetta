/// <summary>
/// Modules.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Modules
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleEmptyClassInModule.ts")]
        public string RenderSimpleEmptyClassInModule()
        {
            var translationUnit = TranslationUnitBuilder.BuildModuleTranslationUnit("MyNamespace") as ModuleTranslationUnit;

            translationUnit.AddClass("SimpleClass");

            return translationUnit.Translate();
        }
    }
}
