/// <summary>
/// PlainTextSeparatedDifferenceDisplayer.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.ThirdParty.Google.DiffMatchPatch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GoogleDMP = global::DiffMatchPatch;

    /// <summary>
    /// Describes a displayer for differences.
    /// </summary>
    internal class PlainTextSeparatedDifferenceDisplayer : IDifferenceDisplayer
    {
        /// <summary>
        /// Displays differences.
        /// </summary>
        /// <param name="differences"></param>
        /// <returns></returns>
        public DiffOutputText ShowDifferences(IEnumerable<GoogleDMP.Diff> differences)
        {
            if (differences == null)
            {
                throw new ArgumentNullException(nameof(differences));
            }

            if (differences.Count() == 0)
            {
                return new DiffOutputText { NumberOfDifferences = 0 };
            }

            // Reconstruct texts from the deltas
            //  text1 = all deletion (-1) and equality (0).
            //  text2 = all insertion (1) and equality (0).
            var text1 = string.Empty;
            var text2 = string.Empty;

            foreach (var d in differences)
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
                NumberOfDifferences = differences.Count(),
                Deletions = text1,
                Insertions = text2
            };
        }
    }
}
