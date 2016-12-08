/// <summary>
/// EnumASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Mocks
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    
    using Rosetta.Translation.Mocks;

    /// <summary>
    /// Walks an interface AST node.
    /// </summary>
    public class MockedEnumASTWalker : EnumASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedEnumASTWalker"/> class.
        /// </summary>
        /// <param name="original"></param>
        protected MockedEnumASTWalker(EnumASTWalker original)
            : base(original)
        {
            this.enumDeclaration = MockedEnumTranslationUnit.Create(this.enumDeclaration);
        }

        public static MockedEnumASTWalker Create(CSharpSyntaxNode node)
        {
            return new MockedEnumASTWalker(EnumASTWalker.Create(node));
        }

        public MockedEnumTranslationUnit EnumDeclaration
        {
            get { return this.enumDeclaration as MockedEnumTranslationUnit; }
        }
    }
}
