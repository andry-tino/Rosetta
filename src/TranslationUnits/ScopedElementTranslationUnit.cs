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
    public abstract class ScopedElementTranslationUnit : NestedElementTranslationUnit
    {
        /// <summary>
        /// The visibility of the element.
        /// </summary>
        protected VisibilityToken Visibility { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        protected ScopedElementTranslationUnit() 
            : this(VisibilityToken.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        /// <param name="visibility"></param>
        protected ScopedElementTranslationUnit(VisibilityToken visibility) 
            : this(visibility, AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="nestingLevel"></param>
        protected ScopedElementTranslationUnit(VisibilityToken visibility, int nestingLevel) : base(nestingLevel)
        {
            this.Visibility = visibility;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        protected ScopedElementTranslationUnit(ScopedElementTranslationUnit other) 
            : base(other)
        {
            this.Visibility = other.Visibility;
        }
    }
}
