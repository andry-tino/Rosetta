/// <summary>
/// Expressions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    namespace BinaryExpressions
    {
        /// <summary>
        /// 
        /// </summary>
        public class Expressions
        {
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            [RenderingResource("BinaryExpression.IntegerSum.ts")]
            public string BinaryExpressionIntegerSum()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Addition, 1, 2);

                return translationUnit.Translate();
            }
        }
    }
}
