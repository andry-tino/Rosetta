/// <summary>
/// InterfaceDeclarationTUFTest.cs
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
    /// Tests for <see cref="InterfaceDeclarationTranslationUnitFactory"/> class.
    /// </summary>
    [TestClass]
    public class InterfaceDeclarationTUFTest
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
        public void EmptyInterface()
        {
            var source = @"
                public interface MyInterface {
                }
            ";

            var interfaceDeclarationNode = NodeFinder<InterfaceDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(interfaceDeclarationNode);

            var translationUnitFactory = new InterfaceDeclarationTranslationUnitFactory(interfaceDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var interfaceTranslationUnit = (translationUnitFactory as InterfaceDeclarationTranslationUnit);
            Assert.IsNotNull(interfaceTranslationUnit, $"Expecting a translation unit of type {typeof(InterfaceDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedInterfaceDeclarationTranslationUnit.Create(interfaceTranslationUnit);

            Assert.IsNotNull(translationUnit.Signatures);
            Assert.AreEqual(0, translationUnit.Signatures.Count(), "Expecting no signatures!");
        }
    }
}
