/// <summary>
/// MockedInterfaceASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation.Mocks;

    /// <summary>
    /// Mock for <see cref="InterfaceASTWalker"/>.
    /// </summary>
    public class MockedInterfaceASTWalker : InterfaceASTWalker
    {
        protected MockedInterfaceASTWalker(InterfaceASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.interfaceDeclaration = MockedInterfaceDeclarationTranslationUnit.Create(this.interfaceDeclaration);
        }

        public static MockedInterfaceASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedInterfaceASTWalker(InterfaceASTWalker.Create(node));
        }

        public MockedInterfaceDeclarationTranslationUnit InterfaceDeclaration
        {
            get { return this.interfaceDeclaration as MockedInterfaceDeclarationTranslationUnit; }
        }
    }
}

