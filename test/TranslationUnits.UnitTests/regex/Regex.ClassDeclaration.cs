/// <summary>
/// Regex.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests.Regex
{
    using System;

    /// <summary>
    /// Includes TypeScript regex for classes.
    /// </summary>
    internal static partial class Regex
    {
        /// <summary>
        /// Matches a class declaration.
        /// </summary>
        public static string ClassDeclaration
        {
            get
            {
                return string.Concat(
                    VisibilityModifier,
                    @"? class ",
                    Identifier,
                    @"\{.*\}"
                    );
            }
        }
    }
}
