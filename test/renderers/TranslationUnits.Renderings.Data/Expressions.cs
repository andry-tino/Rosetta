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
        public class Expressions
        {
            [RenderingResource("BinaryExpression.IntegerSum.ts")]
            public string BinaryExpressionIntegerSum()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Addition, 1, 2);

                return translationUnit.Translate();
            }
        }
    }

    namespace LiteralExpressions
    {
        public class Expressions
        {
            [RenderingResource("LiteralExpression.Integer.ts")]
            public string LiteralExpressionInteger()
            {
                var translationUnit = TranslationUnitBuilder.BuildLiteralTranslationUnit(1);

                return translationUnit.Translate();
            }
            
            [RenderingResource("LiteralExpression.Boolean.ts")]
            public string LiteralExpressionBoolean()
            {
                var translationUnit = TranslationUnitBuilder.BuildLiteralTranslationUnit(true);

                return translationUnit.Translate();
            }
            
            [RenderingResource("LiteralExpression.Null.ts")]
            public string LiteralExpressionNull()
            {
                var translationUnit = TranslationUnitBuilder.BuildNullLiteralTranslationUnit();

                return translationUnit.Translate();
            }
        }
    }

    namespace UnaryExpressions
    {
        public class Expressions
        {
            [RenderingResource("UnaryExpression.PostfixIncrement.ts")]
            public string UnaryExpressionPostfixIncrement()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(OperatorToken.Increment, 1, true);

                return translationUnit.Translate();
            }
            
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
        public class Expressions
        {
            [RenderingResource("CastExpression.NativeCast.ts")]
            public string CastExpressionNativeCast()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit("int", "myVariable");

                return translationUnit.Translate();
            }
        }
    }

    namespace InvokationExpressions
    {
        public class Expressions
        {
            [RenderingResource("InvokationExpressions.ParameterlessInvokationNull.ts")]
            public string InvokationExpressionParameterlessInvokationNull()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(
                    IdentifierTranslationUnit.Create("myMethod"), null);

                return translationUnit.Translate();
            }

            [RenderingResource("InvokationExpressions.ParameterlessInvokation.ts")]
            public string InvokationExpressionParameterlessInvokation()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(
                    IdentifierTranslationUnit.Create("myMethod"), new ITranslationUnit[] { });

                return translationUnit.Translate();
            }

            [RenderingResource("InvokationExpressions.OneParameter.ts")]
            public string InvokationExpressionOneParameter()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(
                    IdentifierTranslationUnit.Create("myMethod"), new ITranslationUnit[] 
                    {
                        TranslationUnitBuilder.BuildLiteralTranslationUnit("string1")
                    });

                return translationUnit.Translate();
            }

            [RenderingResource("InvokationExpressions.TwoParameters.ts")]
            public string InvokationExpressionTwoParameters()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(
                    IdentifierTranslationUnit.Create("myMethod"), new ITranslationUnit[]
                    {
                        TranslationUnitBuilder.BuildLiteralTranslationUnit("string1"),
                        TranslationUnitBuilder.BuildLiteralTranslationUnit(100)
                    });

                return translationUnit.Translate();
            }

            [RenderingResource("InvokationExpressions.ThreeParameters.ts")]
            public string InvokationExpressionThreeParameters()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit(
                    IdentifierTranslationUnit.Create("myMethod"), new ITranslationUnit[]
                    {
                        TranslationUnitBuilder.BuildLiteralTranslationUnit("string1"),
                        TranslationUnitBuilder.BuildLiteralTranslationUnit(100),
                        TranslationUnitBuilder.BuildLiteralTranslationUnit("string2")
                    });

                return translationUnit.Translate();
            }
        }
    }

    namespace ParenthesizedExpressions
    {
        public class Expressions
        {
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
        public class Expressions
        {
            [RenderingResource("MemberAccessExpressions.This.ts")]
            public string MemberAccessExpressionThis()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit("MyMember",
                    MemberAccessExpressionTranslationUnit.MemberAccessMethod.This);

                return translationUnit.Translate();
            }
            
            [RenderingResource("MemberAccessExpressions.Base.ts")]
            public string MemberAccessExpressionBase()
            {
                var translationUnit = TranslationUnitBuilder.BuildExpressionTranslationUnit("MyMember",
                    MemberAccessExpressionTranslationUnit.MemberAccessMethod.Base);

                return translationUnit.Translate();
            }
            
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
