/// <summary>
/// WhiteSpaceInvariantFormatter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Formats by adding whitespaces.
    /// </summary>
    public class WhiteSpaceInvariantFormatter : WhiteSpaceFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteSpaceInvariantFormatter"/> class.
        /// </summary>
        /// <param name="indentationLevel"></param>
        /// <param name="spaceSize"></param>
        public WhiteSpaceInvariantFormatter(int indentationLevel = 0, int spaceSize = 2) 
            : base(indentationLevel, spaceSize)
        {
        }

        protected override void InitializeSpaceBefore()
        {
            string space = string.Empty;
            for (int i = 0; i < this.SpaceSize; i++)
            {
                space += " ";
            }

            this.spaceBefore = this.IndentationLevel > 0 ? space : string.Empty;
        }
    }
}
