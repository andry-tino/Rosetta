/// <summary>
/// NodeFinder.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Utilities for finding nodes in a tree.
    /// </summary>
    public static class NodeFinder<T> where T : SyntaxNode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T GetNode(string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(syntaxTree).LocateFirst(typeof(T));
            if (node == null)
            {
                throw new InvalidOperationException($"Node of type `{typeof(T).Name}` should be found!");
            }

            return node as T;
        }
    }
}
