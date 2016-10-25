/// <summary>
/// ASTWalkerContext.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;

    /// <summary>
    /// Defines the walking context.
    /// </summary>
    public class ASTWalkerContext
    {
        /// <summary>
        /// Gets or sets the originating <see cref="IASTWalker"/>.
        /// </summary>
        public IASTWalker Originator { get; set; }
    }
}
