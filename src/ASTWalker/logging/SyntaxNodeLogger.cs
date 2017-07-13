/// <summary>
/// FieldDeclarationSyntaxNodeLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST.Diagnostics.Logging
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    
    using Rosetta.Diagnostics.Logging;

    /// <summary>
    /// Logs operations concerning <see cref="CSharpSyntaxNode"/> nodes.
    /// </summary>
    public abstract class SyntaxNodeLogger
    {
        private readonly CSharpSyntaxNode parent;
        private readonly CSharpSyntaxNode node;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public SyntaxNodeLogger(CSharpSyntaxNode parent, CSharpSyntaxNode node, ILogger logger)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.node = node;
            this.parent = parent;
            this.logger = logger;
        }

        /// <summary>
        /// Logs info about the traversal of a <see cref="CSharpSyntaxNode"/>.
        /// </summary>
        /// <param name="visitor">The name of the entity performing the logging.</param>
        public void LogVisit(string visitor)
        {
            this.Log(visitor, this.ParentNodeName, this.NodeType, this.NodeName, "Visited and TU created");
        }

        protected abstract string ParentNodeName { get; }

        protected abstract string NodeType { get; }

        protected abstract string NodeName { get; }

        protected void Log(string visitor, string walker, string nodeType, string nodeName, string action)
        {
            this.logger.Log("AST Walking", visitor, walker, "reached node", nodeType, nodeName, "Action:", action);
        }

        protected CSharpSyntaxNode Node => this.node;

        protected CSharpSyntaxNode Parent => this.parent;
    }
}
