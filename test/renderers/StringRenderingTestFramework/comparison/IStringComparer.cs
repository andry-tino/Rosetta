/// <summary>
/// IStringComparer.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;

    /// <summary>
    /// Describes a comparer.
    /// </summary>
    public interface IStringComparer
    {
        /// <summary>
        /// Compares 2 strings and provides results information.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        CompareResult Compare(string x, string y);
    }
}
