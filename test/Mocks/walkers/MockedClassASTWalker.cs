/// <summary>
/// MockedClassASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Mocks
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation.Mocks;

    /// <summary>
    /// Mock for <see cref="ClassASTWalker"/>.
    /// </summary>
    public class MockedClassASTWalker : ClassASTWalker
    {
        protected MockedClassASTWalker(ClassASTWalker original) 
            : base(original)
        {
            // Reassigning since base class operated on it
            this.classDeclaration = MockedClassDeclarationTranslationUnit.Create(this.classDeclaration);
        }

        public static MockedClassASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedClassASTWalker(ClassASTWalker.Create(node));
        }
        
        public MockedClassDeclarationTranslationUnit ClassDeclaration
        {
            get { return this.classDeclaration as MockedClassDeclarationTranslationUnit; }
        }
    }
}
