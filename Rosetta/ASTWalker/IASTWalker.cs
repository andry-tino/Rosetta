/// <summary>
/// IASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation;

    /// <summary>
    /// Interface for describing AST walkers.
    /// </summary>
    public interface IASTWalker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        ITranslationUnit Walk(CSharpSyntaxNode node);
    }
}
