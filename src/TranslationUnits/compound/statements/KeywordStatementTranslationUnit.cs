/// <summary>
/// KeywordStatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a keyword based statement.
    /// </summary>
    /// <remarks>
    /// Acts like a decorator for <see cref="VariableDeKeywordStatementTranslationUnitclarationTranslationUnit"/>.
    /// Internal members protected for testability.
    /// </remarks>
    public class KeywordStatementTranslationUnit : StatementTranslationUnit
    {
        protected string keyword;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordStatementTranslationUnit"/> class.
        /// </summary>
        protected KeywordStatementTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordStatementTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected KeywordStatementTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.keyword = string.Empty;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="keyword"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public KeywordStatementTranslationUnit(KeywordStatementTranslationUnit other)
            : base(other)
        {
            this.keyword = other.keyword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static KeywordStatementTranslationUnit Create(string keyword)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException(nameof(keyword));
            }

            return new KeywordStatementTranslationUnit(AutomaticNestingLevel)
            {
                keyword = keyword
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public static KeywordStatementTranslationUnit Break
        {
            get { return KeywordStatementTranslationUnit.Create(Lexems.BreakKeyword); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static KeywordStatementTranslationUnit Continue
        {
            get { return KeywordStatementTranslationUnit.Create(Lexems.ContinueKeyword); }
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

            writer.Write("{0}",
                keyword);

            return writer.ToString();
        }
    }
}
