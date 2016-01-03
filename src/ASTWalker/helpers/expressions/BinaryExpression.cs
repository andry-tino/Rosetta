/// <summary>
/// BinaryExpression.cs
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
    /// Helper for accessing binary expressions in AST.
    /// </summary>
    internal class BinaryExpression : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public BinaryExpression(BinaryExpressionSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public BinaryExpression(BinaryExpressionSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public OperatorToken Operator
        {
            get
            {
                switch (this.BinaryExpressionSyntaxNode.Kind())
                {
                    case SyntaxKind.AddExpression:
                        return OperatorToken.Addition;
                    case SyntaxKind.SubtractExpression:
                        return OperatorToken.Subtraction;
                    case SyntaxKind.MultiplyExpression:
                        return OperatorToken.Multiplication;
                    case SyntaxKind.DivideExpression:
                        return OperatorToken.Divide;
                    case SyntaxKind.EqualsExpression:
                        return OperatorToken.Equals;
                    case SyntaxKind.NotEqualsExpression:
                        return OperatorToken.NotEquals;
                }

                return OperatorToken.Undefined;
            }
        }

        /// <summary>
        /// Gets the left hand operand.
        /// </summary>
        public ExpressionSyntax LeftHandOperand
        {
            get { return this.BinaryExpressionSyntaxNode.Left; }
        }

        /// <summary>
        /// Gets the right hand operand.
        /// </summary>
        public ExpressionSyntax RightHandOperand
        {
            get { return this.BinaryExpressionSyntaxNode.Right; }
        }

        private BinaryExpressionSyntax BinaryExpressionSyntaxNode
        {
            get { return this.syntaxNode as BinaryExpressionSyntax; }
        }
    }
}
