/// <summary>
/// StatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing modules.
    /// </summary>
    /// <remarks>
    /// Implementing <see cref="ITranslationUnit"/> and <see cref="ICompoundTranslationUnit"/> to provide abstraction
    /// in corresponding walker class.
    /// </remarks>
    public abstract class StatementTranslationUnit : NestedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
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

        /// <summary>
        /// TODO: Consider making abstract.
        /// </summary>
        public virtual IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// TODO: Consider making abstract.
        /// </summary>
        /// <returns></returns>
        public virtual string Translate()
        {
            return null;
        }
    }
}
