/// <summary>
/// DiffOutputText.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.ThirdParty.Google.DiffMatchPatch
{
    using System;

    /// <summary>
    /// Describes a displayer for differences.
    /// </summary>
    public struct DiffOutputText
    {
        public int NumberOfDifferences { get; set; }

        public string Deletions { get; set; }

        public string Insertions { get; set; }
    }
}
