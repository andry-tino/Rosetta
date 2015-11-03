/// <summary>
/// MethodDeclarationTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ASTWalker.Helpers.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for <see cref="MethodDeclaration"/> class.
    /// </summary>
    [TestClass]
    public class MethodDeclarationTest
    {
        private static SyntaxTree Class11SyntaxTree;
        private static SemanticModel Class11SemanticModel;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Creating needed resources
            Class11SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class11.Key);
            Class11SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class11SyntaxTree).GetSemanticModel(Class11SyntaxTree);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        /// <summary>
        /// Tests that we can successfully retrieve the method name.
        /// </summary>
        [TestMethod]
        public void MethodName()
        {
            string[] methodExpectedNames = new string[] 
            {
                TestSuite.Class11.Value["Method1Name"],
                TestSuite.Class11.Value["Method2Name"],
                TestSuite.Class11.Value["Method3Name"],
                TestSuite.Class11.Value["Method4Name"],
                TestSuite.Class11.Value["Method5Name"],
                TestSuite.Class11.Value["Method6Name"],
                TestSuite.Class11.Value["Method7Name"],
            };
            IEnumerable<KeyValuePair<SyntaxNode, string>> items = 
                new NodeLocator(Class11SyntaxTree).LocateAll(typeof(MethodDeclarationSyntax))
                .Select((node, k) => new KeyValuePair<SyntaxNode, string>(node, methodExpectedNames[k]));

            foreach (KeyValuePair<SyntaxNode, string> item in items)
            {
                Assert.IsNotNull(item.Key, string.Format("Node of type `{0}` should be found!",
                    typeof(MethodDeclarationSyntax).Name));

                MethodDeclarationSyntax methodDeclarationNode = item.Key as MethodDeclarationSyntax;

                TestRetrieveMethodName(methodDeclarationNode, item.Value);
            }
        }

        /// <summary>
        /// Tests that we can successfully retrieve the method return type when it is <code>void</code>.
        /// </summary>
        [TestMethod]
        public void MethodReturnTypeVoid()
        {
            IEnumerable<SyntaxNode> nodes = new NodeLocator(Class11SyntaxTree).LocateAll(
                typeof(MethodDeclarationSyntax)).Take(3); // The first 3 have void return type

            foreach (SyntaxNode node in nodes)
            {
                Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                    typeof(MethodDeclarationSyntax).Name));

                MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

                TestRetrieveMethodReturnType(methodDeclarationNode);
            }
        }

        #region Helpers

        private static void TestRetrieveMethodName(MethodDeclarationSyntax methodDeclarationNode, string expected)
        {
            Assert.IsNotNull(methodDeclarationNode, "Found node should be of type `{0}`!",
                typeof(MethodDeclarationSyntax).Name);

            MethodDeclaration methodDeclaration = new MethodDeclaration(methodDeclarationNode);
            string name = methodDeclaration.Name;

            Assert.IsNotNull(name, "Method name should not be null!");
            Assert.AreNotEqual(string.Empty, name, "Method name should not be empty!");
            Assert.AreEqual(expected, name, "Method name is not the one in source!");
        }

        // When specifying `null` as expected return type, we intend `void`.
        private static void TestRetrieveMethodReturnType(MethodDeclarationSyntax methodDeclarationNode, string expected = null)
        {
            Assert.IsNotNull(methodDeclarationNode, "Found node should be of type `{0}`!",
                typeof(MethodDeclarationSyntax).Name);

            MethodDeclaration methodDeclaration = new MethodDeclaration(methodDeclarationNode);
            string typeName = methodDeclaration.ReturnType;

            Assert.IsNotNull(typeName, "Method name should not be null!");
            Assert.AreNotEqual(string.Empty, typeName, "Method name should not be empty!");
            Assert.AreEqual(expected ?? "void", typeName, "Method name is not the one in source!");
        }

        #endregion
    }
}
