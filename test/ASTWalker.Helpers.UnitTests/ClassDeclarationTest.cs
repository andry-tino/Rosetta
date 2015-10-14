/// <summary>
/// ClassDeclarationTest.cs
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

    /// <summary>
    /// Tests for <see cref="ClassDeclaration"/> class.
    /// </summary>
    [TestClass]
    public class ClassDeclarationTest
    {
        private static SyntaxTree Class1SyntaxTree;
        private static SemanticModel Class1SemanticModel;
        private static SyntaxTree Class2SyntaxTree;
        private static SemanticModel Class2SemanticModel;
        private static SyntaxTree Class3SyntaxTree;
        private static SemanticModel Class3SemanticModel;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Creating needed resources
            Class1SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class1.Key);
            Class1SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class1SyntaxTree).GetSemanticModel(Class1SyntaxTree);

            Class2SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class2.Key);
            Class2SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class2SyntaxTree).GetSemanticModel(Class2SyntaxTree);

            Class3SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class3.Key);
            Class3SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class3SyntaxTree).GetSemanticModel(Class3SyntaxTree);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        /// <summary>
        /// Tests that we can successfully retrieve the class name.
        /// </summary>
        [TestMethod]
        public void ClassNameFromSimpleClass()
        {
            SyntaxNode node = new NodeLocator(Class1SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!", 
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveClassName(classDeclarationNode, TestSuite.Class1.Value["ClassName"]);
        }

        /// <summary>
        /// Tests that we can successfully retrieve the class name.
        /// </summary>
        [TestMethod]
        public void ClassNameFromClassWithInheritance()
        {
            SyntaxNode node = new NodeLocator(Class2SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveClassName(classDeclarationNode, TestSuite.Class2.Value["ClassName"]);
        }

        /// <summary>
        /// Tests that we can successfully retrieve the class name.
        /// </summary>
        [TestMethod]
        public void ClassNameFromClassWithInterface()
        {
            SyntaxNode node = new NodeLocator(Class3SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveClassName(classDeclarationNode, TestSuite.Class3.Value["ClassName"]);
        }

        #region Helpers

        private static void TestRetrieveClassName(ClassDeclarationSyntax classDeclarationNode, string expected)
        {
            Assert.IsNotNull(classDeclarationNode, "Found node should be of type `{0}`!",
                typeof(ClassDeclarationSyntax).Name);

            ClassDeclaration classDeclaration = new ClassDeclaration(classDeclarationNode);
            string name = classDeclaration.Name;

            Assert.IsNotNull(name, "Class name should not be null!");
            Assert.AreNotEqual(string.Empty, name, "Class name should not be empty!");
            Assert.AreEqual(expected, name, "Class name is not the one in source!");
        }

        #endregion
    }
}
