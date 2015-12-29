/// <summary>
/// ClassTest.cs
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
    /// Tests for <see cref="ClassASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class ClassTest
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
        public void FieldMembers()
        {
            string source = @"
                public class MyClass {
                    private int myInt;
                    public string MyString;
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(ClassDeclarationSyntax));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedClassASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.ClassDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDeclaration.MemberDeclarations);
            Assert.IsTrue(astWalker.ClassDeclaration.MemberDeclarations.Count() > 0);
            Assert.AreEqual(2, astWalker.ClassDeclaration.MemberDeclarations.Count());

            Assert.IsInstanceOfType(astWalker.ClassDeclaration.MemberDeclarations.ElementAt(0), typeof(FieldDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.ClassDeclaration.MemberDeclarations.ElementAt(1), typeof(FieldDeclarationTranslationUnit));
        }

        [TestMethod]
        public void Methods()
        {
            string source = @"
                public class MyClass {
                    private void Method1() {
                    }

                    protected void Method2() {
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(ClassDeclarationSyntax));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedClassASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.ClassDeclaration);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDeclaration.MethodDeclarations);
            Assert.IsTrue(astWalker.ClassDeclaration.MethodDeclarations.Count() > 0);
            Assert.AreEqual(2, astWalker.ClassDeclaration.MethodDeclarations.Count());

            Assert.IsInstanceOfType(astWalker.ClassDeclaration.MethodDeclarations.ElementAt(0), typeof(MethodDeclarationTranslationUnit));
            Assert.IsInstanceOfType(astWalker.ClassDeclaration.MethodDeclarations.ElementAt(1), typeof(MethodDeclarationTranslationUnit));
        }
    }
}
