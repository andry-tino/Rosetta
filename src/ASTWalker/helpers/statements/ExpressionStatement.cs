/// <summary>
/// ExpressionStatement.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing expression statements.
    /// </summary>
    public class ExpressionStatement : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public ExpressionStatement(ExpressionStatementSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public ExpressionStatement(ExpressionStatementSyntax syntaxNode, SemanticModel semanticModel)
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
                return this.ExpressionStatementSyntaxNode.Expression;
            }
        }

        private ExpressionStatementSyntax ExpressionStatementSyntaxNode
        {
            get { return this.syntaxNode as ExpressionStatementSyntax; }
        }
    }
}
