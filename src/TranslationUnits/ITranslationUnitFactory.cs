/// <summary>
/// ITranslationUnitFactory.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Interface for describing factories for <see cref="ITranslationUnit"/>.
    /// </summary>
    /// <remarks>
    /// Usually, classes implementing <see cref="ITranslationUnit"/> will expose a factory method. However in most 
    /// of the cases the factory method alone is not necessary to set the translation unit with all the value that 
    /// are required before utilizing it, that is when we use factories implementing this interface.
    /// </remarks>
    public interface ITranslationUnitFactory
    {
        /// <summary>
        /// Creates a <see cref="ITranslationUnit"/>.
        /// </summary>
        ITranslationUnit Create();
    }
}
