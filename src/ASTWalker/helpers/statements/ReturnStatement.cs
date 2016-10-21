/// <summary>
/// ReturnStatement.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing return statements.
    /// </summary>
    public class ReturnStatement : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public ReturnStatement(ReturnStatementSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public ReturnStatement(ReturnStatementSyntax syntaxNode, SemanticModel semanticModel)
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
                return this.ReturnStatementSyntaxNode.Expression;
            }
        }
        
        private ReturnStatementSyntax ReturnStatementSyntaxNode
        {
            get { return this.syntaxNode as ReturnStatementSyntax; }
        }
    }
}
