/// <summary>
/// ConditionalStatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing conditional statements.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class ConditionalStatementTranslationUnit : StatementTranslationUnit
    {
        /// <summary>
        /// Contains all test expressions:
        /// <code>
        /// if (<test-expr-1>) {
        /// } else if (<test-expr-2>) {
        /// } else if (<test-expr-3>) {
        /// } else { }
        /// </code>
        /// N is the number of test expressions.
        /// </summary>
        protected ITranslationUnit[] testExpressions;
        /// <summary>
        /// Contains all bodies:
        /// <code>
        /// if (...) {
        ///     <body-1>
        /// } else if (...) {
        ///     <body-2>
        /// } else if (...) {
        ///     <body-3>
        /// } else {...}
        /// </code>
        /// We can have N bodies and they can be blocks or single statements.
        /// </summary>
        protected ITranslationUnit[] bodies;
        /// <summary>
        /// Contains all bodies:
        /// <code>
        /// if (...) { ...
        /// } else if (...) { ...
        /// } else if (...) { ...
        /// } else {
        ///     <last-body>
        /// }
        /// </code>
        /// Total bodies is N + 1 in case we have final ELSE clause and it can be a block or a single statement.
        /// </summary>
        protected ITranslationUnit lastBody;

        protected bool hasFinalElse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalStatementTranslationUnit"/> class.
        /// </summary>
        protected ConditionalStatementTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalStatementTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ConditionalStatementTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.testExpressions = null;
            this.bodies = null;
            this.lastBody = null;
            this.hasFinalElse = false;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ConditionalStatementTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        protected ConditionalStatementTranslationUnit(ConditionalStatementTranslationUnit other) 
            : base(other)
        {
            this.testExpressions = other.testExpressions;
            this.bodies = other.bodies;
            this.lastBody = other.lastBody;
            this.hasFinalElse = other.hasFinalElse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blocksNumber"></param>
        /// <param name="hasFinalElse"></param>
        /// <returns></returns>
        public static ConditionalStatementTranslationUnit Create(int blocksNumber, bool hasFinalElse)
        {
            return new ConditionalStatementTranslationUnit(AutomaticNestingLevel)
            {
                testExpressions = new ITranslationUnit[blocksNumber],
                bodies = new ITranslationUnit[blocksNumber],
                lastBody = null,
                hasFinalElse = hasFinalElse
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return null;
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

            for (var i = 0; i < this.testExpressions.Length; i++)
            {
                // Opening
                writer.WriteLine("{0} {1}{2}{3}",
                    i == 0 ? Lexems.IfKeyword : Lexems.ElseIfKeyword,
                    Lexems.OpenRoundBracket,
                    this.testExpressions[i].Translate(),
                    Lexems.CloseRoundBracket);
                writer.WriteLine("{0}",
                    Lexems.OpenCurlyBracket);

                writer.WriteLine("{0}",
                    this.bodies[i].Translate());

                // Closing
                writer.WriteLine("{0}",
                    Lexems.CloseCurlyBracket);
            }

            if (this.hasFinalElse)
            {
                // Opening
                writer.WriteLine("{0}",
                    Lexems.ElseKeyword);
                writer.WriteLine("{0}",
                    Lexems.OpenCurlyBracket);

                writer.WriteLine("{0}",
                    this.lastBody.Translate());

                // Closing
                writer.WriteLine("{0}",
                    Lexems.CloseCurlyBracket);
            }

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testExpression"></param>
        /// <param name="index"></param>
        public void SetTestExpression(ITranslationUnit testExpression, int index)
        {
            if (testExpression == null)
            {
                throw new ArgumentNullException(nameof(testExpression));
            }
            if (testExpression as ExpressionTranslationUnit == null)
            {
                throw new ArgumentException(nameof(testExpression), "Expected an expression!");
            }
            if (index < 0 || index >= this.testExpressions.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            this.testExpressions[index] = testExpression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement">Can be a block or a statement.</param>
        /// <param name="index"></param>
        public void SetStatementInConditionalBlock(ITranslationUnit statement, int index)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }
            if (index < 0 || index >= this.bodies.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (statement as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)statement).NestingLevel = this.NestingLevel + 1;
            }

            this.bodies[index] = statement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement">Can be a block or a statement.</param>
        public void SetStatementInElseBlock(ITranslationUnit statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            // TODO: Ad unit test for this
            if (!this.hasFinalElse)
            {
                throw new InvalidOperationException("This conditional translation unit has been created without final ELSE support!");
            }

            if (statement as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)statement).NestingLevel = this.NestingLevel + 1;
            }

            this.lastBody = statement;
        }

        #endregion
    }
}
