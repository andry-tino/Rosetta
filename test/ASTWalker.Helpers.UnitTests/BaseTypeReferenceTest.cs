/// <summary>
/// BaseTypeReferenceTest.cs
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
    /// Tests for <see cref="BaseTypeReference"/> class.
    /// </summary>
    [TestClass]
    public class BaseTypeReferenceTest
    {
        private static SyntaxTree Class2SyntaxTree;
        private static SemanticModel Class2SemanticModel;
        private static SyntaxTree Class3SyntaxTree;
        private static SemanticModel Class3SemanticModel;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Creating needed resources
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
        /// Tests that we can successfully retrieve the type name when having a class type in base list.
        /// </summary>
        [TestMethod]
        public void ClassTypeNameFromBaseList()
        {
            SyntaxNode node = new NodeLocator(Class2SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!", 
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;
            BaseTypeSyntax baseTypeNode = classDeclarationNode.BaseList.Types.FirstOrDefault();

            TestRetrieveTypeName(baseTypeNode, TestSuite.Class2.Value["BaseClassName"]);
        }

        /// <summary>
        /// Tests that we can successfully retrieve the type name when having an interface type.
        /// </summary>
        [TestMethod]
        public void InterfaceTypeNameFromBaseList()
        {
            SyntaxNode node = new NodeLocator(Class3SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!", 
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;
            BaseTypeSyntax baseTypeNode = classDeclarationNode.BaseList.Types.FirstOrDefault();

            TestRetrieveTypeName(baseTypeNode, TestSuite.Class3.Value["Interface1Name"]);
        }

        #region Helpers

        private static void TestRetrieveTypeName(BaseTypeSyntax baseTypeNode, string expected)
        {
            Assert.IsNotNull(baseTypeNode, "Found node should be of type `{0}`!", 
                typeof(BaseTypeSyntax).Name);

            BaseTypeReference baseTypeReference = new BaseTypeReference(baseTypeNode);
            string name = baseTypeReference.Name;

            Assert.IsNotNull(name, "Type name should not be null!");
            Assert.AreNotEqual(string.Empty, name, "Type name should not be empty!");
            Assert.AreEqual(expected, name, "Type name is not the one in source!");
        }

        #endregion
    }
}
