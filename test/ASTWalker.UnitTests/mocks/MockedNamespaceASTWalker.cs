/// <summary>
/// MockedNamespaceASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Mock for <see cref="ClassASTWalker"/>.
    /// </summary>
    public class MockedNamespaceASTWalker : NamespaceASTWalker
    {
        protected MockedNamespaceASTWalker(NamespaceASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.module = MockedModuleTranslationUnit.Create(this.module);
        }

        public new static MockedNamespaceASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedNamespaceASTWalker(NamespaceASTWalker.Create(node));
        }

        public MockedModuleTranslationUnit Module
        {
            get { return this.module as MockedModuleTranslationUnit; }
        }
    }
}
