/// <summary>
/// TabFormatter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Formats by adding tabs.
    /// </summary>
    public class TabFormatter : IFormatter
    {
        private string tabBefore;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabFormatter"/> class.
        /// </summary>
        /// <param name="indentationLevel"></param>
        public TabFormatter(int indentationLevel = 0)
        {
            if (indentationLevel < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(indentationLevel), "Indentation level cannot be negative!");
            }

            this.IndentationLevel = indentationLevel;
            this.InitializeTabBefore();
        }

        /// <summary>
        /// 
        /// </summary>
        public int IndentationLevel
        {
            get;
            private set;
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

            return this.tabBefore + line;
        }

        private void InitializeTabBefore()
        {
            this.tabBefore = string.Empty;
            for (int i = 0; i < this.IndentationLevel; i++)
            {
                this.tabBefore += @"\t";
            }
        }
    }
}
