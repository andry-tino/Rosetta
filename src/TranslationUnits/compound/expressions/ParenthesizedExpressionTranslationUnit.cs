/// <summary>
/// CastExpressionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing parenthesis wrapped expressions.
    /// </summary>
    public class ParenthesizedExpressionTranslationUnit : ExpressionTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private ITranslationUnit body;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthesizedExpressionTranslationUnit"/> class.
        /// </summary>
        protected ParenthesizedExpressionTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthesizedExpressionTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ParenthesizedExpressionTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.body = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="castee"></param>
        /// <returns></returns>
        public static ParenthesizedExpressionTranslationUnit Create(ITranslationUnit wrappedExpression)
        {
            if (wrappedExpression == null)
            {
                throw new ArgumentNullException(nameof(wrappedExpression));
            }

            return new ParenthesizedExpressionTranslationUnit(AutomaticNestingLevel)
            {
                WrappedExpression = wrappedExpression
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return new ITranslationUnit[] { this.WrappedExpression };
            }
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            writer.Write("{0}{1}{2}",
                Lexems.OpenRoundBracket,
                this.WrappedExpression.Translate(),
                Lexems.CloseRoundBracket);

            return writer.ToString();
        }

        #region Compound translation unit methods

        private ITranslationUnit WrappedExpression
        {
            get { return this.body; }

            set
            {
                NestedElementTranslationUnit.IncrementNestingLevel(value, this);
                this.body = value;
            }
        }

        #endregion
    }
}
