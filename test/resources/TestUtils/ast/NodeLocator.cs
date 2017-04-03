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
        /// <param name="recurse">If <c>true</c>, the search process will proceed recursively inside nodes, otherwise only the first level will be inspected.</param>
        /// <returns>A <see cref="SyntaxNode"/>.</returns>
        public SyntaxNode LocateFirst(Type nodeType, bool recurse = true)
        {
            ValidateInputType(nodeType);

            List<SyntaxNode> nodes = new List<SyntaxNode>();
            var astExecutor = new ASTWalkerNodeTypeOperationExecutor(this.Root, nodeType, (node) => nodes.Add(node), recurse);
            astExecutor.Start();

            return nodes.Count > 0 ? nodes[0] : null;
        }

        /// <summary>
        /// Gets the last occurrance of a node type in the AST.
        /// </summary>
        /// <param name="nodeType">The node type to look for.</param>
        /// <param name="recurse">If <c>true</c>, the search process will proceed recursively inside nodes, otherwise only the first level will be inspected.</param>
        /// <returns>A <see cref="SyntaxNode"/>.</returns>
        public SyntaxNode LocateLast(Type nodeType, bool recurse = true)
        {
            ValidateInputType(nodeType);

            List<SyntaxNode> nodes = new List<SyntaxNode>();
            var astExecutor = new ASTWalkerNodeTypeOperationExecutor(this.Root, nodeType, (node) => nodes.Add(node), recurse);
            astExecutor.Start();

            return nodes.Count > 0 ? nodes[nodes.Count - 1] : null;
        }

        /// <summary>
        /// Gets all occurrances of a node type in the AST.
        /// </summary>
        /// <param name="nodeType">The node type to look for.</param>
        /// <param name="recurse">If <c>true</c>, the search process will proceed recursively inside nodes, otherwise only the first level will be inspected.</param>
        /// <returns>A <see cref="SyntaxNode"/>.</returns>
        public IEnumerable<SyntaxNode> LocateAll(Type nodeType, bool recurse = true)
        {
            ValidateInputType(nodeType);

            List<SyntaxNode> nodes = new List<SyntaxNode>();
            var astExecutor = new ASTWalkerNodeTypeOperationExecutor(this.Root, nodeType, (node) => nodes.Add(node), recurse);
            astExecutor.Start();

            return nodes.ToArray();
        }

        /// <summary>
        /// Find a node given type and condition.
        /// </summary>
        /// <param name="nodeType">The node type to look for.</param>
        /// <param name="condition">The condition for picking the node or not.</param>
        /// <param name="recurse">If <c>true</c>, the search process will proceed recursively inside nodes, otherwise only the first level will be inspected.</param>
        /// <returns></returns>
        public IEnumerable<SyntaxNode> LocateAll(Type nodeType, Func<SyntaxNode, bool> condition, bool recurse = true)
        {
            var results = this.LocateAll(nodeType, recurse);

            return results.Where(node => condition(node));
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
