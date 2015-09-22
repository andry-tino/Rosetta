/// <summary>
/// ICompoundTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Interface for describing compound translation elements.
    /// </summary>
    public interface ICompoundTranslationUnit
    {
        /// <summary>
        /// Gets the list of inner units.
        /// </summary>
        ITranslationUnit[] InnerUnits
        {
            get;
        }
    }
}
