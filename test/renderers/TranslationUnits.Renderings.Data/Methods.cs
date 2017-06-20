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
        [RenderingResource("SimpleEmptyNoVisibilityMethod.ts")]
        public string RenderSimpleEmptyNoVisibilityMethod()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                ModifierTokens.None, null, "SimpleEmptyMethod");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleEmptyMethod.ts")]
        public string RenderSimpleEmptyMethod()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                ModifierTokens.Public, null, "SimpleEmptyMethod");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyPublicStaticMethod.ts")]
        public string RenderEmptyPublicStaticMethod()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                ModifierTokens.Public | ModifierTokens.Static, null, "EmptyPublicStaticMethod");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyProtectedStaticMethod.ts")]
        public string RenderEmptyProtectedStaticMethod()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                ModifierTokens.Protected | ModifierTokens.Static, null, "EmptyProtectedStaticMethod") as MethodDeclarationTranslationUnit;

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyProtectedInternalMethod.ts")]
        public string RenderEmptyProtectedInternalMethod()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                ModifierTokens.Protected | ModifierTokens.Internal, null, "EmptyProtectedInternal") as MethodDeclarationTranslationUnit;

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("EmptyProtectedInternalStaticMethod.ts")]
        public string RenderEmptyProtectedInternalStaticMethod()
        {
            var translationUnit = TranslationUnitBuilder.BuildMethodTranslationUnit(
                ModifierTokens.Protected | ModifierTokens.Internal | ModifierTokens.Static, null, "EmptyProtectedInternalStatic") as MethodDeclarationTranslationUnit;

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
                ModifierTokens.Public, Lexems.StringType, "EmptyMethodWithReturn");

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
                ModifierTokens.Public,
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
                ModifierTokens.Public, null, "EmptyMethod1Argument") as MethodDeclarationTranslationUnit;
            
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
                ModifierTokens.Public, null, "EmptyMethod2Arguments") as MethodDeclarationTranslationUnit;
            
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
                ModifierTokens.Public, null, "EmptyMethodManyArguments") as MethodDeclarationTranslationUnit;
            
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
