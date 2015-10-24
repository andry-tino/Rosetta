/// <summary>
/// NullFormatter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Does not format.
    /// </summary>
    public class NullFormatter : IFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullFormatter"/> class.
        /// </summary>
        public NullFormatter()
        {
        }

        /// <summary>
        /// Formats a line.
        /// </summary>
        /// <param name="line">The line to format.</param>
        /// <returns>Formatted line.</returns>
        public string FormatLine(string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            return line;
        }
    }
}
