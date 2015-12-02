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
    }
}
