/// <summary>
/// MixedExpressions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class MixedExpressions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("MixedExpressions.SimpleArithmetic.ts")]
        public string MixedExpressionSimpleArithmetic()
        {
            // Expression: 2 * 3 + 10
            var translationUnit = BinaryExpressionTranslationUnit.Create(
                BinaryExpressionTranslationUnit.Create(
                    LiteralTranslationUnit<int>.Create(2), 
                    LiteralTranslationUnit<int>.Create(3), 
                    OperatorToken.Multiplication), 
                LiteralTranslationUnit<int>.Create(10), 
                OperatorToken.Addition);

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("MixedExpressions.SimpleParentheticArithmetic.ts")]
        public string MixedExpressionSimpleParentheticArithmetic()
        {
            // Expression: 2 * 3 + 100++ * (200 - 300)
            var translationUnit = BinaryExpressionTranslationUnit.Create(
                BinaryExpressionTranslationUnit.Create(
                    LiteralTranslationUnit<int>.Create(2),
                    LiteralTranslationUnit<int>.Create(3),
                    OperatorToken.Multiplication),
                BinaryExpressionTranslationUnit.Create(
                    UnaryExpressionTranslationUnit.Create(
                        LiteralTranslationUnit<int>.Create(100), 
                        OperatorToken.Increment, 
                        UnaryExpressionTranslationUnit.UnaryPosition.Postfix),
                    ParenthesizedExpressionTranslationUnit.Create(
                        BinaryExpressionTranslationUnit.Create(
                            LiteralTranslationUnit<int>.Create(200), 
                            LiteralTranslationUnit<int>.Create(300), 
                            OperatorToken.Subtraction)),
                    OperatorToken.Multiplication),
                OperatorToken.Addition);

            return translationUnit.Translate();
        }
    }
}
