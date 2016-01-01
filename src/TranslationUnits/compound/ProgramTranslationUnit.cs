/// <summary>
/// ProgramTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a program.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class ProgramTranslationUnit : ITranslationUnit, ICompoundTranslationUnit
    {
        /// <summary>
        /// We might want to indent stuff in the program root node, however by default we do not do that.
        /// By using -1 we will cause all inner units to have nesting level 0.
        /// </summary>
        protected const int NestingLevel = -1;

        protected IEnumerable<ITranslationUnit> content;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramTranslationUnit"/> class.
        /// </summary>
        protected ProgramTranslationUnit()
        {
            this.content = new List<ITranslationUnit>();
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ProgramTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ProgramTranslationUnit(ProgramTranslationUnit other)
        {
            this.content = other.content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ProgramTranslationUnit Create()
        {
            return new ProgramTranslationUnit();
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return this.content;
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
                Formatter = new WhiteSpaceInvariantFormatter()
            };

            foreach (var translationUnit in this.content)
            {
                writer.WriteLine("{0}",
                    translationUnit.Translate());
            }

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddContent(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = NestingLevel + 1;
            }

            ((List<ITranslationUnit>)this.content).Add(translationUnit);
        }

        #endregion
    }
}
