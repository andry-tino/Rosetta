/// <summary>
/// PropertyDefinitionTUFTest.cs
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
    /// Tests for <see cref="PropertyDefinitionTranslationUnitFactory"/> class.
    /// </summary>
    [TestClass]
    public class PropertyDefinitionTUFTest
    {
        private const string ScriptNamespaceOnReturnTypeTestSource = @"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public class Class2 {
                    public Class1 MyProperty { 
                        get { return null; } 
                        set { }
                    }
                }
            }
        ";

        private const string NamespaceOnReturnTypeTestSource = @"
            namespace Namespace1 {
                public class Class1 { }
                public class Class2 {
                    public Class1 MyProperty { 
                        get { return null; } 
                        set { }
                    }
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
            TestScriptNamespaceOnParameter(NamespaceOnReturnTypeTestSource, false, "Class1");
        }

        /// <summary>
        /// Tests that, with semantic model, we have the possibility to retrieve a namespace.
        /// </summary>
        [TestMethod]
        public void NamespaceRetrievedOnParameterWithSemanticModel()
        {
            TestScriptNamespaceOnParameter(NamespaceOnReturnTypeTestSource, true, "Namespace1.Class1");
        }

        /// <summary>
        /// Tests that, without semantic model, we actually have no possibility to retrieve the overriden namespace.
        /// </summary>
        [TestMethod]
        public void ScriptNamespaceNotRetrievedOnParameterWithoutSemanticModel()
        {
            TestScriptNamespaceOnParameter(ScriptNamespaceOnReturnTypeTestSource, false, "Class1");
        }

        /// <summary>
        /// Tests that, with semantic model, we have the possibility to retrieve the overriden namespace.
        /// </summary>
        [TestMethod]
        public void ScriptNamespaceRetrievedOnParameterWithSemanticModel()
        {
            TestScriptNamespaceOnParameter(ScriptNamespaceOnReturnTypeTestSource, true, "OverridenNamespace.Class1");
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

            var node = new NodeLocator(tree).LocateFirst(typeof(PropertyDeclarationSyntax));
            Assert.IsNotNull(node);

            var propertyDeclarationNode = node as PropertyDeclarationSyntax;
            Assert.IsNotNull(propertyDeclarationNode);

            var translationUnitFactory = new PropertyDefinitionTranslationUnitFactory(propertyDeclarationNode, semanticModel, true).Create();
            Assert.IsNotNull(translationUnitFactory, "Translation unit expected to be created!");

            var propertyTranslationUnit = (translationUnitFactory as PropertyDefinitionTranslationUnit);
            Assert.IsNotNull(propertyTranslationUnit, $"Expecting a translation unit of type {typeof(PropertyDefinitionTranslationUnit).Name}!");

            var translationUnit = MockedPropertyDefinitionTranslationUnit.Create(propertyTranslationUnit);
            Assert.IsNotNull(translationUnit.Type);
            
            var typeIdentifierTranslationUnit = translationUnit.Type as TypeIdentifierTranslationUnit;
            Assert.IsNotNull(typeIdentifierTranslationUnit, $"Expected argument to be of type {typeof(TypeIdentifierTranslationUnit).Name}");

            var typeFullName = typeIdentifierTranslationUnit.Translate();
            Assert.AreEqual(expectedFullName, typeFullName, "Expected ScriptNamespace overriden type to be used!");
        }
    }
}
