/// <summary>
/// ProgramTest.cs
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
    /// Tests for <see cref="ProgramASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class ProgramTest
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
        public void OneClass()
        {
            string source = @"
                public class Class1 {
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(CompilationUnitSyntax));
            CompilationUnitSyntax programNode = node as CompilationUnitSyntax;

            // Creating the walker
            var astWalker = MockedProgramASTWalker.Create(programNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.Program);

            // Checking members
            Assert.IsNotNull(astWalker.Program.Content);
            Assert.IsTrue(astWalker.Program.Content.Count() > 0);
            Assert.AreEqual(1, astWalker.Program.Content.Count());

            Assert.IsInstanceOfType(astWalker.Program.Content.ElementAt(0), typeof(ClassDeclarationTranslationUnit));
        }
    }
}
