/// <summary>
/// MockedMethodDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="MethodDeclarationTranslationUnit"/>.
    /// </summary>
    public class MockedMethodDeclarationTranslationUnit : MethodDeclarationTranslationUnit
    {
        protected MockedMethodDeclarationTranslationUnit(MethodDeclarationTranslationUnit original)
            : base(original)
        {
        }

        public static MockedMethodDeclarationTranslationUnit Create(MethodDeclarationTranslationUnit methodDeclarationTranslationUnit)
        {
            return new MockedMethodDeclarationTranslationUnit(methodDeclarationTranslationUnit);
        }

        public IEnumerable<ITranslationUnit> Statements
        {
            get { return this.statements; }
        }
    }
}
