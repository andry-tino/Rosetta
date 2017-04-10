/// <summary>
/// IASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;

    /// <summary>
    /// Abstraction for building an AST from an assembly.
    /// </summary>
    public interface IASTBuilder
    {
        /// <summary>
        /// Builds the AST from the assembly.
        /// </summary>
        /// <returns>A <see cref="ASTInfo"/> mapping the types found in the assembly.</returns>
        ASTInfo Build();
    }
}
