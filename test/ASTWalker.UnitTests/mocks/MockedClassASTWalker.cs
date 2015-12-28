/// <summary>
/// MockedClassASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Mock for <see cref="ClassASTWalker"/>.
    /// </summary>
    public class MockedClassASTWalker : ClassASTWalker
    {
        protected MockedClassASTWalker(ClassASTWalker original) 
            : base(original)
        {
            this.classDeclaration = MockedClassDeclarationTranslationUnit.Create(this.classDeclaration);
        }

        public new static MockedClassASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedClassASTWalker(ClassASTWalker.Create(node));
        }
        
        public MockedClassDeclarationTranslationUnit ClassDeclaration
        {
            get { return this.classDeclaration as MockedClassDeclarationTranslationUnit; }
        }
    }
}
