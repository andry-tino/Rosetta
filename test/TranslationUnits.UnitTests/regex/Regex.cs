/// <summary>
/// Regex.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests.Regex
{
    using System;

    /// <summary>
    /// Contains common typeScript regex elements.
    /// </summary>
    internal static partial class Regex
    {
        /// <summary>
        /// Matches an identifier.
        /// </summary>
        public static string Identifier
        {
            get
            {
                return @"[a-zA-Z][a-zA-Z0-9]*";
            }
        }

        /// <summary>
        /// Matches a visibility token.
        /// </summary>
        public static string VisibilityModifier
        {
            get
            {
                return @"public|private|protected";
            }
        }
    }
}
