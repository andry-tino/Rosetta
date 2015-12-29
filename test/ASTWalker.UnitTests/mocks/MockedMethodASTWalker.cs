/// <summary>
/// MockedMethodASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Mock for <see cref="MethodASTWalker"/>.
    /// </summary>
    public class MockedMethodASTWalker : MethodASTWalker
    {
        protected MockedMethodASTWalker(MethodASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.methodDeclaration = MockedMethodDeclarationTranslationUnit.Create(this.methodDeclaration);
        }

        public new static MockedMethodASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedMethodASTWalker(MethodASTWalker.Create(node));
        }

        public MockedMethodDeclarationTranslationUnit MethodDeclaration
        {
            get { return this.methodDeclaration as MockedMethodDeclarationTranslationUnit; }
        }
    }
}
