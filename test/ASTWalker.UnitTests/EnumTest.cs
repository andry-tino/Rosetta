/// <summary>
/// EnumTest.cs
/// Andrea Tino - 2016
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
    /// Tests for <see cref="EnumASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class EnumTest
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
        public void EmptyMembers()
        {
            Test(@"
                public enum MyEnum {
                    Value1,
                    Value2,
                    Value3
                }
            ");
        }

        [TestMethod]
        public void MembersWithValues()
        {
            Test(@"
                public enum MyEnum {
                    Value1 = 0,
                    Value2 = 1,
                    Value3 = 2
                }
            ");
        }

        [TestMethod]
        public void MixedMembers()
        {
            Test(@"
                public enum MyEnum {
                    Value1 = 0,
                    Value2,
                    Value3 = 1
                }
            ");
        }

        private static void Test(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(EnumDeclarationSyntax));
            var enumDeclarationNode = node as EnumDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedEnumASTWalker.Create(enumDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.EnumDeclaration);

            // Checking signatures
            Assert.IsNotNull(astWalker.EnumDeclaration.Members);
            Assert.IsTrue(astWalker.EnumDeclaration.Members.Count() > 0);
            Assert.AreEqual(3, astWalker.EnumDeclaration.Members.Count());

            Assert.IsInstanceOfType(astWalker.EnumDeclaration.Members.ElementAt(0), typeof(EnumMemberTranslationUnit));
            Assert.IsInstanceOfType(astWalker.EnumDeclaration.Members.ElementAt(1), typeof(EnumMemberTranslationUnit));
            Assert.IsInstanceOfType(astWalker.EnumDeclaration.Members.ElementAt(2), typeof(EnumMemberTranslationUnit));
        }
    }
}
