/// <summary>
/// NodeLocator.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;
    using System.Linq;
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
        /// 
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public SyntaxNode LocateFirst(Type nodeType)
        {
            return null;
        }
    }
}
