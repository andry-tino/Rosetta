/// <summary>
/// Diff.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ThirdParty.Google.DiffMatchPatch
{
    using System;

    using GoogleDMP = global::DiffMatchPatch;

    /// <summary>
    /// 
    /// </summary>
    public class Diff
    {
        GoogleDMP.Diff diff = new GoogleDMP.Diff(GoogleDMP.Operation.EQUAL, "");
    }
}
