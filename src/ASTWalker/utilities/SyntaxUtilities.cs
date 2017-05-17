/// <summary>
/// SyntaxUtilities.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST.Utilities
{
    using System;
    using System.Linq;

    /// <summary>
    /// Utilities for syntax.
    /// </summary>
    public static class SyntaxUtilities
    {
        public static string StripNamespaceFromTypeName(this string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name == string.Empty)
            {
                return string.Empty;
            }

            return name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).Last();
        }
    }
}
