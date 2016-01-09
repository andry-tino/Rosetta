/// <summary>
/// MockedConstructorDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="ConstructorDeclarationTranslationUnit"/>.
    /// </summary>
    public class MockedConstructorDeclarationTranslationUnit : ConstructorDeclarationTranslationUnit
    {
        protected MockedConstructorDeclarationTranslationUnit(ConstructorDeclarationTranslationUnit original)
            : base(original)
        {
        }

        public static MockedConstructorDeclarationTranslationUnit Create(ConstructorDeclarationTranslationUnit constructorDeclarationTranslationUnit)
        {
            return new MockedConstructorDeclarationTranslationUnit(constructorDeclarationTranslationUnit);
        }

        public IEnumerable<ITranslationUnit> Statements
        {
            get { return this.statements; }
        }

        public new IEnumerable<ITranslationUnit> Arguments
        {
            get { return this.arguments; }
        }

        public new ITranslationUnit ReturnType
        {
            get { return this.returnType; }
        }
    }
}
