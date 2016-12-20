/// <summary>
/// ConstructorDefinitionTUFTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Tests.ScriptSharp.Utils;
    using Rosetta.Tests.Utils;
    using Rosetta.Translation;
    using Rosetta.Translation.Mocks;
    using Rosetta.ScriptSharp.Definition.Translation;

    /// <summary>
    /// Tests for <see cref="ConstructorDefinitionTranslationUnitFactory"/> class.
    /// </summary>
    [TestClass]
    public class ConstructorDefinitionTUFTest
    {
        private const string ScriptNamespaceOnParameterTypeTestSource = @"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public class Class2 {
                    public Class2(Class1 parameter1) { }
                }
            }
        ";

        private const string NamespaceOnParameterTypeTestSource = @"
            namespace Namespace1 {
                public class Class1 { }
                public class Class2 {
                    public Class2(Class1 parameter1) { }
                }
            }
        ";

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        /// <summary>
        /// Tests that, without semantic model, we actually have no possibility to retrieve a namespace.
        /// </summary>
        [TestMethod]
        public void NamespaceNotRetrievedOnParameterWithoutSemanticModel()
        {
            TestScriptNamespaceOnParameter(NamespaceOnParameterTypeTestSource, false, "Class1");
        }

        /// <summary>
        /// Tests that, with semantic model, we have the possibility to retrieve a namespace.
        /// </summary>
        [TestMethod]
        public void NamespaceRetrievedOnParameterWithSemanticModel()
        {
            TestScriptNamespaceOnParameter(NamespaceOnParameterTypeTestSource, true, "Namespace1.Class1");
        }

        /// <summary>
        /// Tests that, without semantic model, we actually have no possibility to retrieve the overriden namespace.
        /// </summary>
        [TestMethod]
        public void ScriptNamespaceNotRetrievedOnParameterWithoutSemanticModel()
        {
            TestScriptNamespaceOnParameter(ScriptNamespaceOnParameterTypeTestSource, false, "Class1");
        }

        /// <summary>
        /// Tests that, with semantic model, we have the possibility to retrieve the overriden namespace.
        /// </summary>
        [TestMethod]
        public void ScriptNamespaceRetrievedOnParameterWithSemanticModel()
        {
            TestScriptNamespaceOnParameter(ScriptNamespaceOnParameterTypeTestSource, true, "OverridenNamespace.Class1");
        }

        private static void TestScriptNamespaceOnParameter(string source, bool withSemanticModel, string expectedFullName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            // Loading MSCoreLib
            var semanticModel = withSemanticModel
                ? (CSharpCompilation.Create("TestAssembly")
                    .AddReferences(
                        MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                    .AddSyntaxTrees(tree).AddScriptNamespaceReference().GetSemanticModel(tree))
                : null;

            var node = new NodeLocator(tree).LocateFirst(typeof(ConstructorDeclarationSyntax));
            Assert.IsNotNull(node);

            var ctorDeclarationNode = node as ConstructorDeclarationSyntax;
            Assert.IsNotNull(ctorDeclarationNode);

            var translationUnitFactory = new ConstructorDefinitionTranslationUnitFactory(ctorDeclarationNode, semanticModel, true).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var ctorTranslationUnit = (translationUnitFactory as ConstructorDefinitionTranslationUnit);
            Assert.IsNotNull(ctorTranslationUnit, $"Expecting a translation unit of type {typeof(ConstructorDefinitionTranslationUnit).Name}!");

            var translationUnit = MockedConstructorDefinitionTranslationUnit.Create(ctorTranslationUnit);

            Assert.IsNotNull(translationUnit.Arguments);
            Assert.AreEqual(1, translationUnit.Arguments.Count(), "Expecting 1 argument!");

            var argumentTranslationUnit = translationUnit.Arguments.ElementAt(0) as ArgumentDefinitionTranslationUnit;
            Assert.IsNotNull(argumentTranslationUnit, $"Expected argument to be of type {typeof(ArgumentDefinitionTranslationUnit).Name}");

            var mockedArgumentTranslationUnit = MockedArgumentDefinitionTranslationUnit.Create(argumentTranslationUnit);
            Assert.IsNotNull(mockedArgumentTranslationUnit.VariableDeclaration);

            var variableDeclarationTranslationUnit = mockedArgumentTranslationUnit.VariableDeclaration as VariableDeclarationTranslationUnit;
            Assert.IsNotNull(variableDeclarationTranslationUnit);

            var mockedVariableDeclarationTranslationUnit = MockedVariableDeclarationTranslationUnit.Create(variableDeclarationTranslationUnit);
            Assert.IsNotNull(mockedVariableDeclarationTranslationUnit.Type, "Expecting a type!");

            var typeIdentifierTranslationUnit = mockedVariableDeclarationTranslationUnit.Type as TypeIdentifierTranslationUnit;
            Assert.IsNotNull(typeIdentifierTranslationUnit, $"Expected argument to be of type {typeof(TypeIdentifierTranslationUnit).Name}");

            var typeFullName = typeIdentifierTranslationUnit.Translate();
            Assert.AreEqual(expectedFullName, typeFullName, "Expected ScriptNamespace overriden type to be used!");
        }
    }
}
