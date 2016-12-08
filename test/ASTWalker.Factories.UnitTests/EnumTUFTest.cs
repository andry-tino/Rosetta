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
    /// Tests for <see cref="EnumTranslationUnitFactory"/> class.
    /// </summary>
    [TestClass]
    public class EnumTUFTest
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
        public void EmptyEnum()
        {
            var source = @"
                public enum MyEnum {
                }
            ";
            
            var enumDeclarationNode = NodeFinder<EnumDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(enumDeclarationNode);

            var translationUnitFactory = new EnumTranslationUnitFactory(enumDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var enumTranslationUnit = (translationUnitFactory as EnumTranslationUnit);
            Assert.IsNotNull(enumTranslationUnit, $"Expecting a translation unit of type {typeof(EnumTranslationUnit).Name}!");

            var translationUnit = MockedEnumTranslationUnit.Create(enumTranslationUnit);

            Assert.IsNotNull(translationUnit.Members);
            Assert.AreEqual(0, translationUnit.Members.Count(), "Expecting no members!");
        }
    }
}
