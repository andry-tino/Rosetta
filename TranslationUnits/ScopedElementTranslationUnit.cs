/// <summary>
/// ScopedElementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing scoped elements.
    /// </summary>
    public abstract class ScopedElementTranslationUnit
    {
        /// <summary>
        /// The visibility of the element.
        /// </summary>
        protected VisibilityToken Visibility { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        protected ScopedElementTranslationUnit()
        {
            this.Visibility = VisibilityToken.None;
        }
    }
}
