/// <summary>
/// MockedConstructorASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Mock for <see cref="ConstructorASTWalker"/>.
    /// </summary>
    public class MockedConstructorASTWalker : ConstructorASTWalker
    {
        protected MockedConstructorASTWalker(ConstructorASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.constructorDeclaration = MockedConstructorDeclarationTranslationUnit.Create(this.constructorDeclaration);
        }

        public new static MockedConstructorASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedConstructorASTWalker(ConstructorASTWalker.Create(node));
        }

        public MockedConstructorDeclarationTranslationUnit ConstructorDeclaration
        {
            get { return this.constructorDeclaration as MockedConstructorDeclarationTranslationUnit; }
        }
    }
}
