/// <summary>
/// TypeReferenceTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.ScriptSharp.AST.Helpers;
    using Rosetta.Tests.ScriptSharp.Utils;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for <see cref="BaseTypeReference"/> and the <see cref="TypeReference"/> classes.
    /// </summary>
    [TestClass]
    public class BaseTypeReferenceTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        /// <summary>
        /// Tests that we render the name and full name when not specifying the semantic model and when doing so.
        /// </summary>
        [TestMethod]
        public void ScriptNamespaceRetrievedOnReturnType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public class Class2 {
                    public Class1 MyMethod() { return null; }
                }
            }
            ");

            // Loading MSCoreLib
            var semanticModel = (CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference().GetSemanticModel(tree));

            var node = new NodeLocator(tree).LocateFirst(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node);

            var methodDeclarationNode = node as MethodDeclarationSyntax;
            Assert.IsNotNull(methodDeclarationNode);

            var typeReference = new MethodDeclaration(methodDeclarationNode, semanticModel).ReturnType as TypeReference;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }
    }
}
