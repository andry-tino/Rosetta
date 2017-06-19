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
        protected ModifierTokens Modifiers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        protected ScopedElementTranslationUnit() 
            : this(ModifierTokens.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        /// <param name="modifiers"></param>
        protected ScopedElementTranslationUnit(ModifierTokens modifiers) 
            : this(modifiers, AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedElementTranslationUnit"/>.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="nestingLevel"></param>
        protected ScopedElementTranslationUnit(ModifierTokens modifiers, int nestingLevel) : base(nestingLevel)
        {
            this.Modifiers = modifiers;
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
            this.Modifiers = other.Modifiers;
        }
    }
}
