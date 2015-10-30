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

    /// <summary>
    /// Mock for <see cref="ClassASTWalker"/>.
    /// </summary>
    internal class MockedClassASTWalker : ClassASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedClassASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        public MockedClassASTWalker(CSharpSyntaxNode node) 
            : base(node)
        {
        }

        /// <summary>
        /// Gets the underlying <see cref="ITranslationUnit"/> for class declaration.
        /// </summary>
        public ITranslationUnit ClassDeclarationTranslationUnit
        {
            get { return this.classDeclaration; }
        }
    }
}
