/// <summary>
/// IdentifierExpression.cs
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
    /// Helper for accessing identifier expressions in AST.
    /// </summary>
    public class IdentifierExpression : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public IdentifierExpression(IdentifierNameSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public IdentifierExpression(IdentifierNameSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public string Identifier
        {
            get { return this.IdentifierNameSyntaxNode.Identifier.ValueText; }
        }

        private IdentifierNameSyntax IdentifierNameSyntaxNode
        {
            get { return this.syntaxNode as IdentifierNameSyntax; }
        }
    }
}
