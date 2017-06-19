/// <summary>
/// NestingLevelHandlingTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;
    using Rosetta.Translation.UnitTests.Data;
    using Utils = Rosetta.Tests.Utils;

    [TestClass]
    public class NestingLevelHandlingTest
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
        public void NestedElementAddsNestedElement()
        {
            var translationUnit = new NestedTranslationUnit();
            Assert.AreEqual(-1, translationUnit.NestingLevel, 
                "At the beginning, for a class not calling the base constructor with automatic nesting level, nesting level should be the automatic one!");

            var nestedTranslationUnit = new NestedTranslationUnit();

            translationUnit.AddTranslationUnit(nestedTranslationUnit);
            Assert.AreEqual(0, nestedTranslationUnit.NestingLevel, 
                "When adding to an automatic nested unit, nesting level should be 0!");
        }

        [TestMethod]
        public void NestedElementAddsNestedElement2()
        {
            var translationUnit = new NestedTranslationUnit2();
            TestInitialNestingLevel(translationUnit);

            var nestedTranslationUnit = new NestedTranslationUnit();

            translationUnit.AddTranslationUnit(nestedTranslationUnit);
            TestNestingLevels(translationUnit, nestedTranslationUnit);
        }

        [TestMethod]
        public void ModuleAddsClass()
        {
            var translationUnit = ModuleTranslationUnit.Create(IdentifierTranslationUnit.Create("Module1"));
            TestInitialNestingLevel(translationUnit);

            var nestedTranslationUnit = ClassDeclarationTranslationUnit.Create(
                ModifierTokens.None, IdentifierTranslationUnit.Create("Class1"), null);

            translationUnit.AddClass(nestedTranslationUnit);
            TestNestingLevels(translationUnit, nestedTranslationUnit);
        }

        [TestMethod]
        public void ClassAddsMethod()
        {
            var translationUnit = ClassDeclarationTranslationUnit.Create(
                ModifierTokens.None, IdentifierTranslationUnit.Create("Class1"), null);
            TestInitialNestingLevel(translationUnit);

            var nestedTranslationUnit = MethodDeclarationTranslationUnit.Create(
                ModifierTokens.None, null, IdentifierTranslationUnit.Create("Method1"));

            translationUnit.AddMethodDeclaration(nestedTranslationUnit);
            TestNestingLevels(translationUnit, nestedTranslationUnit);
        }

        [TestMethod]
        public void MethodAddsStatement()
        {
            var translationUnit = MethodDeclarationTranslationUnit.Create(
                ModifierTokens.None, null, IdentifierTranslationUnit.Create("Method1")); ;
            TestInitialNestingLevel(translationUnit);

            var nestedTranslationUnit = VariableDeclarationTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Create("int"),
                IdentifierTranslationUnit.Create("var1"));

            translationUnit.AddStatement(nestedTranslationUnit);
            TestNestingLevels(translationUnit, nestedTranslationUnit);
        }

        private static void TestInitialNestingLevel(NestedElementTranslationUnit translationUnit)
        {
            Assert.AreEqual(0, translationUnit.NestingLevel, "At the beginning, nesting level should be 0!");
        }

        private static void TestNestingLevels(NestedElementTranslationUnit parent, NestedElementTranslationUnit child)
        {
            Assert.AreEqual(parent.NestingLevel + 1, child.NestingLevel, "Nested translation unit should have +1 nesting level!");
        }

        /// <summary>
        /// Class used for testing.
        /// </summary>
        private class NestedTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
        {
            public NestedTranslationUnit()
                : base()
            {
            }

            public new void AddTranslationUnit(ITranslationUnit translationUnit)
            {
                base.AddTranslationUnit(translationUnit);
            }

            public string Translate()
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Class used for testing.
        /// </summary>
        private class NestedTranslationUnit2 : NestedElementTranslationUnit
        {
            public NestedTranslationUnit2()
                : base(AutomaticNestingLevel)
            {
            }

            public new void AddTranslationUnit(ITranslationUnit translationUnit)
            {
                base.AddTranslationUnit(translationUnit);
            }
        }
    }
}
