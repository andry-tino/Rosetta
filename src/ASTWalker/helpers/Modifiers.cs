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
        public static ModifierTokens Get(this SyntaxTokenList tokenList)
        {
            if (tokenList == null)
            {
                throw new ArgumentNullException("tokenList");
            }

            ModifierTokens visibility = ModifierTokens.None;

            foreach (SyntaxToken token in tokenList)
            {
                if (token.ValueText.CompareTo(TokenUtility.ToString(ModifierTokens.Public)) == 0)
                {
                    visibility = visibility | ModifierTokens.Public; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(ModifierTokens.Private)) == 0)
                {
                    visibility = visibility | ModifierTokens.Private; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(ModifierTokens.Internal)) == 0)
                {
                    visibility = visibility | ModifierTokens.Internal; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(ModifierTokens.Protected)) == 0)
                {
                    visibility = visibility | ModifierTokens.Protected; continue;
                }
                if (token.ValueText.CompareTo(TokenUtility.ToString(ModifierTokens.Static)) == 0)
                {
                    visibility = visibility | ModifierTokens.Static; continue;
                }
            }

            return visibility;
        }

        /// <summary>
        /// Gets a value indicating whether the visibility is expression access to member.
        /// </summary>
        /// <param name="visibilityToken"></param>
        /// <returns></returns>
        public static bool IsExposedVisibility(this ModifierTokens visibilityToken)
        {
            return visibilityToken.HasFlag(ModifierTokens.Public) || 
                   visibilityToken.HasFlag(ModifierTokens.Internal);
        }
    }
}
