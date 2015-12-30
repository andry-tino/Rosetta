﻿/// <summary>
/// MockedConditionalStatementASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Mock for <see cref="ConditionalStatementASTWalker"/>.
    /// </summary>
    public class MockedConditionalStatementASTWalker : ConditionalStatementASTWalker
    {
        protected MockedConditionalStatementASTWalker(ConditionalStatementASTWalker original)
            : base(original)
        {
            // Reassigning since base class operated on it
            this.statement = MockedConditionalStatementTranslationUnit.Create(base.Statement);
        }

        public new static MockedConditionalStatementASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedConditionalStatementASTWalker(ConditionalStatementASTWalker.Create(node));
        }

        public new MockedConditionalStatementTranslationUnit Statement
        {
            get { return this.statement as MockedConditionalStatementTranslationUnit; }
        }
    }
}
