/// <summary>
/// CastExpressionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing cast expressions.
    /// </summary>
    public class CastExpressionTranslationUnit : ExpressionTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private ITranslationUnit type;
        private ITranslationUnit body;

        /// <summary>
        /// Initializes a new instance of the <see cref="CastExpressionTranslationUnit"/> class.
        /// </summary>
        protected CastExpressionTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CastExpressionTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected CastExpressionTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.type = null;
            this.body = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="castee"></param>
        /// <returns></returns>
        public static CastExpressionTranslationUnit Create(ITranslationUnit type, ITranslationUnit castee)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (castee == null)
            {
                throw new ArgumentNullException(nameof(castee));
            }

            return new CastExpressionTranslationUnit(AutomaticNestingLevel)
            {
                Type = type,
                Castee = castee
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return new ITranslationUnit[] { this.Type, this.Castee };
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
            
            writer.Write("{0}{1}{2}{3}",
                Lexems.OpenAngularBracket,
                this.Type.Translate(),
                Lexems.CloseAngularBracket,
                this.Castee.Translate());

            return writer.ToString();
        }

        #region Compound translation unit methods

        private ITranslationUnit Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        private ITranslationUnit Castee
        {
            get { return this.body; }
            set { this.body = value; }
        }

        #endregion
    }
}
