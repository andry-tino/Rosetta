/// <summary>
/// Helper.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public abstract class Helper
    {
        private CSharpSyntaxNode syntaxNode;
        private SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Helper"/> class.
        /// </summary>
        /// <param name="node"></param>
        public Helper(CSharpSyntaxNode node)
        {
            this.syntaxNode = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Helper"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        public Helper(CSharpSyntaxNode node, SemanticModel semanticModel) : this(node)
        {
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Gets the semantic model.
        /// </summary>
        public SemanticModel SemanticModel => this.semanticModel;

        /// <summary>
        /// Gets the underlying syntax node.
        /// </summary>
        public CSharpSyntaxNode SyntaxNode => this.syntaxNode;
    }
}
