/// <summary>
/// MockedVariableDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="VariableDeclarationTranslationUnit"/>.
    /// </summary>
    public class MockedVariableDeclarationTranslationUnit : VariableDeclarationTranslationUnit
    {
        protected MockedVariableDeclarationTranslationUnit(VariableDeclarationTranslationUnit original)
            : base(original)
        {
        }

        public static MockedVariableDeclarationTranslationUnit Create(VariableDeclarationTranslationUnit variableDeclarationTranslationUnit)
        {
            return new MockedVariableDeclarationTranslationUnit(variableDeclarationTranslationUnit);
        }

        public ITranslationUnit Type
        {
            get { return this.type; }
        }

        public IEnumerable<ITranslationUnit> Names
        {
            get { return this.names; }
        }

        public IEnumerable<ITranslationUnit> Expressions
        {
            get { return this.expressions; }
        }
    }
}
