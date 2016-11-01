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
    /// Helper for accessing literal expressions in AST.
    /// </summary>
    public class LiteralExpression : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public LiteralExpression(LiteralExpressionSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public LiteralExpression(LiteralExpressionSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the literal value.
        /// </summary>
        public SyntaxToken Literal
        {
            get { return this.LiteralExpressionSyntaxNode.Token; }
        }

        private LiteralExpressionSyntax LiteralExpressionSyntaxNode
        {
            get { return this.SyntaxNode as LiteralExpressionSyntax; }
        }
    }
}
