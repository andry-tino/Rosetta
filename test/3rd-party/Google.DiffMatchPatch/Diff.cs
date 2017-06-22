/// <summary>
/// Diff.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ThirdParty.Google.DiffMatchPatch
{
    using System;
    using System.Collections.Generic;

    using GoogleDMP = global::DiffMatchPatch;

    /// <summary>
    /// Entry point for using the tool.
    /// </summary>
    public static class Diff
    {
        /// <summary>
        /// Computes differences.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static DiffOutputText ComputeDifferences(string s1, string s2, DifferenceDisplayMode mode = DifferenceDisplayMode.PlainTextSeparated)
        {
            GoogleDMP.diff_match_patch dmp = new GoogleDMP.diff_match_patch();
            List<GoogleDMP.Diff> diffs = dmp.diff_main(s1, s2);

            if (diffs == null)
            {
                return new DiffOutputText { NumberOfDifferences = 0 };
            }

            return new DifferenceDisplayerFactory(mode).Create().ShowDifferences(diffs);
        }
    }
}
