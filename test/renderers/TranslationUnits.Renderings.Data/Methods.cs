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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyMethod1Argument.ts")]
        public string RenderEmptyMethod1Argument()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                VisibilityToken.Public, null, "EmptyMethod1Argument") as MethodDeclarationTranslationUnit;
            
            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Number, IdentifierTranslationUnit.Create("param1")));

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyMethod2Arguments.ts")]
        public string RenderEmptyMethod2Arguments()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                VisibilityToken.Public, null, "EmptyMethod2Arguments") as MethodDeclarationTranslationUnit;
            
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
        [RenderingResource("EmptyMethodManyArguments.ts")]
        public string RenderEmptyMethodManyArguments()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                VisibilityToken.Public, null, "EmptyMethodManyArguments") as MethodDeclarationTranslationUnit;
            
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
