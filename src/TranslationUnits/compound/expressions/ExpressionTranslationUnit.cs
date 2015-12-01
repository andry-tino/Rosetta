/// <summary>
/// ExpressionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing modules.
    /// </summary>
    public class ExpressionTranslationUnit : NestedElementTranslationUnit
    {
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
        }
    }
}
