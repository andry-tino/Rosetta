/// <summary>
/// MockedArgumentDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation.Mocks
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="ArgumentDefinitionTranslationUnit"/>.
    /// </summary>
    public class MockedArgumentDefinitionTranslationUnit : ArgumentDefinitionTranslationUnit
    {
        protected MockedArgumentDefinitionTranslationUnit(ArgumentDefinitionTranslationUnit original)
            : base(original)
        {
        }

        public static MockedArgumentDefinitionTranslationUnit Create(ArgumentDefinitionTranslationUnit argumentDefinitionTranslationUnit)
        {
            return new MockedArgumentDefinitionTranslationUnit(argumentDefinitionTranslationUnit);
        }

        public ITranslationUnit VariableDeclaration
        {
            get { return this.variableDeclaration; }
        }
    }
}
