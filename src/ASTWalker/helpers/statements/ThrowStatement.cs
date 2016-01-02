/// <summary>
/// ThrowStatement.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing throw statements.
    /// </summary>
    internal class ThrowStatement : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public ThrowStatement(ThrowStatementSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public ThrowStatement(ThrowStatementSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public ExpressionSyntax Expression
        {
            get
            {
                return this.ThrowStatementSyntaxNode.Expression;
            }
        }

        private ThrowStatementSyntax ThrowStatementSyntaxNode
        {
            get { return this.syntaxNode as ThrowStatementSyntax; }
        }
    }
}
