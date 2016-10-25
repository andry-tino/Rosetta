/// <summary>
/// ClassDeclarationTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers.UnitTests
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

        /// <summary>
        /// Tests that we can successfully retrieve the base class name (null).
        /// </summary>
        [TestMethod]
        public void BaseClassNameFromClass()
        {
            SyntaxNode node = new NodeLocator(Class1SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveBaseClassName(classDeclarationNode, Class1SemanticModel, null);
        }

        /// <summary>
        /// Tests that we can successfully retrieve the base class name.
        /// </summary>
        [TestMethod]
        public void BaseClassNameFromClassWithInheritance()
        {
            SyntaxNode node = new NodeLocator(Class2SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveBaseClassName(classDeclarationNode, Class2SemanticModel, TestSuite.Class2.Value["BaseClassName"]);
        }

        /// <summary>
        /// Tests that we can successfully retrieve the base class name (null).
        /// </summary>
        [TestMethod]
        public void BaseClassNameFromClassWithInterface()
        {
            SyntaxNode node = new NodeLocator(Class3SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveBaseClassName(classDeclarationNode, Class3SemanticModel, null);
        }

        /// <summary>
        /// Tests that we can successfully retrieve interfaces name (null).
        /// </summary>
        [TestMethod]
        public void InterfaceNameFromClass()
        {
            SyntaxNode node = new NodeLocator(Class1SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveInterfacesName(classDeclarationNode, Class1SemanticModel, null);
        }

        /// <summary>
        /// Tests that we can successfully retrieve interfaces name (null).
        /// </summary>
        [TestMethod]
        public void InterfaceNameFromClassWithInheritance()
        {
            SyntaxNode node = new NodeLocator(Class2SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveInterfacesName(classDeclarationNode, Class2SemanticModel, null);
        }

        /// <summary>
        /// Tests that we can successfully retrieve interfaces name.
        /// </summary>
        [TestMethod]
        public void InterfaceNameFromClassWithInterface()
        {
            SyntaxNode node = new NodeLocator(Class3SyntaxTree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            TestRetrieveInterfacesName(classDeclarationNode, Class3SemanticModel, 
                new string[] { TestSuite.Class3.Value["Interface1Name"] });
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

        private static void TestRetrieveBaseClassName(ClassDeclarationSyntax classDeclarationNode, SemanticModel semanticModel, string expected)
        {
            Assert.IsNotNull(classDeclarationNode, "Found node should be of type `{0}`!",
                typeof(ClassDeclarationSyntax).Name);

            ClassDeclaration classDeclaration = new ClassDeclaration(classDeclarationNode, semanticModel);
            BaseTypeReference baseClass = classDeclaration.BaseClass;

            if (expected == null)
            {
                Assert.IsNull(baseClass, "Class name should be null!");
                return;
            }

            string name = classDeclaration.BaseClass.Name;

            Assert.IsNotNull(name, "Class name should not be null!");
            Assert.AreNotEqual(string.Empty, name, "Class name should not be empty!");
            Assert.AreEqual(expected, name, "Base class name is not the one in source!");
        }

        private static void TestRetrieveInterfacesName(ClassDeclarationSyntax classDeclarationNode, SemanticModel semanticModel, string[] expected)
        {
            Assert.IsNotNull(classDeclarationNode, "Found node should be of type `{0}`!",
                typeof(ClassDeclarationSyntax).Name);

            ClassDeclaration classDeclaration = new ClassDeclaration(classDeclarationNode, semanticModel);
            IEnumerable<BaseTypeReference> implementedInterfaces = classDeclaration.ImplementedInterfaces;
            Assert.IsNotNull(implementedInterfaces, "Implemented interfaces should not be null!");

            if (expected == null)
            {
                Assert.AreEqual(0, implementedInterfaces.Count(),
                    "Number of implemented interfaces does not match expected!");
                return;
            }

            Assert.AreEqual(expected.Length, implementedInterfaces.Count(), 
                "Number of implemented interfaces does not match expected!");
            string[] names = classDeclaration.ImplementedInterfaces.Select(interfaceType => interfaceType.Name).ToArray();

            Assert.IsNotNull(names, "Interface names should not be null!");
            Assert.AreEqual(names.Length, implementedInterfaces.Count(), 
                "Number of implemented interfaces does not match expected!");

            foreach (string name in names)
            {
                Assert.AreNotEqual(string.Empty, name, "Interface name should not be empty!");
                Assert.IsTrue(expected.Where(expectedName => name == expectedName).Count() == 1, 
                    string.Format("Expecting an interface of type {0}, but not found!", name));
            }
        }

        #endregion
    }
}
