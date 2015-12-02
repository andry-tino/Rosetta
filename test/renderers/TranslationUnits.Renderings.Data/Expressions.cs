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

    namespace UnaryExpressions
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
            [RenderingResource("UnaryExpression.PostfixIncrement.ts")]
            public string UnaryExpressionPostfixIncrement()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Increment, 1, true);

                return translationUnit.Translate();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            [RenderingResource("UnaryExpression.PrefixIncrement.ts")]
            public string UnaryExpressionPrefixIncrement()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Increment, 1, false);

                return translationUnit.Translate();
            }
        }
    }

    namespace CastExpressions
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
            [RenderingResource("CastExpression.NativeCast.ts")]
            public string CastExpressionNativeCast()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit("int", "myVariable");

                return translationUnit.Translate();
            }
        }
    }

    namespace ParenthesizedExpressions
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
            [RenderingResource("ParenthesizedExpressions.VariableWrapping.ts")]
            public string ParenthesizedExpressionVariableWrapping()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(IdentifierTranslationUnit.Create("myVariable"));

                return translationUnit.Translate();
            }
        }
    }

    namespace MemberAccessExpressions
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
            [RenderingResource("MemberAccessExpressions.This.ts")]
            public string MemberAccessExpressionThis()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit("MyMember",
                    MemberAccessExpressionTranslationUnit.MemberAccessMethod.This);

                return translationUnit.Translate();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            [RenderingResource("MemberAccessExpressions.Base.ts")]
            public string MemberAccessExpressionBase()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit("MyMember",
                    MemberAccessExpressionTranslationUnit.MemberAccessMethod.Base);

                return translationUnit.Translate();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            [RenderingResource("MemberAccessExpressions.None.ts")]
            public string MemberAccessExpressionNone()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit("MyMember",
                    MemberAccessExpressionTranslationUnit.MemberAccessMethod.None);

                return translationUnit.Translate();
            }
        }
    }
}
