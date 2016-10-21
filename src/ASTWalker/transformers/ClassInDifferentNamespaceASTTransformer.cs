/// <summary>
/// ClassInDifferentNamespaceASTTransformer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Transformers
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Base class for transformers acting on namespaces.
    /// </summary>
    public abstract class ClassInDifferentNamespaceASTTransformer : IASTTransformer
    {
        private readonly string namespaceFullName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassInDifferentNamespaceASTTransformer"/> class.
        /// </summary>
        /// <param name="namespaceFullName">The new namespace to assign to the class.</param>
        public ClassInDifferentNamespaceASTTransformer(string namespaceFullName)
        {
            if (namespaceFullName == null)
            {
                throw new ArgumentNullException(nameof(namespaceFullName), "A namespace name must be provided!");
            }

            this.namespaceFullName = namespaceFullName;
        }

        /// <summary>
        /// Gets the namespace full name.
        /// </summary>
        protected string NamespaceFullName
        {
            get { return this.namespaceFullName; }
        }

        /// <summary>
        /// Transforms the tree.
        /// </summary>
        /// <param name="node"></param>
        public abstract void Transform(ref CSharpSyntaxNode node);
    }
}
