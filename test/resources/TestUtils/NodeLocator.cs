/// <summary>
/// NodeLocator.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Locates nodes in an AST.
    /// </summary>
    public class NodeLocator
    {
        public SyntaxNode Root { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeLocator"/> class.
        /// </summary>
        /// <param name="syntaxTree"></param>
        public NodeLocator(SyntaxTree syntaxTree)
        {
            this.Root = syntaxTree.GetRoot();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeLocator"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public NodeLocator(SyntaxNode syntaxNode)
        {
            this.Root = syntaxNode;
        }

        /// <summary>
        /// Gets the first occurrance of a node type in the AST.
        /// </summary>
        /// <param name="nodeType">The node type to look for.</param>
        /// <returns>A <see cref="SyntaxNode"/>.</returns>
        public SyntaxNode LocateFirst(Type nodeType)
        {
            ValidateInputType(nodeType);

            List<SyntaxNode> nodes = new List<SyntaxNode>();
            var astExecutor = new ASTWalkerNodeTypeOperationExecutor(this.Root, nodeType, (node) => nodes.Add(node));
            astExecutor.Start();

            return nodes.Count > 0 ? nodes[0] : null;
        }

        /// <summary>
        /// Gets all occurrances of a node type in the AST.
        /// </summary>
        /// <param name="nodeType">The node type to look for.</param>
        /// <returns>A <see cref="SyntaxNode"/>.</returns>
        public IEnumerable<SyntaxNode> LocateAll(Type nodeType)
        {
            ValidateInputType(nodeType);

            List<SyntaxNode> nodes = new List<SyntaxNode>();
            var astExecutor = new ASTWalkerNodeTypeOperationExecutor(this.Root, nodeType, (node) => nodes.Add(node));
            astExecutor.Start();

            return nodes.ToArray();
        }

        private static void ValidateInputType(Type type)
        {
            if (!type.IsSubclassOf(typeof(SyntaxNode)))
            {
                throw new InvalidOperationException("Type should be a subclass of `SyntaxNode`!");
            }
        }
    }
}
