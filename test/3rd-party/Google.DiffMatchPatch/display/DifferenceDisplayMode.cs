/// <summary>
/// DifferenceDisplayMode.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.ThirdParty.Google.DiffMatchPatch
{
    using System;

    /// <summary>
    /// Describes the available differences display modes.
    /// </summary>
    public enum DifferenceDisplayMode
    {
        /// <summary>
        /// Display deletions and insertions separately in two blocks.
        /// </summary>
        PlainTextSeparated,

        /// <summary>
        /// Visualize inline differences in HTML.
        /// </summary>
        HTML
    }
}
