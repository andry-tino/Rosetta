/// <summary>
/// ConditionalStatementTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for <see cref="ConditionalStatement"/> class.
    /// </summary>
    [TestClass]
    public class ConditionalStatementTest
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
            Class1SyntaxTree = CSharpSyntaxTree.ParseText(@"
                class Class1 {
                    public void Method1() {
                        if (true) {
                        }
                    }
                }
            ");
            Class1SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class1SyntaxTree).GetSemanticModel(Class1SyntaxTree);

            Class2SyntaxTree = CSharpSyntaxTree.ParseText(@"
                class Class1 {
                    public void Method1() {
                        if (true) {
                        } else {
                        }
                    }
                }
            ");
            Class2SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class2SyntaxTree).GetSemanticModel(Class2SyntaxTree);

            Class3SyntaxTree = CSharpSyntaxTree.ParseText(@"
                class Class1 {
                    public void Method1() {
                        if (true) {
                        } else if (false) {
                        } else {
                        }
                    }
                }
            ");
            Class3SemanticModel = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(
                Class3SyntaxTree).GetSemanticModel(Class3SyntaxTree);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        /// <summary>
        /// Tests that we can successfully retrieve the correct number of bodies.
        /// </summary>
        [TestMethod]
        public void RetrieveCorrectNumberOfBodies()
        {
            RetrieveNumberOfBodiesAndTest(Class1SyntaxTree, 1);
            RetrieveNumberOfBodiesAndTest(Class2SyntaxTree, 1);
            RetrieveNumberOfBodiesAndTest(Class3SyntaxTree, 2);
        }

        /// <summary>
        /// Tests that we can successfully determine whether the last ELSE clause is present.
        /// </summary>
        [TestMethod]
        public void CorrectlyDeterminePresentOfLastElseClause()
        {
            DetermineLastElseAndTest(Class1SyntaxTree, false);
            DetermineLastElseAndTest(Class2SyntaxTree, true);
            DetermineLastElseAndTest(Class3SyntaxTree, true);
        }

        #region Helpers

        private static void RetrieveNumberOfBodiesAndTest(SyntaxTree syntaxTree, int expectedNumberOfBodies)
        {
            SyntaxNode node = new NodeLocator(syntaxTree).LocateFirst(typeof(IfStatementSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(IfStatementSyntax).Name));

            IfStatementSyntax conditionalNode = node as IfStatementSyntax;

            TestRetrievedNumberOfBodies(conditionalNode, expectedNumberOfBodies);
        }

        private static void DetermineLastElseAndTest(SyntaxTree syntaxTree, bool expectedElse)
        {
            SyntaxNode node = new NodeLocator(syntaxTree).LocateFirst(typeof(IfStatementSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(IfStatementSyntax).Name));

            IfStatementSyntax conditionalNode = node as IfStatementSyntax;

            TestDeterminedLastElseClause(conditionalNode, expectedElse);
        }

        private static void TestRetrievedNumberOfBodies(IfStatementSyntax conditionalNode, int expected)
        {
            Assert.IsNotNull(conditionalNode, "Found node should be of type `{0}`!",
                typeof(IfStatementSyntax).Name);

            ConditionalStatement conditionalStatement = new ConditionalStatement(conditionalNode);
            int blocksNumber = conditionalStatement.BlocksNumber;
            
            Assert.AreEqual(expected, blocksNumber, "Number of retrieved blocks does not match!");
        }

        private static void TestDeterminedLastElseClause(IfStatementSyntax conditionalNode, bool expected)
        {
            Assert.IsNotNull(conditionalNode, "Found node should be of type `{0}`!",
                typeof(IfStatementSyntax).Name);

            ConditionalStatement conditionalStatement = new ConditionalStatement(conditionalNode);
            bool lastElsePresent = conditionalStatement.HasElseBlock;

            Assert.AreEqual(expected, lastElsePresent, "Expected last ELSE clause presence does not match!");
        }

        #endregion
    }
}
