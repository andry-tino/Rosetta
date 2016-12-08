/// <summary>
/// MockedLocalDeclarationStatementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="LocalDeclarationStatementTranslationUnit"/>.
    /// </summary>
    public class MockedLocalDeclarationStatementTranslationUnit : LocalDeclarationStatementTranslationUnit
    {
        protected MockedLocalDeclarationStatementTranslationUnit(LocalDeclarationStatementTranslationUnit original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.variableDeclaration = MockedVariableDeclarationTranslationUnit.Create(this.variableDeclaration);
        }

        public static MockedLocalDeclarationStatementTranslationUnit Create(LocalDeclarationStatementTranslationUnit statementTranslationUnit)
        {
            return new MockedLocalDeclarationStatementTranslationUnit(statementTranslationUnit);
        }

        public ITranslationUnit Type
        {
            get { return this.VariableDeclaration.Type; }
        }

        public IEnumerable<ITranslationUnit> Names
        {
            get { return this.VariableDeclaration.Names; }
        }

        public IEnumerable<ITranslationUnit> Expressions
        {
            get { return this.VariableDeclaration.Expressions; }
        }

        private MockedVariableDeclarationTranslationUnit VariableDeclaration
        {
            get { return this.variableDeclaration as MockedVariableDeclarationTranslationUnit; }
        }
    }
}
