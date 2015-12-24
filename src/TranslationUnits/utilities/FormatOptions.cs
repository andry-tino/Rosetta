/// <summary>
/// FormatOptions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Options for <see cref="FormatWriter"/>.
    /// </summary>
    public sealed class FormatOptions
    {
        /// <summary>
        /// Gets or sets a value used to decide whether the final rendering should have a blank line or not.
        /// </summary>
        public bool ShouldWriteLastBlankLine { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatOptions"/> class.
        /// </summary>
        public FormatOptions()
        {
            this.ShouldWriteLastBlankLine = false;
        }
    }
}
