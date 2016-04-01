/// <summary>
/// ExpressionStatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing an expression based statement.
    /// </summary>
    /// <remarks>
    public class ExpressionStatementTranslationUnit : StatementTranslationUnit
    {
        protected ExpressionTranslationUnit expression;
        protected string keyword;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionStatementTranslationUnit"/> class.
        /// </summary>
        protected ExpressionStatementTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionStatementTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ExpressionStatementTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.expression = null;
            this.keyword = null;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ExpressionStatementTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ExpressionStatementTranslationUnit(ExpressionStatementTranslationUnit other)
            : base(other)
        {
            this.expression = other.expression;
            this.keyword = other.keyword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static ExpressionStatementTranslationUnit Create(ExpressionTranslationUnit expression, string keyword = null)
        {
            ExpressionTranslationUnit realExpression = expression ?? ExpressionTranslationUnit.Create(VoidTranslationUnit.Create());

            return new ExpressionStatementTranslationUnit(AutomaticNestingLevel)
            {
                expression = realExpression,
                keyword = keyword
            };
        }

        /// <summary>
        /// Builds a return statement with the specified expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ExpressionStatementTranslationUnit CreateReturn(ExpressionTranslationUnit expression)
        {
            return Create(expression, Lexems.ReturnKeyword);
        }

        /// <summary>
        /// Builds a void return statement.
        /// </summary>
        /// <returns></returns>
        public static ExpressionStatementTranslationUnit CreateReturn()
        {
            return Create(null, Lexems.ReturnKeyword);
        }

        /// <summary>
        /// Builds a throw statement with the specified expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ExpressionStatementTranslationUnit CreateThrow(ExpressionTranslationUnit expression)
        {
            return Create(expression, Lexems.ThrowKeyword);
        }

        /// <summary>
        /// Builds a void throw statement.
        /// </summary>
        /// <returns></returns>
        public static ExpressionStatementTranslationUnit CreateThrow()
        {
            return Create(null, Lexems.ThrowKeyword);
        }

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return new ITranslationUnit[] { this.expression };
            }
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public override string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            writer.Write("{0}{1}",
                this.keyword != null ? keyword + " " : string.Empty,
                this.expression.Translate());

            return writer.ToString();
        }
    }
}
