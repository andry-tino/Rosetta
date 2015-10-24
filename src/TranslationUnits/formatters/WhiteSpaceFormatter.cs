/// <summary>
/// WhiteSpaceFormatter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Formats by adding tabs.
    /// </summary>
    public class WhiteSpaceFormatter : IFormatter
    {
        private string spaceBefore;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteSpaceFormatter"/> class.
        /// </summary>
        /// <param name="indentationLevel"></param>
        /// <param name="spaceSize"></param>
        public WhiteSpaceFormatter(int indentationLevel = 0, int spaceSize = 2)
        {
            if (indentationLevel < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(indentationLevel), "Indentation level cannot be negative!");
            }

            this.IndentationLevel = indentationLevel;
            this.SpaceSize = spaceSize;
            this.InitializeSpaceBefore();
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
        /// 
        /// </summary>
        public int SpaceSize
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

            return this.spaceBefore + line;
        }

        private void InitializeSpaceBefore()
        {
            string space = string.Empty;
            for (int i = 0; i < this.SpaceSize; i++)
            {
                space += " ";
            }

            this.spaceBefore = string.Empty;
            for (int i = 0; i < this.IndentationLevel; i++)
            {
                this.spaceBefore += space;
            }
        }
    }
}
