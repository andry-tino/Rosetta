/// <summary>
/// SymbolNotFoundException.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Exception thrown by helpers when a symbol could not be found in the <see cref="SemanticModel"/>.
    /// </summary>
    public class SymbolNotFoundException : Exception
    {
        protected SyntaxNode node;
        protected SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolNotFoundException"/> class.
        /// </summary>
        public SymbolNotFoundException(SyntaxNode node, SemanticModel semanticModel)
            : base()
        {
            this.node = node;
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public override string Message => $"Could not find symbol information for node {node.ToString()} in semantic model!";
    }
}
