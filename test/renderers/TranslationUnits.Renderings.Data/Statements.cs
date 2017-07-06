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
            // TODO: There is a problem, in body block, no semicolons are added between statements
            //[RenderingResource("ConditionalStatements.IfStatement.ts")]
            public string RenderIfStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildIfStatementTranslationUnit(
                    TranslationUnitBuilder.BuildLiteralTranslationUnit(true),
                    new ITranslationUnit[]
                    {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1",
                        TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Addition, 12, 23)),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable2",
                        TranslationUnitBuilder.BuildLiteralTranslationUnit(14))
                    });

                return translationUnit.Translate();
            }

            // TODO: There is a problem, in body block, no semicolons are added between statements
            //[RenderingResource("ConditionalStatements.IfElseStatement.ts")]
            public string RenderIfElseStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildIfStatementTranslationUnit(
                    TranslationUnitBuilder.BuildLiteralTranslationUnit(true),
                    new ITranslationUnit[]
                    {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1",
                        TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Subtraction, 1, 2)),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.StringType, "variable2",
                        TranslationUnitBuilder.BuildLiteralTranslationUnit("Hello"))
                    },
                    new ITranslationUnit[]
                    {
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1",
                        TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Multiplication, 100, 21))
                    });

                return translationUnit.Translate();
            }
        }
    }

    namespace KeywordStatements
    {
        /// <summary>
        /// Conditional statements.
        /// </summary>
        public class Statements
        {
            [RenderingResource("KeywordStatements.BreakStatement.ts")]
            public string RenderBreakStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildBreakStatementTranslationUnit();
                return translationUnit.Translate();
            }

            [RenderingResource("KeywordStatements.ContinueStatement.ts")]
            public string RenderContinueStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildContinueStatementTranslationUnit();
                return translationUnit.Translate();
            }
        }
    }

    namespace ExpressionStatements
    {
        /// <summary>
        /// Conditional statements.
        /// </summary>
        public class Statements
        {
            [RenderingResource("ExpressionStatements.ReturnStatement.ts")]
            public string RenderReturnStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildReturnStatementTranslationUnit(
                    TranslationUnitBuilder.BuildLiteralTranslationUnit(true));
                return translationUnit.Translate();
            }

            [RenderingResource("ExpressionStatements.ReturnVoidStatement.ts")]
            public string RenderReturnVoidStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildReturnStatementTranslationUnit();
                return translationUnit.Translate();
            }

            [RenderingResource("ExpressionStatements.ThrowStatement.ts")]
            public string RenderThrowStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildThrowStatementTranslationUnit(
                    TranslationUnitBuilder.BuildNullLiteralTranslationUnit());
                return translationUnit.Translate();
            }

            [RenderingResource("ExpressionStatements.ThrowVoidStatement.ts")]
            public string RenderThrowVoidStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildThrowStatementTranslationUnit();
                return translationUnit.Translate();
            }
        }
    }
}
