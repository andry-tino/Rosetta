/// <summary>
/// MockedConditionalStatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="ConditionalStatementTranslationUnit"/>.
    /// </summary>
    public class MockedConditionalStatementTranslationUnit : ConditionalStatementTranslationUnit
    {
        protected MockedConditionalStatementTranslationUnit(ConditionalStatementTranslationUnit original)
            : base(original)
        {
        }

        public static MockedConditionalStatementTranslationUnit Create(ConditionalStatementTranslationUnit statementTranslationUnit)
        {
            return new MockedConditionalStatementTranslationUnit(statementTranslationUnit);
        }

        public IEnumerable<ITranslationUnit> TestExpressions
        {
            get { return this.testExpressions; }
        }

        public IEnumerable<ITranslationUnit> Bodies
        {
            get { return this.bodies; }
        }

        public ITranslationUnit LastBody
        {
            get { return this.lastBody; }
        }

        public bool HasFinalElse
        {
            get { return this.hasFinalElse; }
        }
    }
}
