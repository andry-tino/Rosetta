/// <summary>
/// NamespaceTest.cs
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
    /// Tests for <see cref="NamespaceASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class NamespaceTest
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
        public void ClassDeclarations()
        {
            string source = @"
                namespace MyNamespace {
                    public class Class1 {
                    }
                    private class Class2 {
                    }
                    internal class Class3 {
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(NamespaceDeclarationSyntax));
            NamespaceDeclarationSyntax namespaceNode = node as NamespaceDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedNamespaceASTWalker.Create(namespaceNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.Module);

            // Checking members
            Assert.IsNotNull(astWalker.Module.Classes);
            Assert.IsTrue(astWalker.Module.Classes.Count() > 0);
            Assert.AreEqual(3, astWalker.Module.Classes.Count());

            Assert.IsInstanceOfType(astWalker.Module.Classes.ElementAt(0), typeof(ClassDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.Module.Classes.ElementAt(1), typeof(ClassDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.Module.Classes.ElementAt(2), typeof(ClassDeclarationTranslationUnit));
        }
    }
}
