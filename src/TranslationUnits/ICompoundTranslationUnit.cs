/// <summary>
/// ICompoundTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for describing compound translation elements.
    /// </summary>
    public interface ICompoundTranslationUnit
    {
        /// <summary>
        /// Gets the list of inner units.
        /// </summary>
        IEnumerable<ITranslationUnit> InnerUnits
        {
            get;
        }
    }
}
