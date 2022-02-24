/// <summary>
/// SyntaxUtility.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Utility class for tokens.
    /// </summary>
    public static class SyntaxUtility
    {
        /// <summary>
        /// Outputs the name in the correct ScriptSharp convention:
        /// - First letter must be lowercase.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToScriptSharpName(this string name, bool preserveCase = false)
        {
            if (string.IsNullOrEmpty(name)) { return name; }
            return preserveCase
                ? name
                : CreateCamelCaseName(name);
        }

        /// <summary>
        /// Copied directly from https://github.com/nikhilk/scriptsharp/blob/d91350021b68d01ceb15ee0c706b835e36aba284/src/Core/Compiler/Utility.cs#L36
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateCamelCaseName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return name;
            }

            // Some exceptions that simply need to be special cased
            if (name.Equals("ID", StringComparison.Ordinal))
            {
                return "id";
            }

            bool hasLowerCase = false;
            int conversionLength = 0;

            for (int i = 0; i < name.Length; i++)
            {
                if (Char.IsUpper(name, i))
                {
                    conversionLength++;
                }
                else
                {
                    hasLowerCase = true;
                    break;
                }
            }

            if (((hasLowerCase == false) && (name.Length != 1)) || (conversionLength == 0))
            {
                // Name is all upper case, or all lower case;
                // leave it as-is.
                return name;
            }

            if (conversionLength > 1)
            {
                // Convert the leading uppercase segment, except the last character
                // which is assumed to be the first letter of the next word
                return name.Substring(0, conversionLength - 1).ToLower(CultureInfo.InvariantCulture) +
                       name.Substring(conversionLength - 1);
            }
            else if (name.Length == 1)
            {
                return name.ToLower(CultureInfo.InvariantCulture);
            }
            else
            {
                // Convert the leading upper case character to lower case
                return Char.ToLower(name[0], CultureInfo.InvariantCulture) + name.Substring(1);
            }
        }
    }
}
