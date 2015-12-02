/// <summary>
/// BinaryExpressionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing binary expressions.
    /// </summary>
    public class BinaryExpressionTranslationUnit : ExpressionTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private OperatorToken operatorToken;
        private ITranslationUnit leftOperand;
        private ITranslationUnit rightOperand;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpressionTranslationUnit"/> class.
        /// </summary>
        protected BinaryExpressionTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpressionTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected BinaryExpressionTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.leftOperand = null;
            this.rightOperand = null;
            this.operatorToken = OperatorToken.Undefined;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rhand"></param>
        /// <param name="lhand"></param>
        /// <param name="operatorToken"></param>
        /// <returns></returns>
        public static BinaryExpressionTranslationUnit Create(ITranslationUnit rhand, ITranslationUnit lhand, OperatorToken operatorToken)
        {
            if (rhand == null)
            {
                throw new ArgumentNullException(nameof(rhand));
            }
            if (lhand == null)
            {
                throw new ArgumentNullException(nameof(lhand));
            }

            return new BinaryExpressionTranslationUnit(AutomaticNestingLevel)
            {
                LeftOperand = lhand,
                RightOperand = rhand,
                operatorToken = operatorToken
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return new ITranslationUnit[] { this.LeftOperand, this.RightOperand };
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

            // TODO: Use `{0}{1}{2}{1}{3}` when FormatWriter gets fixed to support repetitive placeholders
            writer.Write("{0}{1}{2}{3}{4}", 
                this.LeftOperand.Translate(),
                Lexems.Whitespace,
                TokenUtility.ToString(this.operatorToken),
                Lexems.Whitespace, 
                this.RightOperand.Translate());

            return writer.ToString();
        }

        #region Compound translation unit methods
        
        private ITranslationUnit LeftOperand
        {
            get { return this.leftOperand; }

            set
            {
                NestedElementTranslationUnit.IncrementNestingLevel(value, this);
                this.leftOperand = value;
            }
        }

        private ITranslationUnit RightOperand
        {
            get { return this.rightOperand; }

            set
            {
                NestedElementTranslationUnit.IncrementNestingLevel(value, this);
                this.rightOperand = value;
            }
        }

        #endregion
    }
}
