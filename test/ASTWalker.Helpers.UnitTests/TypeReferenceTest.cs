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

    using Rosetta.AST.Helpers;
    using Rosetta.Tests.Utils;

    [TestClass]
    public class TypeReferenceTest
    {
        private static SyntaxNode Class2SyntaxTree;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Creating needed resources
            Class2SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class2.Key).GetRoot();
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TypeName()
        {
            SyntaxNode node = new NodeLocator(Class2SyntaxTree).LocateFirst(typeof(SimpleBaseTypeSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!", "SimpleBaseTypeSyntax"));

            SimpleBaseTypeSyntax simpleBaseTypeNode = node as SimpleBaseTypeSyntax;
            Assert.IsNotNull(simpleBaseTypeNode, "Found node should be of type `{0}`!", "SimpleBaseTypeSyntax");

            TypeReference typeReference = new TypeReference(simpleBaseTypeNode);
            string identifier = typeReference.Identifier;
            Assert.IsNotNull(identifier, "Property `Identifier` should not be null!");
            Assert.AreEqual(TestSuite.Class1.Value["BaseClassName"], identifier);
        }
    }
}
