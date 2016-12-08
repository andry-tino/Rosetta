/// <summary>
/// ConstructorDeclarationTUFTest.cs
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
    /// Tests for <see cref="ConstructorDeclarationTranslationUnitFactory"/> class.
    /// </summary>
    [TestClass]
    public class ConstructorDeclarationTUFTest
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
        public void ParameterlessCtor()
        {
            var source = @"
                class Class1 {
                    public Class1() { }
                }
            ";
            
            var ctorDeclarationNode = NodeFinder<ConstructorDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(ctorDeclarationNode);

            var translationUnitFactory = new ConstructorDeclarationTranslationUnitFactory(ctorDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var ctorDeclarationTranslationUnit = (translationUnitFactory as ConstructorDeclarationTranslationUnit);
            Assert.IsNotNull(ctorDeclarationTranslationUnit, $"Expecting a translation unit of type {typeof(ConstructorDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedConstructorDeclarationTranslationUnit.Create(ctorDeclarationTranslationUnit);

            Assert.IsNotNull(translationUnit.Arguments);
            Assert.AreEqual(0, translationUnit.Arguments.Count(), "Expecting no arguments!");
        }
    }
}
