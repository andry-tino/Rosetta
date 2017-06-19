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

        [TestMethod]
        public void PublicMethod()
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

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Public), "Expected public modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Static));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
        }

        [TestMethod]
        public void PrivateMethod()
        {
            var source = @"
                class MyClass {
                    private void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Private), "Expected private modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Static));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
        }

        [TestMethod]
        public void ProtectedMethod()
        {
            var source = @"
                class MyClass {
                    protected void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected), "Expected protected modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Static));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
        }

        [TestMethod]
        public void InternalMethod()
        {
            var source = @"
                class MyClass {
                    internal void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal), "Expected internal modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Static));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
        }

        [TestMethod]
        public void NoModifierMethod()
        {
            var source = @"
                class MyClass {
                    void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Static));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
        }

        [TestMethod]
        public void StaticMethod()
        {
            var source = @"
                class MyClass {
                    static void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Static), "Expected static modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
        }

        [TestMethod]
        public void PrivateStaticMethod()
        {
            var source = @"
                class MyClass {
                    private static void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Static), "Expected static modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Private), "Expected private modifier");
        }

        [TestMethod]
        public void PublicStaticMethod()
        {
            var source = @"
                class MyClass {
                    public static void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Static), "Expected static modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Public), "Expected public modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
        }

        [TestMethod]
        public void ProtectedStaticMethod()
        {
            var source = @"
                class MyClass {
                    protected static void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Static), "Expected static modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal));
            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected), "Expected protected modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
        }

        [TestMethod]
        public void InternalStaticMethod()
        {
            var source = @"
                class MyClass {
                    internal static void MyMethod() { }
                }
            ";

            var methodDeclarationNode = NodeFinder<MethodDeclarationSyntax>.GetNode(source);
            Assert.IsNotNull(methodDeclarationNode);

            var translationUnitFactory = new MethodDeclarationTranslationUnitFactory(methodDeclarationNode).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var methodTranslationUnit = (translationUnitFactory as MethodDeclarationTranslationUnit);
            Assert.IsNotNull(methodTranslationUnit, $"Expecting a translation unit of type {typeof(MethodDeclarationTranslationUnit).Name}!");

            var translationUnit = MockedMethodDeclarationTranslationUnit.Create(methodTranslationUnit);

            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Static), "Expected static modifier");
            Assert.IsTrue(translationUnit.Modifiers.HasFlag(ModifierTokens.Internal), "Expected internal modifier");
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Protected));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Public));
            Assert.IsFalse(translationUnit.Modifiers.HasFlag(ModifierTokens.Private));
        }
    }
}
