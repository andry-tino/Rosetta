/// <summary>
/// ClassDeclarationTUFTest.cs
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
    /// Tests for <see cref="ClassDeclarationTranslationUnitFactory"/> class.
    /// </summary>
    [TestClass]
    public class ClassDeclarationTranslationUnitFactoryTest
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
        public void ClassName()
        {
            var source = @"
                class Class1 {
                }
            ";
            
            var classDeclarationNode = NodeFinder<ClassDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(classDeclarationNode);

            var translationUnitFactory = new ClassDeclarationTranslationUnitFactory(classDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var classDeclarationTranslationUnit = (translationUnitFactory as ClassDeclarationTranslationUnit);
            Assert.IsNotNull(classDeclarationTranslationUnit, $"Expecting a translation unit of type {typeof(ClassDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedClassDeclarationTranslationUnit.Create(classDeclarationTranslationUnit);

            Assert.IsNotNull(translationUnit.Name);
            Assert.AreEqual("Class1", translationUnit.Name.Translate(), "Wrong name!");
        }

        [TestMethod]
        public void EmptyClass()
        {
            var source = @"
                class Class1 {
                }
            ";
            
            var classDeclarationNode = NodeFinder<ClassDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(classDeclarationNode);

            var translationUnitFactory = new ClassDeclarationTranslationUnitFactory(classDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var classDeclarationTranslationUnit = (translationUnitFactory as ClassDeclarationTranslationUnit);
            Assert.IsNotNull(classDeclarationTranslationUnit, $"Expecting a translation unit of type {typeof(ClassDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedClassDeclarationTranslationUnit.Create(classDeclarationTranslationUnit);

            Assert.IsNotNull(translationUnit.ConstructorDeclarations);
            Assert.AreEqual(0, translationUnit.ConstructorDeclarations.Count(), "Expecting no ctors!");

            Assert.IsNotNull(translationUnit.MethodDeclarations);
            Assert.AreEqual(0, translationUnit.MethodDeclarations.Count(), "Expecting no methods!");

            Assert.IsNotNull(translationUnit.PropertyDeclarations);
            Assert.AreEqual(0, translationUnit.PropertyDeclarations.Count(), "Expecting no properties!");

            Assert.IsNotNull(translationUnit.MemberDeclarations);
            Assert.AreEqual(0, translationUnit.MemberDeclarations.Count(), "Expecting no members!");
        }
    }
}
