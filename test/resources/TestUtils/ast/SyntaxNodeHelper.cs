/// <summary>
/// SyntaxNodeHelper.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Utilities for finding nodes in a tree.
    /// </summary>
    public static class SyntaxNodeHelper
    {
        /// <summary>
        /// Gets a value indicating whether two nodes are in a parent/child relationship.
        /// </summary>
        /// <param name="node">The node to test.</param>
        /// <param name="parent">The presumed parent.</param>
        /// <returns></returns>
        public static bool IsChildOf(this SyntaxNode node, SyntaxNode parent)
        {
            for (var currentNode = node; currentNode != null; currentNode = currentNode.Parent)
            {
                if ((object)currentNode == (object)parent)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the node is included in the root node (compilation unit).
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsInRoot(this SyntaxNode node) => node.Parent == null || 
            (node.Parent.GetType() == typeof(CompilationUnitSyntax) && node.Parent.Parent == null);

        /// <summary>
        /// Gets a value indicating whether a namespace node is empty or not.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsNamespaceEmpty(this NamespaceDeclarationSyntax node)
        {
            int count = 0;

            new ASTWalkerNodeTypeOperationExecutor(node, typeof(ClassDeclarationSyntax), n => count++);
            new ASTWalkerNodeTypeOperationExecutor(node, typeof(InterfaceDeclarationSyntax), n => count++).Start();
            new ASTWalkerNodeTypeOperationExecutor(node, typeof(NamespaceDeclarationSyntax), n => count++).Start();
            new ASTWalkerNodeTypeOperationExecutor(node, typeof(EnumDeclarationSyntax), n => count++).Start();
            new ASTWalkerNodeTypeOperationExecutor(node, typeof(DelegateDeclarationSyntax), n => count++).Start();

            return count == 0;
        }
    }
}
