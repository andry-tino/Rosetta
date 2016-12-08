/// <summary>
/// MethodDeclarationTUFTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories.UnitTests
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
    using Rosetta.Translation;
    using Rosetta.Translation.Mocks;

    /// <summary>
    /// Tests for <see cref="MethodDeclarationTranslationUnitFactory"/> class.
    /// </summary>
    [TestClass]
    public class MethodDeclarationTUFTest
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
        public void ParameterlessMethod()
        {
            var source = @"
                class MyClass {
                    public void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsNotNull(translationUnit.Arguments);
            Assert.AreEqual(0, translationUnit.Arguments.Count(), "Expecting no arguments!");
        }

        [TestMethod]
        public void EmptyMethod()
        {
            var source = @"
                class MyClass {
                    public void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsNotNull(translationUnit.Statements);
            Assert.AreEqual(0, translationUnit.Statements.Count(), "Expecting no statements!");
        }
    }
}
