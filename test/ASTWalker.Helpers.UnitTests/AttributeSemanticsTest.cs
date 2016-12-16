/// <summary>
/// AttributeSemanticsTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for <see cref="AttributeSemantics"/>.
    /// </summary>
    [TestClass]
    public class AttributeSemanticsTest
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
        public void DecoratingClass()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(@"
            using System;
            namespace Namespace1
            {
                [Obsolete]
                public class Class1 { }
                public class Class2 { 
                    public Class1 Method1() { return null; }
                }
            }"
            );

            // Second class
            var node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(MethodDeclarationSyntax).Name));

            // Loading MSCoreLib
            var compilation = CSharpCompilation.Create("TestAssembly")
                  .AddReferences(
                     MetadataReference.CreateFromFile(
                       typeof(object).Assembly.Location))
                  .AddSyntaxTrees(tree);
            var semanticModel = compilation.GetSemanticModel(tree);

            var methodDeclarationNode = node as MethodDeclarationSyntax;
            var helper = new MethodDeclaration(methodDeclarationNode, semanticModel).ReturnType;

            var attributes = helper.Attributes;
            Assert.IsNotNull(attributes);
            Assert.AreNotEqual(0, attributes.Count(), "Expecting attributes!");

            var attribute = attributes.First();
            Assert.AreEqual("ObsoleteAttribute", attribute.AttributeClassName, "Wrong name!");
            Assert.AreEqual("System.ObsoleteAttribute", attribute.AttributeClassFullName, "Wrong full name!");
            Assert.IsNotNull(attribute.ConstructorArguments);
            Assert.AreEqual(0, attribute.ConstructorArguments.Count(), "Expected no constructor arguments");
        }
    }
}
