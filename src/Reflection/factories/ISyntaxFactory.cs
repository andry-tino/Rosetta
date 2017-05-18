/// <summary>
/// ISyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Factories
{
    using System;

    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Abstract common funcitonalities of syntax factories.
    /// </summary>
    public interface ISyntaxFactory
    {

        /// <summary>
        /// Creates a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <returns></returns>
        SyntaxNode Create();
    }
}
