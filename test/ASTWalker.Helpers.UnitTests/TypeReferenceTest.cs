/// <summary>
/// TypeReferenceTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ASTWalker.Helpers.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.ASTWalker.Helpers;
    using Rosetta.Tests.Utils;

    [TestClass]
    public class TypeReferenceTest
    {
        private static SyntaxNode Class1SyntaxTree;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Creating needed resources
            Class1SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class1).GetRoot();
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        [TestMethod]
        public void TypeName()
        {
            SyntaxNode node = new NodeLocator(Class1SyntaxTree).LocateFirst(typeof(SyntaxNode));
        }
    }
}
