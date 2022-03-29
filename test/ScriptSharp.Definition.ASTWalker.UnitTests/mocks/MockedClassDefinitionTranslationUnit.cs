/// <summary>
/// MockedClassDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.UnitTests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="ClassDefinitionTranslationUnit"/>.
    /// </summary>
    public class MockedClassDefinitionTranslationUnit : ClassDefinitionTranslationUnit
    {
        protected MockedClassDefinitionTranslationUnit(ClassDefinitionTranslationUnit original)
            : base(original)
        {
        }

        public static MockedClassDefinitionTranslationUnit Create(ClassDefinitionTranslationUnit classDefinitionTranslationUnit)
        {
            return new MockedClassDefinitionTranslationUnit(classDefinitionTranslationUnit);
        }

        public IEnumerable<ITranslationUnit> MemberDeclarations
        {
            get { return this.memberDeclarations; }
        }

        public IEnumerable<ITranslationUnit> ConstructorDeclarations
        {
            get { return this.constructorDeclarations; }
        }

        public IEnumerable<ITranslationUnit> PropertyDeclarations
        {
            get { return this.propertyDeclarations; }
        }

        public IEnumerable<ITranslationUnit> MethodDeclarations
        {
            get { return this.methodDeclarations; }
        }

        public IEnumerable<ITranslationUnit> OtherDeclarations
        {
            get { return this.otherDeclarations; }
        }
    }
}
