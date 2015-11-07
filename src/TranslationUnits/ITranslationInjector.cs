/// <summary>
/// ITranslationInjector.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Interface for describing translation elements allowing the 
    /// injection of translation components.
    /// </summary>
    /// <remarks>
    /// Injected translation units will not be present as part of the output of 
    /// <see cref="ICompoundTranslationUnit.InnerUnits"/>.
    /// </remarks>
    public interface ITranslationInjector
    {
        /// <summary>
        /// Sets the <see cref="ITranslationUnit"/> to concatenate 
        /// before the translation of the main one.
        /// </summary>
        ITranslationUnit InjectedTranslationUnitBefore { set; }

        /// <summary>
        /// Sets the <see cref="ITranslationUnit"/> to concatenate 
        /// after the translation of the main one.
        /// </summary>
        ITranslationUnit InjectedTranslationUnitAfter { set; }
    }
}
