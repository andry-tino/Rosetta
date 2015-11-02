/// <summary>
/// CodePerfect.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;

    /// <summary>
    /// Utility class fixing code format.
    /// </summary>
    public static class CodePerfect
    {
        /// <summary>
        /// Removes all sequences of consecutive whitespaces in order 
        /// to ensure that two words are never separated by more 
        /// than one single whitespace character.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EnsureOnlyOneWhiteSpace(this string input)
        {
            return Regex.Replace(input, @"\s{2,}", match => " ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EnsureNoHeadingTrailingWhiteSpaces(this string input)
        {
            return input.Trim();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ClassDeclarationCodePerfect
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RefineDeclaration(this string input)
        {
            return input
                .EnsureNoHeadingTrailingWhiteSpaces()
                .EnsureOnlyOneWhiteSpace();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MethodDeclarationCodePerfect
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RefineDeclaration(this string input)
        {
            return input
                .EnsureNoHeadingTrailingWhiteSpaces()
                .EnsureOnlyOneWhiteSpace();
        }
    }
}
