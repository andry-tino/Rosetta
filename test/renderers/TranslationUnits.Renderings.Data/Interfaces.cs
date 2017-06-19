/// <summary>
/// Interfaces.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Interfaces
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleEmptyInterface.ts")]
        public string RenderSimpleEmptyInterface()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildInterfaceTranslationUnit(
                ModifierTokens.Public, "SimpleEmptyInterface");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyInterfaceWithOneExtendedInterface.ts")]
        public string RenderEmptyInterfaceWithOneExtendedInterface()
        {
            var translationUnit = TranslationUnitBuilder.BuildInterfaceTranslationUnit(
                ModifierTokens.Public, "EmptyInterfaceWithOneExtendedInterface")
                as InterfaceDeclarationTranslationUnit;

            translationUnit.AddExtendedInterface("Interface1");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyInterfaceWithManyExtendedInterfaces.ts")]
        public string RenderEmptyInterfaceWithManyExtendedInterfaces()
        {
            var translationUnit = TranslationUnitBuilder.BuildInterfaceTranslationUnit(
                ModifierTokens.Public, "EmptyInterfaceWithManyExtendedInterfaces")
                as InterfaceDeclarationTranslationUnit;

            translationUnit.AddExtendedInterface("Interface1");
            translationUnit.AddExtendedInterface("Interface2");
            translationUnit.AddExtendedInterface("Interface3");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("InterfaceWithSignatures.ts")]
        public string RenderInterfaceWithSignatures()
        {
            var translationUnit = TranslationUnitBuilder.BuildInterfaceTranslationUnit(
                ModifierTokens.Public, "ClassWithEmptyMethods")
                as InterfaceDeclarationTranslationUnit;

            translationUnit.AddSignature("Method1");
            translationUnit.AddSignature("Method2");
            translationUnit.AddSignature("Method3");

            return translationUnit.Translate();
        }
    }
}
