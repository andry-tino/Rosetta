/// <summary>
/// ExpressionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing expressions.
    /// </summary>
    public class ExpressionTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
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

        /// <summary>
        /// Override this.
        /// </summary>
        /// <returns></returns>
        public virtual string Translate()
        {
            throw new NotImplementedException();
        }
    }
}
