/// <summary>
/// ExpressionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing expressions.
    /// TODO: Remove inherit from <see cref="NestedElementTranslationUnit"/>.
    /// </summary>
    public class ExpressionTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
    {
        private ITranslationUnit translationUnit;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTranslationUnit"/> class.
        /// </summary>
        /// <param name="translationUnit">The translation unit to use for rendering.</param>
        protected ExpressionTranslationUnit(ITranslationUnit translationUnit)
            : this(AutomaticNestingLevel)
        {
            this.translationUnit = translationUnit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTranslationUnit"/> class.
        /// </summary>
        protected ExpressionTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ExpressionTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.translationUnit = null;
        }

        /// <summary>
        /// This builder has a sole purpose: allowing the ability to create expressions that 
        /// translates in a particular desired way. This is more a utility mechanism!
        /// </summary>
        /// <param name="translationUnit"></param>
        /// <returns></returns>
        internal static ExpressionTranslationUnit Create(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            return new ExpressionTranslationUnit(translationUnit);
        }

        /// <summary>
        /// Override this.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Do not implement a fallback mechanism when overriding.</remarks>
        public virtual string Translate()
        {
            if (this.translationUnit == null)
            {
                throw new InvalidOperationException("The translation unit was not constructed with a translation unit!");
            }

            return this.translationUnit.Translate();
        }
    }
}
