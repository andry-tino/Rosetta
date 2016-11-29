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
        /// <param name="tree">The tree which will be impacted by the transformation.</param>
        void Transform(ref CSharpSyntaxTree tree);

        /// <summary>
        /// Transforms the tree and the semantic model.
        /// </summary>
        /// <param name="tree">The tree which will be impacted by the transformation.</param>
        /// <param name="compilation">The compilation containing the semantic model associated to the node.</param>
        void Transform(ref CSharpSyntaxTree tree, ref CSharpCompilation compilation);
    }
}
