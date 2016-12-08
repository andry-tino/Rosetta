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
    /// Tests for <see cref="ConstructorASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class ConstructorTest
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
        public void ConstructorWithVariableDeclarations()
        {
            string source = @"
                class Class1 {
                    public Class1() {
                        string var1;
                        int var2;
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ConstructorDeclarationSyntax));
            ConstructorDeclarationSyntax constructorDeclarationNode = node as ConstructorDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedConstructorASTWalker.Create(constructorDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.ConstructorDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.ConstructorDeclaration.Statements);
            Assert.IsTrue(astWalker.ConstructorDeclaration.Statements.Count() > 0);
            Assert.AreEqual(2, astWalker.ConstructorDeclaration.Statements.Count());

            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Statements.ElementAt(0), typeof(StatementTranslationUnit));
            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Statements.ElementAt(1), typeof(StatementTranslationUnit));
        }

        [TestMethod]
        public void EmptyMethodWithNoParameters()
        {
            string source = @"
                class Class1 {
                    public Class1() { }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ConstructorDeclarationSyntax));
            ConstructorDeclarationSyntax constructorDeclarationNode = node as ConstructorDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedConstructorASTWalker.Create(constructorDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.ConstructorDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.ConstructorDeclaration.Arguments);
            Assert.AreEqual(0, astWalker.ConstructorDeclaration.Arguments.Count());
        }

        [TestMethod]
        public void EmptyMethodWith1Parameter()
        {
            string source = @"
                class Class1 {
                    public Class1(string param1) { }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ConstructorDeclarationSyntax));
            ConstructorDeclarationSyntax constructorDeclarationNode = node as ConstructorDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedConstructorASTWalker.Create(constructorDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.ConstructorDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.ConstructorDeclaration.Arguments);
            Assert.IsTrue(astWalker.ConstructorDeclaration.Arguments.Count() > 0);
            Assert.AreEqual(1, astWalker.ConstructorDeclaration.Arguments.Count());

            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Arguments.ElementAt(0), typeof(ArgumentDefinitionTranslationUnit));
        }

        [TestMethod]
        public void EmptyMethodWith2Parameters()
        {
            string source = @"
                class Class1 {
                    public Class1(string param1, int param2) { }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ConstructorDeclarationSyntax));
            ConstructorDeclarationSyntax constructorDeclarationNode = node as ConstructorDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedConstructorASTWalker.Create(constructorDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.ConstructorDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.ConstructorDeclaration.Arguments);
            Assert.IsTrue(astWalker.ConstructorDeclaration.Arguments.Count() > 0);
            Assert.AreEqual(2, astWalker.ConstructorDeclaration.Arguments.Count());

            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Arguments.ElementAt(0), typeof(ArgumentDefinitionTranslationUnit));
            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Arguments.ElementAt(1), typeof(ArgumentDefinitionTranslationUnit));
        }

        [TestMethod]
        public void EmptyMethodWith3Parameters()
        {
            string source = @"
                class Class1 {
                    public Class1(string param1, int param2, bool param3) { }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ConstructorDeclarationSyntax));
            ConstructorDeclarationSyntax constructorDeclarationNode = node as ConstructorDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedConstructorASTWalker.Create(constructorDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.ConstructorDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.ConstructorDeclaration.Arguments);
            Assert.IsTrue(astWalker.ConstructorDeclaration.Arguments.Count() > 0);
            Assert.AreEqual(3, astWalker.ConstructorDeclaration.Arguments.Count());

            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Arguments.ElementAt(0), typeof(ArgumentDefinitionTranslationUnit));
            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Arguments.ElementAt(1), typeof(ArgumentDefinitionTranslationUnit));
            Assert.IsInstanceOfType(astWalker.ConstructorDeclaration.Arguments.ElementAt(2), typeof(ArgumentDefinitionTranslationUnit));
        }
    }
}
