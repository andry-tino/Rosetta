/// <summary>
/// IFormatter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Interface for describing components handling code output formatting.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Formats a line.
        /// </summary>
        /// <param name="line">The line to format.</param>
        /// <returns>Formatted line.</returns>
        string FormatLine(string line);
    }
}
