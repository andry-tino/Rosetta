/// <summary>
/// StatementsGroupTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a group of statements.
    /// </summary>
    public class StatementsGroupTranslationUnit : NestedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private IEnumerable<ITranslationUnit> statements;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsGroupTranslationUnit"/> class.
        /// </summary>
        protected StatementsGroupTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsGroupTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected StatementsGroupTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.statements = new List<ITranslationUnit>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StatementsGroupTranslationUnit Create()
        {
            return new StatementsGroupTranslationUnit(AutomaticNestingLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return this.statements;
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
            
            foreach (var statement in this.statements)
            {
                writer.WriteLine("{0}{1}",
                    statement.Translate(),
                    Lexems.Semicolon);
            }

            return writer.ToString();
        }

        #region Compound translation unit methods
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public void AddStatement(ITranslationUnit statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            // Group of statements does not add one more nesting level, so we do not need to
            // increase the nesting level for each added unit.

            ((List<ITranslationUnit>)this.statements).Add(statement);
        }

        #endregion
    }
}
