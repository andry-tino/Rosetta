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
        public static DiffOutputText ComputeDifferences(string s1, string s2)
        {
            GoogleDMP.diff_match_patch dmp = new GoogleDMP.diff_match_patch();
            List<GoogleDMP.Diff> diffs = dmp.diff_main(s1, s2);

            if (diffs == null || diffs.Count == 0)
            {
                return new DiffOutputText { NumberOfDifferences = 0 };
            }

            // Reconstruct texts from the deltas
            //  text1 = all deletion (-1) and equality (0).
            //  text2 = all insertion (1) and equality (0).
            var text1 = string.Empty;
            var text2 = string.Empty;

            foreach (var d in diffs)
            {
                if (d.operation == GoogleDMP.Operation.DELETE)
                    text1 += d.text;
                else if (d.operation == GoogleDMP.Operation.INSERT)
                    text2 += d.text;
                else
                {
                    text1 += d.text;
                    text2 += d.text;
                }
            }

            return new DiffOutputText
            {
                NumberOfDifferences = diffs.Count,
                Deletions = text1,
                Insertions = text2
            };
        }

        #region Types

        /// <summary>
        /// Describes the output of the diffing operation.
        /// </summary>
        public struct DiffOutputText
        {
            public int NumberOfDifferences { get; set; }

            public string Deletions { get; set; }

            public string Insertions { get; set; }
        }

        #endregion
    }
}
