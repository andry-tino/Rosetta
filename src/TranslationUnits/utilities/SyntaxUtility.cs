/// <summary>
/// SyntaxUtility.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;

    /// <summary>
    /// Utility class for tokens.
    /// </summary>
    public static class SyntaxUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="separatorLast"></param>
        /// <returns></returns>
        public static string ToTokenSeparatedList(IEnumerable<string> items, string separator, bool includeSeparatorAtLast = false)
        {
            if (items == null)
            {
                return string.Empty;
            }
            if (separator == null)
            {
                throw new ArgumentNullException(nameof(separator));
            }

            string output = string.Empty;
            for (int i = 0, l = items.Count(); i < l; i++)
            {
                output += items.ElementAt(i) + (i == l - 1 ? (includeSeparatorAtLast ? separator : string.Empty) : separator);
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="includeSeparatorAtLast"></param>
        /// <returns></returns>
        public static string ToBracketEnclosedList(IEnumerable<string> items, bool includeSeparatorAtLast = false)
        {
            return string.Format("{1}{0}{2}", 
                ToTokenSeparatedList(items, Lexems.Comma, includeSeparatorAtLast), 
                Lexems.OpenRoundBracket, 
                Lexems.CloseRoundBracket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="includeSeparatorAtLast"></param>
        /// <returns></returns>
        public static string ToNewlineSemicolonSeparatedList(IEnumerable<string> items, bool includeSeparatorAtLast = false)
        {
            return ToTokenSeparatedList(items, Lexems.Semicolon + Lexems.Newline, includeSeparatorAtLast);
        }

        /// <summary>
        /// Collapses consecutive white spaces into one.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CollapseMultipleWhiteSpaces(string input)
        {
            return new Regex(@"\s+").Replace(input, " ");
        }
    }
}
