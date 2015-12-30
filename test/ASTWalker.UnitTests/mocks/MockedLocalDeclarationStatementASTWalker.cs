/// <summary>
/// MockedLocalDeclarationStatementASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Mock for <see cref="LocalDeclarationStatementASTWalker"/>.
    /// </summary>
    public class MockedLocalDeclarationStatementASTWalker : LocalDeclarationStatementASTWalker
    {
        protected MockedLocalDeclarationStatementASTWalker(LocalDeclarationStatementASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.statement = MockedLocalDeclarationStatementTranslationUnit.Create(base.Statement);
        }

        public new static MockedLocalDeclarationStatementASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedLocalDeclarationStatementASTWalker(LocalDeclarationStatementASTWalker.Create(node));
        }

        public new MockedLocalDeclarationStatementTranslationUnit Statement
        {
            get { return this.statement as MockedLocalDeclarationStatementTranslationUnit; }
        }
    }
}
