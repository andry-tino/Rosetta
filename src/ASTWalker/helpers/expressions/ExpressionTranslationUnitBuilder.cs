/// <summary>
/// ExpressionTranslationUnitBuilder.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Builder responsible for creating the correct <see cref="ITranslationUnit"/> 
    /// from an expression syntax node.
    /// This is the main entry point for whatever AST walker which needs to create an expression.
    /// </summary>
    internal sealed class ExpressionTranslationUnitBuilder
    {
        private ExpressionSyntax node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTranslationUnitBuilder"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public ExpressionTranslationUnitBuilder(ExpressionSyntax node)
        {
            this.node = node;
        }

        /// <summary>
        /// Builds the proper <see cref="ITranslationUnit"/> for the specific expression.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ITranslationUnit Build()
        {
            switch (this.node.Kind())
            {
                // Binary expressions
                case SyntaxKind.AddExpression:
                case SyntaxKind.MultiplyExpression:
                case SyntaxKind.DivideExpression:
                case SyntaxKind.SubtractExpression:
                case SyntaxKind.EqualsExpression:
                case SyntaxKind.NotEqualsExpression:
                    var binaryExpression = this.node as BinaryExpressionSyntax;
                    if (binaryExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected binary expression to binary expression!");
                    }
                    return BuildBinaryExpressionTranslationUnit(binaryExpression);

                // Unary expressions
                case SyntaxKind.PostIncrementExpression:
                case SyntaxKind.PreIncrementExpression:
                case SyntaxKind.PostDecrementExpression:
                case SyntaxKind.PreDecrementExpression:
                case SyntaxKind.LogicalNotExpression:
                    var prefixUnaryExpression = this.node as PrefixUnaryExpressionSyntax;
                    var postfixUnaryExpression = this.node as PostfixUnaryExpressionSyntax;
                    if (prefixUnaryExpression == null && postfixUnaryExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected unary expression to unary expression!");
                    }
                    return prefixUnaryExpression != null ? 
                        BuildUnaryExpressionTranslationUnit(prefixUnaryExpression) : 
                        BuildUnaryExpressionTranslationUnit(postfixUnaryExpression);

                // Literal expressions
                case SyntaxKind.NumericLiteralExpression:
                case SyntaxKind.StringLiteralExpression:
                case SyntaxKind.NullLiteralExpression:
                case SyntaxKind.CharacterLiteralExpression:
                case SyntaxKind.FalseLiteralExpression:
                case SyntaxKind.TrueLiteralExpression:
                    var literalExpression = this.node as LiteralExpressionSyntax;
                    if (literalExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected literal expression to literal expression!");
                    }
                    return BuildLiteralExpressionTranslationUnit(literalExpression);

                // Parenthetical
                case SyntaxKind.ParenthesizedExpression:
                    var parenthesizedExpression = this.node as ParenthesizedExpressionSyntax;
                    if (parenthesizedExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected parenthesized expression to parenthesized expression!");
                    }
                    return BuildParenthesizedExpressionTranslationUnit(parenthesizedExpression);

                // Instance expressions
                case SyntaxKind.ThisExpression:
                case SyntaxKind.BaseExpression:
                    return null;
            }

            return null;
        }

        private static ITranslationUnit BuildBinaryExpressionTranslationUnit(BinaryExpressionSyntax expression)
        {
            OperatorToken token = OperatorToken.Undefined;

            switch (expression.Kind())
            {
                case SyntaxKind.AddExpression:
                    token = OperatorToken.Addition;
                    break;
                case SyntaxKind.MultiplyExpression:
                    token = OperatorToken.Multiplication;
                    break;
                case SyntaxKind.DivideExpression:
                    token = OperatorToken.Divide;
                    break;
                case SyntaxKind.SubtractExpression:
                    token = OperatorToken.Subtraction;
                    break;
                case SyntaxKind.EqualsExpression:
                    token = OperatorToken.Equals;
                    break;
                case SyntaxKind.NotEqualsExpression:
                    token = OperatorToken.NotEquals;
                    break;
            }

            if (token == OperatorToken.Undefined)
            {
                throw new InvalidOperationException("Binary operator could not be detected!");
            }

            BinaryExpression binaryExpressionHelper = new BinaryExpression(expression);
            ITranslationUnit leftHandOperand = new ExpressionTranslationUnitBuilder(binaryExpressionHelper.LeftHandOperand).Build();
            ITranslationUnit rightHandOperand = new ExpressionTranslationUnitBuilder(binaryExpressionHelper.RightHandOperand).Build();

            return BinaryExpressionTranslationUnit.Create(leftHandOperand, rightHandOperand, token);
        }

        private static ITranslationUnit BuildUnaryExpressionTranslationUnit(PrefixUnaryExpressionSyntax expression)
        {
            OperatorToken token = OperatorToken.Undefined;

            switch (expression.Kind())
            {
                case SyntaxKind.PreIncrementExpression:
                    token = OperatorToken.Increment;
                    break;
                case SyntaxKind.PreDecrementExpression:
                    token = OperatorToken.Decrement;
                    break;
                case SyntaxKind.LogicalNotExpression:
                    token = OperatorToken.LogicalNot;
                    break;
            }

            if (token == OperatorToken.Undefined)
            {
                throw new InvalidOperationException("Unary operator could not be detected!");
            }

            UnaryExpression unaryExpressionHelper = new UnaryExpression(expression);
            ITranslationUnit operand = new ExpressionTranslationUnitBuilder(unaryExpressionHelper.Operand).Build();

            return UnaryExpressionTranslationUnit.Create(operand, token, UnaryExpressionTranslationUnit.UnaryPosition.Prefix);
        }

        private static ITranslationUnit BuildUnaryExpressionTranslationUnit(PostfixUnaryExpressionSyntax expression)
        {
            OperatorToken token = OperatorToken.Undefined;

            switch (expression.Kind())
            {
                case SyntaxKind.PostIncrementExpression:
                    token = OperatorToken.Increment;
                    break;
                case SyntaxKind.PostDecrementExpression:
                    token = OperatorToken.Decrement;
                    break;
            }

            if (token == OperatorToken.Undefined)
            {
                throw new InvalidOperationException("Unary operator could not be detected!");
            }

            UnaryExpression unaryExpressionHelper = new UnaryExpression(expression);
            ITranslationUnit operand = new ExpressionTranslationUnitBuilder(unaryExpressionHelper.Operand).Build();

            return UnaryExpressionTranslationUnit.Create(operand, token, UnaryExpressionTranslationUnit.UnaryPosition.Postfix);
        }

        private static ITranslationUnit BuildLiteralExpressionTranslationUnit(LiteralExpressionSyntax expression)
        {
            SyntaxToken token = expression.Token;

            switch (token.Kind())
            {
                case SyntaxKind.NumericLiteralToken:
                    return LiteralTranslationUnit<int>.Create((int)token.Value);

                case SyntaxKind.StringLiteralToken:
                    return LiteralTranslationUnit<string>.Create((string)token.Value);

                case SyntaxKind.CharacterLiteralExpression:
                    return null;

                case SyntaxKind.TrueKeyword:
                case SyntaxKind.FalseKeyword:
                    return LiteralTranslationUnit<bool>.Create((bool)token.Value);

                case SyntaxKind.NullKeyword:
                    return LiteralTranslationUnit.Null;
            }

            return null;
        }

        private static ITranslationUnit BuildParenthesizedExpressionTranslationUnit(ParenthesizedExpressionSyntax expression)
        {
            ParenthesizedExpression parenthesizedExpressionHelper = new ParenthesizedExpression(expression);

            return ParenthesizedExpressionTranslationUnit.Create(
                new ExpressionTranslationUnitBuilder(parenthesizedExpressionHelper.Expression).Build());
        }
    }
}
