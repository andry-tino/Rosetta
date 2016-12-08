/// <summary>
/// MockedProgramASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation.Mocks;

    /// <summary>
    /// Mock for <see cref="ProgramASTWalker"/>.
    /// </summary>
    public class MockedProgramASTWalker : ProgramASTWalker
    {
        protected MockedProgramASTWalker(ProgramASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.program = MockedProgramTranslationUnit.Create(this.program);
        }

        public static MockedProgramASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedProgramASTWalker(ProgramASTWalker.Create(node));
        }

        public MockedProgramTranslationUnit Program
        {
            get { return this.program as MockedProgramTranslationUnit; }
        }
    }
}
