/// <summary>
/// UnaryExpression.cs
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
    /// Helper for accessing unary expressions in AST.
    /// </summary>
    public class UnaryExpression : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public UnaryExpression(PrefixUnaryExpressionSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public UnaryExpression(PrefixUnaryExpressionSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
            this.IsPrefix = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public UnaryExpression(PostfixUnaryExpressionSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public UnaryExpression(PostfixUnaryExpressionSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
            this.IsPrefix = false;
        }

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public OperatorToken Operator
        {
            get
            {
                var kind = this.PrefixUnaryExpressionSyntaxNode != null ?
                    this.PrefixUnaryExpressionSyntaxNode.Kind() :
                    this.PostfixUnaryExpressionSyntaxNode.Kind();

                switch (kind)
                {
                    case SyntaxKind.PostIncrementExpression:
                    case SyntaxKind.PreIncrementExpression:
                        return OperatorToken.Increment;
                    case SyntaxKind.PostDecrementExpression:
                    case SyntaxKind.PreDecrementExpression:
                        return OperatorToken.Decrement;
                    case SyntaxKind.LogicalNotExpression:
                        return OperatorToken.LogicalNot;
                }

                return OperatorToken.Undefined;
            }
        }

        /// <summary>
        /// Gets the prefix/postfix type.
        /// </summary>
        public bool IsPrefix { get; private set; }

        /// <summary>
        /// Gets the left hand operand.
        /// </summary>
        public ExpressionSyntax Operand
        {
            get
            {
                return this.PrefixUnaryExpressionSyntaxNode != null ? 
                    this.PrefixUnaryExpressionSyntaxNode.Operand :
                    this.PostfixUnaryExpressionSyntaxNode.Operand;
            }
        }

        private PrefixUnaryExpressionSyntax PrefixUnaryExpressionSyntaxNode
        {
            get { return this.syntaxNode as PrefixUnaryExpressionSyntax; }
        }

        private PostfixUnaryExpressionSyntax PostfixUnaryExpressionSyntaxNode
        {
            get { return this.syntaxNode as PostfixUnaryExpressionSyntax; }
        }
    }
}
