/// <summary>
/// Modifiers.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing modifiers in AST.
    /// </summary>
    public static class Modifiers
    {
        /// <summary>
        /// Produces the right visibility token for a node.
        /// </summary>
        /// <param name="tokenList">The <see cref="SyntaxTokenList"/> containing the list of modifiers.</param>
        /// <returns></returns>
        public static VisibilityToken Get(this SyntaxTokenList tokenList)
        {
            if (tokenList == null)
            {
                throw new ArgumentNullException("tokenList");
            }

            VisibilityToken visibility = VisibilityToken.None;

            foreach (SyntaxToken token in tokenList)
            {
                if (token.ValueText.CompareTo(TokenUtility.ToString(VisibilityToken.Public)) == 0)
                {
                    visibility = visibility | VisibilityToken.Public; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(VisibilityToken.Private)) == 0)
                {
                    visibility = visibility | VisibilityToken.Private; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(VisibilityToken.Internal)) == 0)
                {
                    visibility = visibility | VisibilityToken.Internal; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(VisibilityToken.Protected)) == 0)
                {
                    visibility = visibility | VisibilityToken.Protected; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(VisibilityToken.Static)) == 0)
                {
                    visibility = visibility | VisibilityToken.Static; continue;
                }
            }

            return visibility;
        }

        /// <summary>
        /// Gets a value indicating whether the visibility is expression access to member.
        /// </summary>
        /// <param name="visibilityToken"></param>
        /// <returns></returns>
        public static bool IsExposedVisibility(this VisibilityToken visibilityToken)
        {
            return visibilityToken.HasFlag(VisibilityToken.Public) || 
                   visibilityToken.HasFlag(VisibilityToken.Internal);
        }
    }
}
