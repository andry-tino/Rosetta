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
        private ITranslationUnit[] testExpressions;
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
        /// We can have N bodies.
        /// </summary>
        private StatementsGroupTranslationUnit[] bodies;
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
        /// Total bodies is N + 1 in case we have final ELSE clause.
        /// </summary>
        private StatementsGroupTranslationUnit lastBody;

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
                bodies = new StatementsGroupTranslationUnit[blocksNumber],
                lastBody = hasFinalElse ? StatementsGroupTranslationUnit.Create() : null
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

            if (this.lastBody != null)
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
            if (testExpression as ExpressionTranslationUnit != null)
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
        public void AddStatementInConditionalBlock(ITranslationUnit statement, int index)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }
            if (statement as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)statement).NestingLevel = this.NestingLevel + 1;
            }
            if (index < 0 || index >= this.bodies.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (this.bodies[index] == null)
            {
                this.bodies[index] = StatementsGroupTranslationUnit.Create();
            }

            if (statement as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)statement).NestingLevel = this.NestingLevel + 1;
            }

            this.bodies[index].AddStatement(statement);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement">Can be a block or a statement.</param>
        public void AddStatementInElseBlock(ITranslationUnit statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }
            if (statement as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)statement).NestingLevel = this.NestingLevel + 1;
            }

            if (this.lastBody == null)
            {
                throw new InvalidOperationException("This conditional translation unit has been created without final ELSE support!");
            }

            if (statement as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)statement).NestingLevel = this.NestingLevel + 1;
            }

            this.lastBody.AddStatement(statement);
        }

        #endregion
    }
}
