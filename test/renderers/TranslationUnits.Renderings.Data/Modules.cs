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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleClassInModule.ts")]
        public string RenderSimpleClassInModule()
        {
            var moduleTranslationUnit = TranslationUnitBuilder.BuildModuleTranslationUnit("MyNamespace") as ModuleTranslationUnit;

            var classTranslationUnit = moduleTranslationUnit.AddClass("MyClass", "MyBaseClass");

            var method1 = classTranslationUnit.AddMethod(null, "Method1");
            var method2 = classTranslationUnit.AddMethod(Lexems.StringType, "Method2");
            var method3 = classTranslationUnit.AddMethod(Lexems.NumberType, "Method3");
            var method4 = classTranslationUnit.AddMethod(null, "Method4");

            method1.AddVariable(null, "var1");
            method1.AddVariable(Lexems.StringType, "var1");
            method2.AddVariable(null, "var1");
            method1.AddVariable(Lexems.NumberType, "var2");
            method1.AddVariable(Lexems.StringType, "var3");
            method3.AddVariable(Lexems.NumberType, "var1");
            method4.AddVariable(null, "var1");

            return moduleTranslationUnit.Translate();
        }
    }
}
