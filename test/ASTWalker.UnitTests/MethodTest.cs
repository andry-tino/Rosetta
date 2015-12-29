/// <summary>
/// MethodTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;
    using Rosetta.Tests.Data;
    using Rosetta.Tests.Utils;
    using Rosetta.AST.UnitTests.Mocks;

    /// <summary>
    /// Tests for <see cref="MethodASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class MethodTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        [TestMethod]
        public void VariableDeclarations()
        {
            string source = @"
                public void Method() {
                    string var1;
                    int var2;
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedMethodASTWalker.Create(methodDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.MethodDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.MethodDeclaration.Statements);
            Assert.IsTrue(astWalker.MethodDeclaration.Statements.Count() > 0);
            Assert.AreEqual(2, astWalker.MethodDeclaration.Statements.Count());

            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Statements.ElementAt(0), typeof(StatementTranslationUnit));
            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Statements.ElementAt(1), typeof(StatementTranslationUnit));
        }
    }
}
