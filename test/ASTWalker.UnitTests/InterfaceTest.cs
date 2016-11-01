/// <summary>
/// InterfaceTest.cs
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
    /// Tests for <see cref="InterfaceASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class InterfaceTest
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
        public void Methods()
        {
            string source = @"
                public interface MyInterface {
                    int Method1();
                    string Method2();
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedInterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.InterfaceDeclaration);

            // Checking signatures
            Assert.IsNotNull(astWalker.InterfaceDeclaration.Signatures);
            Assert.IsTrue(astWalker.InterfaceDeclaration.Signatures.Count() > 0);
            Assert.AreEqual(2, astWalker.InterfaceDeclaration.Signatures.Count());

            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(0), typeof(MethodSignatureDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(1), typeof(MethodSignatureDeclarationTranslationUnit));
        }

        [TestMethod]
        public void Properties()
        {
            string source = @"
                public interface MyInterface {
                    int Property1 { get; set; }
                    string Property2 { get; set; }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedInterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.InterfaceDeclaration);

            // Checking signatures
            Assert.IsNotNull(astWalker.InterfaceDeclaration.Signatures);
            Assert.IsTrue(astWalker.InterfaceDeclaration.Signatures.Count() > 0);
            Assert.AreEqual(2, astWalker.InterfaceDeclaration.Signatures.Count());

            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(0), typeof(MethodSignatureDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(1), typeof(MethodSignatureDeclarationTranslationUnit));
        }

        [TestMethod]
        public void GetOnlyProperties()
        {
            string source = @"
                public interface MyInterface {
                    int Property1 { get; }
                    string Property2 { get; }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedInterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.InterfaceDeclaration);

            // Checking signatures
            Assert.IsNotNull(astWalker.InterfaceDeclaration.Signatures);
            Assert.IsTrue(astWalker.InterfaceDeclaration.Signatures.Count() > 0);
            Assert.AreEqual(2, astWalker.InterfaceDeclaration.Signatures.Count());

            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(0), typeof(MethodSignatureDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(1), typeof(MethodSignatureDeclarationTranslationUnit));
        }

        [TestMethod]
        public void SetOnlyProperties()
        {
            string source = @"
                public interface MyInterface {
                    int Property1 { set; }
                    string Property2 { set; }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedInterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.InterfaceDeclaration);

            // Checking signatures
            Assert.IsNotNull(astWalker.InterfaceDeclaration.Signatures);
            Assert.IsTrue(astWalker.InterfaceDeclaration.Signatures.Count() > 0);
            Assert.AreEqual(2, astWalker.InterfaceDeclaration.Signatures.Count());

            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(0), typeof(MethodSignatureDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(1), typeof(MethodSignatureDeclarationTranslationUnit));
        }

        [TestMethod]
        public void MixedProperties()
        {
            string source = @"
                public interface MyInterface {
                    int Property1 { get; }
                    string Property2 { set; }
                    bool Property3 { get; set; }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedInterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.InterfaceDeclaration);

            // Checking signatures
            Assert.IsNotNull(astWalker.InterfaceDeclaration.Signatures);
            Assert.IsTrue(astWalker.InterfaceDeclaration.Signatures.Count() > 0);
            Assert.AreEqual(3, astWalker.InterfaceDeclaration.Signatures.Count());

            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(0), typeof(MethodSignatureDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(1), typeof(MethodSignatureDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.InterfaceDeclaration.Signatures.ElementAt(2), typeof(MethodSignatureDeclarationTranslationUnit));
        }
    }
}
