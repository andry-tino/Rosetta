/// <summary>
/// Statements.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    namespace ConditionalStatements
    {
        /// <summary>
        /// Conditional statements.
        /// </summary>
        public class Statements
        {
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            [RenderingResource("IfStatement.ts")]
            public string RenderIfStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildIfStatementTranslationUnit(
                    TranslationUnitBuilder.BuildLiteralTranslationUnit(true),
                    new ITranslationUnit[]
                    {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit("int", "variable1",
                        TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Addition, 12, 23)),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit("int", "variable2",
                        TranslationUnitBuilder.BuildLiteralTranslationUnit(14))
                    });

                return translationUnit.Translate();
            }

            [RenderingResource("IfElseStatement.ts")]
            public string RenderIfElseStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildIfStatementTranslationUnit(
                    TranslationUnitBuilder.BuildLiteralTranslationUnit(true),
                    new ITranslationUnit[]
                    {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit("int", "variable1",
                        TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Subtraction, 1, 2)),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit("string", "variable2",
                        TranslationUnitBuilder.BuildLiteralTranslationUnit("Hello"))
                    },
                    new ITranslationUnit[]
                    {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit("int", "variable1",
                        TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Multiplication, 100, 21))
                    });

                return translationUnit.Translate();
            }
        }
    }
}
