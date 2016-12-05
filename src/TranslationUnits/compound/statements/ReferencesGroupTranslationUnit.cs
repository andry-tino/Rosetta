/// <summary>
/// ReferencesGroupTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class bridging <see cref="StatementsGroupTranslationUnit"/> and <see cref="ReferenceTranslationUnit"/>.
    /// </summary>
    public class ReferencesGroupTranslationUnit : StatementsGroupTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencesGroupTranslationUnit"/> class.
        /// </summary>
        protected ReferencesGroupTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencesGroupTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ReferencesGroupTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ReferencesGroupTranslationUnit Create()
        {
            return new ReferencesGroupTranslationUnit(AutomaticNestingLevel);
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public override void AddStatement(ITranslationUnit statement)
        {
            if (statement.GetType() != typeof(ReferenceTranslationUnit))
            {
                throw new ArgumentException($"Expecting type: {typeof(ReferenceTranslationUnit).Name}", nameof(statement));
            }

            base.AddStatement(statement);
        }

        #endregion

        protected override bool ShouldRenderSemicolon(ITranslationUnit statement)
        {
            return false;
        }
    }
}
