/// <summary>
/// LocalDeclarationStatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a local declaration statement.
    /// </summary>
    /// <remarks>
    /// Acts like a decorator for <see cref="VariableDeclarationTranslationUnit"/>.
    /// </remarks>
    public class LocalDeclarationStatementTranslationUnit : StatementTranslationUnit
    {
        private VariableDeclarationTranslationUnit variableDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDeclarationStatementTranslationUnit"/> class.
        /// </summary>
        protected LocalDeclarationStatementTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDeclarationStatementTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected LocalDeclarationStatementTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.variableDeclaration = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static LocalDeclarationStatementTranslationUnit Create(VariableDeclarationTranslationUnit variableDeclaration)
        {
            if (variableDeclaration == null)
            {
                throw new ArgumentNullException(nameof(variableDeclaration));
            }

            return new LocalDeclarationStatementTranslationUnit(AutomaticNestingLevel)
            {
                variableDeclaration = variableDeclaration
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return new ITranslationUnit[] { this.variableDeclaration };
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
                this.variableDeclaration.Translate());

            return writer.ToString();
        }
    }
}
