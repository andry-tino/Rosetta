/// <summary>
/// BaseTypeReferenceTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for <see cref="BaseTypeReference"/> and the <see cref="TypeReference"/> classes.
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
            Class2SemanticModel = null;

            Class3SyntaxTree = CSharpSyntaxTree.ParseText(TestSuite.Class3.Key);
            Class3SemanticModel = null;
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

            TestRetrieveTypeName(baseTypeNode, null, TestSuite.Class2.Value["BaseClassName"]);
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

            TestRetrieveTypeName(baseTypeNode, null, TestSuite.Class3.Value["Interface1Name"]);
        }

        /// <summary>
        /// Tests that we render the name and full name when not specifying the semantic model and when doing so.
        /// </summary>
        [TestMethod]
        public void TypeFullNameRenderingSemanticModel()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            using System;
            public class MyClass : IDisposable {
                public void Dispose() { }
            }
            ");

            var node = new NodeLocator(tree).LocateLast(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(ClassDeclarationSyntax).Name));

            // Loading MSCoreLib
            var compilation = CSharpCompilation.Create("TestAssembly")
                  .AddReferences(
                     MetadataReference.CreateFromFile(
                       typeof(object).Assembly.Location))
                  .AddSyntaxTrees(tree);
            var semanticModel = compilation.GetSemanticModel(tree);

            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;
            BaseTypeSyntax baseTypeNode = classDeclarationNode.BaseList.Types.FirstOrDefault();

            TestRetrieveTypeName(baseTypeNode, null, "IDisposable");
            TestRetrieveTypeName(baseTypeNode, semanticModel, "IDisposable");
            TestRetrieveTypeFullName(baseTypeNode, null, "IDisposable");
            TestRetrieveTypeFullName(baseTypeNode, semanticModel, "System.IDisposable");
        }

        /// <summary>
        /// Tests that we can successfully discriminate void from non-void without semantic model.
        /// </summary>
        [TestMethod]
        public void DetectVoidType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            using System;
            public class MyClass {
                public void Method1() { }
                public int Method2() { }
            }
            ");
            
            // First method
            var node = new NodeLocator(tree).LocateFirst(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(MethodDeclarationSyntax).Name));

            var methodDeclarationNode = node as MethodDeclarationSyntax;
            var helper = new MethodDeclaration(methodDeclarationNode).ReturnType;

            Assert.IsTrue(helper.IsVoid, "Expected void type!");

            // Second method
            node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(MethodDeclarationSyntax).Name));

            methodDeclarationNode = node as MethodDeclarationSyntax;
            helper = new MethodDeclaration(methodDeclarationNode).ReturnType;

            Assert.IsFalse(helper.IsVoid, "Void type not expected!");
        }

        /// <summary>
        /// Tests that we can successfully discriminate void from non-void without semantic model.
        /// </summary>
        [TestMethod]
        public void DetectVoidTypeWithSemanticModel()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            using System;
            public class MyClass {
                public void Method1() { }
                public int Method2() { }
            }
            ");

            // First method
            var node = new NodeLocator(tree).LocateFirst(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(MethodDeclarationSyntax).Name));

            // Loading MSCoreLib
            var compilation = CSharpCompilation.Create("TestAssembly")
                  .AddReferences(
                     MetadataReference.CreateFromFile(
                       typeof(object).Assembly.Location))
                  .AddSyntaxTrees(tree);
            var semanticModel = compilation.GetSemanticModel(tree);

            var methodDeclarationNode = node as MethodDeclarationSyntax;
            var helper = new MethodDeclaration(methodDeclarationNode, semanticModel).ReturnType;

            Assert.IsTrue(helper.IsVoid, "Expected void type!");

            // Second method
            node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(MethodDeclarationSyntax).Name));

            methodDeclarationNode = node as MethodDeclarationSyntax;
            helper = new MethodDeclaration(methodDeclarationNode, semanticModel).ReturnType;

            Assert.IsFalse(helper.IsVoid, "Void type not expected!");
        }

        [TestMethod]
        public void RetrieveAttributesOnClassFromMethodViaSemantics()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(@"
            using System;
            namespace Namespace1
            {
                [Obsolete]
                public class Class1 { }
                public class Class2 { 
                    public Class1 Method1() { return null; }
                }
            }"
            );

            // Second class
            var node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(MethodDeclarationSyntax).Name));

            // Loading MSCoreLib
            var compilation = CSharpCompilation.Create("TestAssembly")
                  .AddReferences(
                     MetadataReference.CreateFromFile(
                       typeof(object).Assembly.Location))
                  .AddSyntaxTrees(tree);
            var semanticModel = compilation.GetSemanticModel(tree);

            var methodDeclarationNode = node as MethodDeclarationSyntax;
            var helper = new MethodDeclaration(methodDeclarationNode, semanticModel).ReturnType;

            var attributes = helper.Attributes;
            Assert.IsNotNull(attributes);
            Assert.AreEqual(1, attributes.Count(), "Expecting 1 attribute!");
            Assert.IsTrue(attributes.First().AttributeClassName.Contains("ObsoleteAttribute"), "Attribute `Obsolete` expected!");
        }

        #region Helpers

        private static void TestRetrieveTypeName(BaseTypeSyntax baseTypeNode, SemanticModel semanticModel, string expected)
        {
            Assert.IsNotNull(baseTypeNode, "Found node should be of type `{0}`!", 
                typeof(BaseTypeSyntax).Name);

            BaseTypeReference baseTypeReference = new BaseTypeReference(baseTypeNode, semanticModel);
            string name = baseTypeReference.Name;

            Assert.IsNotNull(name, "Type name should not be null!");
            Assert.AreNotEqual(string.Empty, name, "Type name should not be empty!");
            Assert.AreEqual(expected, name, "Type name is not the one in source!");
        }

        private static void TestRetrieveTypeFullName(BaseTypeSyntax baseTypeNode, SemanticModel semanticModel, string expected)
        {
            Assert.IsNotNull(baseTypeNode, "Found node should be of type `{0}`!",
                typeof(BaseTypeSyntax).Name);

            BaseTypeReference baseTypeReference = new BaseTypeReference(baseTypeNode, semanticModel);
            string name = baseTypeReference.FullName;

            Assert.IsNotNull(name, "Type full name should not be null!");
            Assert.AreNotEqual(string.Empty, name, "Type full name should not be empty!");
            Assert.AreEqual(expected, name, "Type full name is not the one in source!");
        }

        #endregion
    }
}
