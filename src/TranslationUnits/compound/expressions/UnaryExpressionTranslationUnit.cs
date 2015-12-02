/// <summary>
/// UnaryExpressionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing unary expressions.
    /// </summary>
    public class UnaryExpressionTranslationUnit : ExpressionTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private OperatorToken operatorToken;
        private ITranslationUnit operand;
        private UnaryPosition unaryPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpressionTranslationUnit"/> class.
        /// </summary>
        protected UnaryExpressionTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpressionTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected UnaryExpressionTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.operand = null;
            this.operatorToken = OperatorToken.Undefined;
            this.unaryPosition = UnaryPosition.Postfix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="operatorToken"></param>
        /// <param name="unaryPosition"></param>
        /// <returns></returns>
        public static UnaryExpressionTranslationUnit Create(ITranslationUnit body, OperatorToken operatorToken, UnaryPosition unaryPosition)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            return new UnaryExpressionTranslationUnit(AutomaticNestingLevel)
            {
                Operand = body,
                operatorToken = operatorToken,
                unaryPosition = unaryPosition
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return new ITranslationUnit[] { this.Operand };
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
            
            writer.Write(this.unaryPosition == UnaryPosition.Postfix ? "{0}{1}" : "{1}{0}",
                this.Operand.Translate(),
                TokenUtility.ToString(this.operatorToken));

            return writer.ToString();
        }

        #region Compound translation unit methods

        private ITranslationUnit Operand
        {
            get { return this.operand; }

            set
            {
                NestedElementTranslationUnit.IncrementNestingLevel(value, this);
                this.operand = value;
            }
        }

        #endregion

        #region Types

        /// <summary>
        /// Enumerating the possible dispositions of operator and operand.
        /// </summary>
        public enum UnaryPosition
        {
            /// <summary>
            /// Operator should be postfixed to operand.
            /// </summary>
            Postfix,

            /// <summary>
            /// Operator should be prefixed to operand.
            /// </summary>
            Prefix
        }

        #endregion
    }
}
