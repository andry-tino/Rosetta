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

        /// <summary>
        /// Tests that we can successfully retrieve the method parameters when there are no parameters.
        /// </summary>
        [TestMethod]
        public void MethodWithNoParameters()
        {
            var source = @"
                class Class1 {
                    public void Method1() { }
                }
            ";

            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var semanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                syntaxTree).GetSemanticModel(syntaxTree);

            SyntaxNode node = new NodeLocator(syntaxTree).LocateFirst(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(MethodDeclarationSyntax).Name));

            MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;
            MethodDeclaration methodDeclaration = new MethodDeclaration(methodDeclarationNode);

            Assert.IsNotNull(methodDeclaration.Parameters, "Expecting list of parameters not to be null!");
            Assert.AreEqual(0, methodDeclaration.Parameters.Count(), "Expecting no parameters!");
        }

        /// <summary>
        /// Tests that we can successfully retrieve the method parameters.
        /// </summary>
        [TestMethod]
        public void MethodWithParameters()
        {
            var source = @"
                class Class1 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                    public void Method3(int param1, string param2, object param3) { }
                    public void Method4(int param1, string param2, object param3, bool param4) { }
                }
            ";

            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var semanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                syntaxTree).GetSemanticModel(syntaxTree);

            IEnumerable<SyntaxNode> nodes = new NodeLocator(syntaxTree).LocateAll(
                typeof(MethodDeclarationSyntax));

            foreach (SyntaxNode node in nodes)
            {
                Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                    typeof(MethodDeclarationSyntax).Name));
                MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

                MethodDeclaration methodDeclaration = new MethodDeclaration(methodDeclarationNode);
                Assert.IsNotNull(methodDeclaration.Parameters, "Expecting list of parameters not to be null!");
                Assert.IsTrue(methodDeclaration.Parameters.Count() > 0, "Expecting parameters!");
            }

            // 1 parameter
            MethodDeclaration method1Declaration = new MethodDeclaration(nodes.ElementAt(0) as MethodDeclarationSyntax);
            Assert.AreEqual(1, method1Declaration.Parameters.Count(), "Expecting different number of paramters!");

            // 2 parameters
            MethodDeclaration method2Declaration = new MethodDeclaration(nodes.ElementAt(1) as MethodDeclarationSyntax);
            Assert.AreEqual(2, method2Declaration.Parameters.Count(), "Expecting different number of paramters!");

            // 3 parameters
            MethodDeclaration method3Declaration = new MethodDeclaration(nodes.ElementAt(2) as MethodDeclarationSyntax);
            Assert.AreEqual(3, method3Declaration.Parameters.Count(), "Expecting different number of paramters!");

            // 4 parameters
            MethodDeclaration method4Declaration = new MethodDeclaration(nodes.ElementAt(3) as MethodDeclarationSyntax);
            Assert.AreEqual(4, method4Declaration.Parameters.Count(), "Expecting different number of paramters!");
        }

        /// <summary>
        /// Tests that we can successfully retrieve the method parameters' type.
        /// </summary>
        [TestMethod]
        public void MethodParametersAreOfCorrectType()
        {
            var source = @"
                class Class1 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                    public void Method3(int param1, string param2, object param3) { }
                    public void Method4(int param1, string param2, object param3, bool param4) { }
                }
            ";

            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var semanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                syntaxTree).GetSemanticModel(syntaxTree);

            IEnumerable<SyntaxNode> nodes = new NodeLocator(syntaxTree).LocateAll(
                typeof(MethodDeclarationSyntax));

            foreach (SyntaxNode node in nodes)
            {
                Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                    typeof(MethodDeclarationSyntax).Name));
                MethodDeclarationSyntax methodDeclarationNode = node as MethodDeclarationSyntax;

                MethodDeclaration methodDeclaration = new MethodDeclaration(methodDeclarationNode);

                foreach (var param in methodDeclaration.Parameters)
                {
                    Assert.IsNotNull(param, "Parameter should not be null!");
                    Assert.IsInstanceOfType(param, typeof(Parameter), "Wrong parameter type!");
                }
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
