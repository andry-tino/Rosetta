/// <summary>
/// ASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Walks a class AST node.
    /// // TODO: Override class definition in order to create an inner class and remove the node!
    /// </summary>
    public abstract class ASTWalker : CSharpSyntaxWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        private ASTWalkerContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        protected ASTWalker(CSharpSyntaxNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.node = node;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ASTWalker(ASTWalker other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            this.node = other.node;
        }

        /// <summary>
        /// Gets or sets the walking context.
        /// </summary>
        public ASTWalkerContext Context
        {
            get { return this.context; }

            set
            {
                this.context = value;
                this.OnContextChanged();
            }
        }

        /// <summary>
        /// Called when the context has changed. When accessing the context, it is already the new context, 
        /// no information is provided in this method about the old one.
        /// </summary>
        protected virtual void OnContextChanged()
        {

        }
    }
}
