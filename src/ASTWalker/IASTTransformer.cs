/// <summary>
/// IASTTransformer.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Transformers
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Interface for describing AST transformers.
    /// </summary>
    public interface IASTTransformer
    {
        /// <summary>
        /// Transforms the tree.
        /// </summary>
        /// <param name="node">The node which will be impacted by the transformation.</param>
        void Transform(ref CSharpSyntaxNode node);
    }
}
