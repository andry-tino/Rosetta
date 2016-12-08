/// <summary>
/// MockedNamespaceASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation.Mocks;

    /// <summary>
    /// Mock for <see cref="NamespaceASTWalker"/>.
    /// </summary>
    public class MockedNamespaceASTWalker : NamespaceASTWalker
    {
        protected MockedNamespaceASTWalker(NamespaceASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.module = MockedModuleTranslationUnit.Create(this.module);
        }

        public static MockedNamespaceASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedNamespaceASTWalker(NamespaceASTWalker.Create(node));
        }

        public MockedModuleTranslationUnit Module
        {
            get { return this.module as MockedModuleTranslationUnit; }
        }
    }
}
