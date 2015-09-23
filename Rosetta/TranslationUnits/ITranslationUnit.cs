/// <summary>
/// ITranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Interface for describing translation elements.
    /// </summary>
    public interface ITranslationUnit
    {
        /// <summary>
        /// Translates the unit into TypeScript.
        /// </summary>
        /// <returns>TypeScript code.</returns>
        string Translate();

        /// <summary>
        /// Gets the <see cref="TranslationHost"/> for this <see cref="ITranslationUnit"/>.
        /// </summary>
        TranslationHost Host
        {
            get;
        }
    }
}
