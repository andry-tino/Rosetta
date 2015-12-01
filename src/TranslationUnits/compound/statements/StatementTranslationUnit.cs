/// <summary>
/// StatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing modules.
    /// </summary>
    public abstract class StatementTranslationUnit : NestedElementTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatementTranslationUnit"/> class.
        /// </summary>
        protected StatementTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected StatementTranslationUnit(int nestingLevel) 
            : base(nestingLevel)
        {
        }
    }
}
