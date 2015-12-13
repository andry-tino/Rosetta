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
    /// Class describing a group of statements.
    /// </summary>
    public class StetementsGroupTranslationUnit : NestedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private IEnumerable<ITranslationUnit> statements;

        /// <summary>
        /// Initializes a new instance of the <see cref="StetementsGroupTranslationUnit"/> class.
        /// </summary>
        protected StetementsGroupTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StetementsGroupTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected StetementsGroupTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.statements = new List<ITranslationUnit>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StetementsGroupTranslationUnit Create()
        {
            return new StetementsGroupTranslationUnit(AutomaticNestingLevel);
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
            if (statement as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)statement).NestingLevel = this.NestingLevel + 1;
            }

            ((List<ITranslationUnit>)this.statements).Add(statement);
        }

        #endregion
    }
}
