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

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Creating needed resources
            Class2SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class2.Key);
            Class2SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class2SyntaxTree).GetSemanticModel(Class2SyntaxTree);
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
            SyntaxNode node = new NodeLocator(Class2SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!", typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;
            BaseTypeSyntax baseTypeNode = classDeclarationNode.BaseList.Types.FirstOrDefault();
            Assert.IsNotNull(baseTypeNode, "Found node should be of type `{0}`!", typeof(BaseTypeSyntax).Name);

            string a = Class2SemanticModel.GetDeclaredSymbol(classDeclarationNode).BaseType.Name;

            BaseTypeReference baseTypeReference = new BaseTypeReference(baseTypeNode, Class2SemanticModel);
            string name = baseTypeReference.Name;

            Assert.AreNotEqual(string.Empty, name, "Type name should not be empty!");
            Assert.IsNotNull(name, "Property `Name` should not be null!");
            Assert.AreEqual(TestSuite.Class2.Value["BaseClassName"], name);
        }
    }
}
