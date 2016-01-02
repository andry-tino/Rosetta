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
            #region Conditional

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
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable1",
                        TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Addition, 12, 23)),
                    TranslationUnitBuilder.BuildVariableDeclarationTranslationUnit(Lexems.NumberType, "variable2",
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

            #endregion

            #region Keyword

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            [RenderingResource("BreakStatement.ts")]
            public string RenderBreakStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildBreakStatementTranslationUnit();
                return translationUnit.Translate();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            [RenderingResource("ContinueStatement.ts")]
            public string RenderContinueStatement()
            {
                ITranslationUnit translationUnit = TranslationUnitBuilder.BuildContinueStatementTranslationUnit();
                return translationUnit.Translate();
            }

            #endregion
        }
    }
}
