/// <summary>
/// VisibilityUtilities.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Utilities
{
    using System;
    using Microsoft.CodeAnalysis;

    using Rosetta.Translation;

    /// <summary>
    /// Walks a class AST node.
    /// </summary>
    public static class VisibilityUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="syntaxToken"></param>
        /// <returns></returns>
        public static VisibilityToken GetVibilityToken(this SyntaxToken syntaxToken)
        {
            string value = syntaxToken.ValueText;

            switch (value)
            {
                case "public":
                    return VisibilityToken.Public;
                case "private":
                    return VisibilityToken.Private;
                case "protected":
                    return VisibilityToken.Protected;
            }

            throw new InvalidOperationException("Not recognized visibility!");
        }
    }
}
