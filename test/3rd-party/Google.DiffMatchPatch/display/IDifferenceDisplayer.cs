/// <summary>
/// IDifferenceDisplayer.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.ThirdParty.Google.DiffMatchPatch
{
    using System;
    using System.Collections.Generic;

    using GoogleDMP = global::DiffMatchPatch;

    /// <summary>
    /// Describes a displayer for differences.
    /// </summary>
    internal interface IDifferenceDisplayer
    {
        /// <summary>
        /// Displays differences.
        /// </summary>
        /// <param name="differences"></param>
        /// <returns></returns>
        DiffOutputText ShowDifferences(IEnumerable<GoogleDMP.Diff> differences);
    }
}
