/// <summary>
/// Constructors.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Constructors
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleEmptyConstructor.ts")]
        public string RenderSimpleEmptyConstructor()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildConstructorTranslationUnit(
                VisibilityToken.Public, "SimpleEmptyConstructorClass");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleConstructor.ts")]
        public string RenderSimpleConstructor()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildConstructorTranslationUnit(
                VisibilityToken.Public,
                "SimpleConstructorClass",
                new ITranslationUnit[] {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1"),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.StringType, "variable2"),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(null, "variable3")
                });

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyConstructor1Argument.ts")]
        public string RenderEmptyConstructor1Argument()
        {
            var translationUnit = TranslationUnitBuilder.BuildConstructorTranslationUnit(
                VisibilityToken.Public, "EmptyConstructor1ArgumentClass") as MethodDeclarationTranslationUnit;

            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Number, IdentifierTranslationUnit.Create("param1")));

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyConstructor2Arguments.ts")]
        public string RenderEmptyConstructor2Arguments()
        {
            var translationUnit = TranslationUnitBuilder.BuildConstructorTranslationUnit(
                VisibilityToken.Public, "EmptyConstructor2ArgumentsClass") as MethodDeclarationTranslationUnit;

            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Number, IdentifierTranslationUnit.Create("param1")));
            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.String, IdentifierTranslationUnit.Create("param2")));

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyConstructorManyArguments.ts")]
        public string RenderEmptyConstructorManyArguments()
        {
            var translationUnit = TranslationUnitBuilder.BuildConstructorTranslationUnit(
                VisibilityToken.Public, "EmptyConstructorManyArgumentsClass") as MethodDeclarationTranslationUnit;

            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Number, IdentifierTranslationUnit.Create("param1")));
            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.String, IdentifierTranslationUnit.Create("param2")));
            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Boolean, IdentifierTranslationUnit.Create("param3")));
            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Any, IdentifierTranslationUnit.Create("param4")));

            return translationUnit.Translate();
        }
    }
}
