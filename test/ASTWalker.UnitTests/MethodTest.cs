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
    using Rosetta.AST.Mocks;

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
        public void MethodWithVariableDeclarations()
        {
            string source = @"
                public void Method() {
                    string var1;
                    int var2;
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

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

        [TestMethod]
        public void EmptyMethodWithNoParameters()
        {
            string source = @"
                public void Method() { }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedMethodASTWalker.Create(methodDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.MethodDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.MethodDeclaration.Arguments);
            Assert.AreEqual(0, astWalker.MethodDeclaration.Arguments.Count());
        }

        [TestMethod]
        public void EmptyMethodWith1Parameter()
        {
            string source = @"
                public void Method(string param1) { }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedMethodASTWalker.Create(methodDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.MethodDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.MethodDeclaration.Arguments);
            Assert.IsTrue(astWalker.MethodDeclaration.Arguments.Count() > 0);
            Assert.AreEqual(1, astWalker.MethodDeclaration.Arguments.Count());

            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Arguments.ElementAt(0), typeof(ArgumentDefinitionTranslationUnit));
        }

        [TestMethod]
        public void EmptyMethodWith2Parameters()
        {
            string source = @"
                public void Method(string param1, int param2) { }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedMethodASTWalker.Create(methodDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.MethodDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.MethodDeclaration.Arguments);
            Assert.IsTrue(astWalker.MethodDeclaration.Arguments.Count() > 0);
            Assert.AreEqual(2, astWalker.MethodDeclaration.Arguments.Count());

            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Arguments.ElementAt(0), typeof(ArgumentDefinitionTranslationUnit));
            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Arguments.ElementAt(1), typeof(ArgumentDefinitionTranslationUnit));
        }

        [TestMethod]
        public void EmptyMethodWith3Parameters()
        {
            string source = @"
                public void Method(string param1, int param2, bool param3) { }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedMethodASTWalker.Create(methodDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.MethodDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.MethodDeclaration.Arguments);
            Assert.IsTrue(astWalker.MethodDeclaration.Arguments.Count() > 0);
            Assert.AreEqual(3, astWalker.MethodDeclaration.Arguments.Count());

            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Arguments.ElementAt(0), typeof(ArgumentDefinitionTranslationUnit));
            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Arguments.ElementAt(1), typeof(ArgumentDefinitionTranslationUnit));
            Assert.IsInstanceOfType(astWalker.MethodDeclaration.Arguments.ElementAt(2), typeof(ArgumentDefinitionTranslationUnit));
        }
    }
}
