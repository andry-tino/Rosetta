/// <summary>
/// IASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Interface for describing AST walkers.
    /// </summary>
    public interface IASTWalker
    {
        /// <summary>
        /// Performs the walking process.
        /// </summary>
        /// <returns>An <see cref="ITranslationUnit"/> representing the translation unit generated out of the tree.</returns>
        ITranslationUnit Walk();
    }
}
