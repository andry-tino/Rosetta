/// <summary>
/// MethodSignatures.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class MethodSignatures
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleMethodSignature.ts")]
        public string RenderSimpleEmptyMethod()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodSignatureTranslationUnit(
                ModifierTokens.Public, null, "SimpleMethodSignature");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("MethodSignatureWithReturn.ts")]
        public string RenderEmptyMethodWithReturn()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodSignatureTranslationUnit(
                ModifierTokens.Public, Lexems.StringType, "MethodSignatureWithReturn");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("MethodSignature1Argument.ts")]
        public string RenderEmptyMethod1Argument()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodSignatureTranslationUnit(
                ModifierTokens.Public, null, "MethodSignature1Argument") as MethodSignatureDeclarationTranslationUnit;

            translationUnit.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Number, IdentifierTranslationUnit.Create("param1")));

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("MethodSignature2Arguments.ts")]
        public string RenderEmptyMethod2Arguments()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodSignatureTranslationUnit(
                ModifierTokens.Public, null, "MethodSignature2Arguments") as MethodSignatureDeclarationTranslationUnit;

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
        [RenderingResource("MethodSignatureManyArguments.ts")]
        public string RenderEmptyMethodManyArguments()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodSignatureTranslationUnit(
                ModifierTokens.Public, null, "MethodSignatureManyArguments") as MethodSignatureDeclarationTranslationUnit;

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
