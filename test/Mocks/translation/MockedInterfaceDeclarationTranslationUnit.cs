/// <summary>
/// MockedInterfaceDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="InterfaceDeclarationTranslationUnit"/>.
    /// </summary>
    public class MockedInterfaceDeclarationTranslationUnit : InterfaceDeclarationTranslationUnit
    {
        protected MockedInterfaceDeclarationTranslationUnit(InterfaceDeclarationTranslationUnit original)
            : base(original)
        {
        }

        public static MockedInterfaceDeclarationTranslationUnit Create(InterfaceDeclarationTranslationUnit interfaceDeclarationTranslationUnit)
        {
            return new MockedInterfaceDeclarationTranslationUnit(interfaceDeclarationTranslationUnit);
        }

        public IEnumerable<ITranslationUnit> Signatures
        {
            get { return this.signatures; }
        }
    }
}
