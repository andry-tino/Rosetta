/// <summary>
/// MockedProgramTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="ProgramTranslationUnit"/>.
    /// </summary>
    public class MockedProgramTranslationUnit : ProgramTranslationUnit
    {
        protected MockedProgramTranslationUnit(ProgramTranslationUnit original)
            : base(original)
        {
        }

        public static MockedProgramTranslationUnit Create(ProgramTranslationUnit programTranslationUnit)
        {
            return new MockedProgramTranslationUnit(programTranslationUnit);
        }

        public IEnumerable<ITranslationUnit> Content
        {
            get { return this.content; }
        }
    }
}
