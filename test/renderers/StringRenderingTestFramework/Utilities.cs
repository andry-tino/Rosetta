/// <summary>
/// Utils.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;
    using System.Text;

    /// <summary>
    /// Multi-purpose test utils.
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Converts a boolean test result into a string.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToTestPassResult(this bool result) => result ? "Pass" : "Fail";

        /// <summary>
        /// Prints a separator line.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="char"></param>
        /// <returns></returns>
        public static string PrintSeparator(int length, string @char = "-")
        {
            var sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(@char);
            }

            return sb.ToString();
        }
    }
}
