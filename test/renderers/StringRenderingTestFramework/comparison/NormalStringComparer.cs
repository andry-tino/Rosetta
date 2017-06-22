/// <summary>
/// NormalStringComparer.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;
    using System.Collections.Generic;

    using DMP = Rosetta.ThirdParty.Google.DiffMatchPatch;

    /// <summary>
    /// Normal exact comparer.
    /// </summary>
    public class NormalStringComparer : IStringComparer
    {
        /// <summary>
        /// Compares 2 strings and provides results information.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CompareResult Compare(string x, string y)
        {
            DMP.Diff.DiffOutputText output = DMP.Diff.ComputeDifferences(x, y);

            var result = new CompareResult();
            result.Result = output.NumberOfDifferences == 0; // Ok if no differences

            string nl = Environment.NewLine;

            string deletions = output.Deletions != null ? output.Deletions : "none";
            string insertions = output.Insertions != null ? output.Insertions : "none";
            string description = $"Differences found.{nl}LEFT{nl}===={nl}{deletions}{nl}RIGHT{nl}===={nl}{insertions}";
            result.Description = description;

            return result;
        }
    }
}
