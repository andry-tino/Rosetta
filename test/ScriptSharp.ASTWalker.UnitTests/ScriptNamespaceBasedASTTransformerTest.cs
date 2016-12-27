/// <summary>
/// ScriptNamespaceBasedASTTransformerTest.cs
/// Andrea Tino - 2016
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

    using Rosetta.AST.Helpers;
    using Rosetta.ScriptSharp.AST.Transformers;
    using Rosetta.Tests.ScriptSharp.Utils;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for the <see cref="ScriptNamespaceBasedASTTransformerTest"/> class.
    /// </summary>
    [TestClass]
    public class ScriptNamespaceBasedASTTransformerTest
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
        public void WhenNoScriptNamespaceAttributeProvidedThenASTIsNotChanged()
        {
            TestWhenNoScriptNamespaceAttributeProvidedThenASTIsNotChanged();
        }

        [TestMethod]
        public void WhenNoScriptNamespaceAttributeProvidedWithCompilationThenASTIsNotChanged()
        {
            TestWhenNoScriptNamespaceAttributeProvidedThenASTIsNotChanged(true);
        }

        private static void TestWhenNoScriptNamespaceAttributeProvidedThenASTIsNotChanged(bool withCompilation = false)
        {
            var source = @"
            namespace OriginalNamespace {
                public class MyClass { }
            }
            ";

            var tree = CSharpSyntaxTree.ParseText(source) as CSharpSyntaxTree;

            // Loading MSCoreLib
            var compilation = withCompilation
                ? CSharpCompilation.Create("TestAssembly")
                    .AddReferences(
                        MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                    .AddSyntaxTrees(tree).AddScriptNamespaceReference()
                : null;

            // Transform
            if (withCompilation)
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree, ref compilation);
            }
            else
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree);
            }

            // Test
            var namespaceSyntaxNode = new NodeLocator(tree).LocateFirst(typeof(NamespaceDeclarationSyntax));
            Assert.IsNotNull(namespaceSyntaxNode, "A namespace was expected");
            var namespaceDeclarationNode = namespaceSyntaxNode as NamespaceDeclarationSyntax;
            Assert.IsNotNull(namespaceDeclarationNode);
            var namespaceHelper = new NamespaceDeclaration(namespaceDeclarationNode);
            Assert.AreEqual("OriginalNamespace", namespaceHelper.Name, "The name of the namespace should be the same");

            var classSyntaxNode = new NodeLocator(tree).LocateFirst(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(classSyntaxNode, "A class was expected");
            var classDeclarationNode = classSyntaxNode as ClassDeclarationSyntax;
            Assert.IsNotNull(namespaceDeclarationNode);
            var classHelper = new ClassDeclaration(classDeclarationNode);
            Assert.AreEqual("MyClass", classHelper.Name, "The name of the class should be the same");

            Assert.IsTrue(classSyntaxNode.IsChildOf(namespaceDeclarationNode), "Class expected to be child of namespace");
        }

        [TestMethod]
        public void WhenScriptNamespaceAttributeProvidedThenClassPlacedInDifferentNamespace()
        {
            TestWhenScriptNamespaceAttributeProvidedThenClassPlacedInDifferentNamespace();
        }

        [TestMethod]
        public void WhenScriptNamespaceAttributeProvidedWithCompilationThenClassPlacedInDifferentNamespace()
        {
            TestWhenScriptNamespaceAttributeProvidedThenClassPlacedInDifferentNamespace(true);
        }

        private static void TestWhenScriptNamespaceAttributeProvidedThenClassPlacedInDifferentNamespace(bool withCompilation = false)
        {
            var source = @"
            namespace OriginalNamespace {
                [ScriptNamespace(""NewNamespace"")]
                public class MyClass { }
            }
            ";

            var tree = CSharpSyntaxTree.ParseText(source) as CSharpSyntaxTree;

            // Loading MSCoreLib
            var compilation = withCompilation
                ? CSharpCompilation.Create("TestAssembly")
                    .AddReferences(
                        MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                    .AddSyntaxTrees(tree).AddScriptNamespaceReference()
                : null;

            // Transform
            if (withCompilation)
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree, ref compilation);
            }
            else
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree);
            }

            // Test
            var namespaceSyntaxNode = new NodeLocator(tree).LocateFirst(typeof(NamespaceDeclarationSyntax));
            Assert.IsNotNull(namespaceSyntaxNode, "A namespace was expected");
            var namespaceDeclarationNode = namespaceSyntaxNode as NamespaceDeclarationSyntax;
            Assert.IsNotNull(namespaceDeclarationNode);
            var namespaceHelper = new NamespaceDeclaration(namespaceDeclarationNode);
            Assert.AreEqual("NewNamespace", namespaceHelper.Name, "The name of the namespace should have changed");

            var classSyntaxNode = new NodeLocator(tree).LocateFirst(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(classSyntaxNode, "A class was expected");
            var classDeclarationNode = classSyntaxNode as ClassDeclarationSyntax;
            Assert.IsNotNull(namespaceDeclarationNode);
            var classHelper = new ClassDeclaration(classDeclarationNode);
            Assert.AreEqual("MyClass", classHelper.Name, "The name of the class should be the same");

            Assert.IsTrue(classSyntaxNode.IsChildOf(namespaceDeclarationNode), "Class expected to be child of namespace");
        }

        [TestMethod]
        public void WhenScriptNamespaceAttributeProvidedThenOldNamespaceAppearsNoMoreInAST()
        {
            TestWhenScriptNamespaceAttributeProvidedThenOldNamespaceAppearsNoMoreInAST();
        }

        [TestMethod]
        public void WhenScriptNamespaceAttributeProvidedWithCompilationThenOldNamespaceAppearsNoMoreInAST()
        {
            TestWhenScriptNamespaceAttributeProvidedThenOldNamespaceAppearsNoMoreInAST(true);
        }

        private static void TestWhenScriptNamespaceAttributeProvidedThenOldNamespaceAppearsNoMoreInAST(bool withCompilation = false)
        {
            var source = @"
            namespace OriginalNamespace {
                [ScriptNamespace(""NewNamespace"")]
                public class MyClass { }
            }
            ";

            var tree = CSharpSyntaxTree.ParseText(source) as CSharpSyntaxTree;

            // Loading MSCoreLib
            var compilation = withCompilation
                ? CSharpCompilation.Create("TestAssembly")
                    .AddReferences(
                        MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                    .AddSyntaxTrees(tree).AddScriptNamespaceReference()
                : null;

            // Transform
            if (withCompilation)
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree, ref compilation);
            }
            else
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree);
            }

            // Test
            var namespaceSyntaxNode = new NodeLocator(tree).LocateFirst(typeof(NamespaceDeclarationSyntax));
            Assert.IsNotNull(namespaceSyntaxNode, "A namespace was expected");
            var namespaceDeclarationNode = namespaceSyntaxNode as NamespaceDeclarationSyntax;
            Assert.IsNotNull(namespaceDeclarationNode);
            var namespaceHelper = new NamespaceDeclaration(namespaceDeclarationNode);
            Assert.AreEqual("NewNamespace", namespaceHelper.Name, "The name of the namespace should have changed");

            var oldNamespaceSyntaxNodes = new NodeLocator(tree).LocateAll(typeof(NamespaceDeclarationSyntax),
                node => node as NamespaceDeclarationSyntax != null &&
                new NamespaceDeclaration(node as NamespaceDeclarationSyntax).Name == "OriginalNamespace");
            Assert.AreEqual(0, oldNamespaceSyntaxNodes.Count(), "The old namespace is not supposed to be present");
        }

        [TestMethod]
        public void WhenASTIsChangedThenNoEmptyNamespacesAreLeftOver()
        {
            TestWhenASTIsChangedThenNoEmptyNamespacesAreLeftOver();
        }

        [TestMethod]
        public void WhenASTIsChangedWithCompilationThenNoEmptyNamespacesAreLeftOver()
        {
            TestWhenASTIsChangedThenNoEmptyNamespacesAreLeftOver(true);
        }

        private static void TestWhenASTIsChangedThenNoEmptyNamespacesAreLeftOver(bool withCompilation = false)
        {
            var source = @"
            namespace OriginalNamespace {
                [ScriptNamespace(""NewNamespace"")]
                public class Class1 { }

                [ScriptNamespace(""NewNamespace"")]
                public class Class2 { }

                [ScriptNamespace(""NewNamespace1"")]
                public class Class3 { }

                [ScriptNamespace(""NewNamespace1"")]
                public class Class4 { }
            }
            ";

            var tree = CSharpSyntaxTree.ParseText(source) as CSharpSyntaxTree;

            // Loading MSCoreLib
            var compilation = withCompilation
                ? CSharpCompilation.Create("TestAssembly")
                    .AddReferences(
                        MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                    .AddSyntaxTrees(tree).AddScriptNamespaceReference()
                : null;

            // Transform
            if (withCompilation)
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree, ref compilation);
            }
            else
            {
                new ScriptNamespaceBasedASTTransformer().Transform(ref tree);
            }

            // Test
            var oldNamespaceSyntaxNodes = new NodeLocator(tree).LocateAll(typeof(NamespaceDeclarationSyntax), 
                node => node as NamespaceDeclarationSyntax != null &&
                new NamespaceDeclaration(node as NamespaceDeclarationSyntax).Name == "OriginalNamespace");
            Assert.AreEqual(0, oldNamespaceSyntaxNodes.Count(), "Old namespace should not be present");
            
            var namespaceSyntaxNodes = new NodeLocator(tree).LocateAll(typeof(NamespaceDeclarationSyntax));
            Assert.IsTrue(namespaceSyntaxNodes.Count() > 0, "Namespaces expected");

            foreach (var namespaceNode in namespaceSyntaxNodes)
            {
                Assert.IsFalse((namespaceNode as NamespaceDeclarationSyntax).IsNamespaceEmpty(), "No empty namespace expected");
            }
        }

        /// <summary>
        /// When a <see cref="CSharpCompilation"/> is provided, the transformer will try to preserve references
        /// in the final tree by transplanting using directives in original namespaces into the root level of
        /// the generated AST.
        /// </summary>
        [TestMethod]
        public void UsingDirectivesAreTransplantedAtRootLevel()
        {
            var source = @"
            namespace Namespace1 {
                namespace Namespace11 {
                    public class Class111 { }
                }
                namespace Namespace12 {
                    public class Class121 { }
                }
            }

            namespace OriginalNamespace {
                using Namespace1.Namespace11;
                using Namespace1.Namespace12;

                [ScriptNamespace(""NewNamespace"")]
                public class Class1 { }
            }
            ";

            var tree = CSharpSyntaxTree.ParseText(source) as CSharpSyntaxTree;

            // Loading MSCoreLib
            var compilation = CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference();

            // Transform
            new ScriptNamespaceBasedASTTransformer().Transform(ref tree, ref compilation);

            // Test
            var oldNamespaceSyntaxNodes = new NodeLocator(tree).LocateAll(typeof(NamespaceDeclarationSyntax),
                node => node as NamespaceDeclarationSyntax != null &&
                new NamespaceDeclaration(node as NamespaceDeclarationSyntax).Name == "OriginalNamespace");
            // TODO: The next line cannot happen because at the moment the transformer leaves the ld namespace if
            //       using directives are present inside, later we might think about removing using directives if
            //       a namespace contains only using directivess parte of the cleanup process
            //Assert.AreEqual(0, oldNamespaceSyntaxNodes.Count(), "Old namespace should not be present");

            var usingSyntaxNodes = new NodeLocator(tree).LocateAll(typeof(UsingDirectiveSyntax));
            Assert.IsTrue(usingSyntaxNodes.Count() >= 2, "Original using directives should still be in the transformed AST");

            var inRootUsingSyntaxNodes = new NodeLocator(tree).LocateAll(typeof(UsingDirectiveSyntax), node => node.IsInRoot());
            Assert.IsTrue(inRootUsingSyntaxNodes.Count() >= 2, "Original using directives should have been transplanted into root");

            var directive1Nodes = new NodeLocator(tree).LocateAll(typeof(UsingDirectiveSyntax), 
                node => node.IsInRoot() && new UsingDirective(node as UsingDirectiveSyntax).Value == "Namespace1.Namespace11");
            Assert.AreEqual(1, directive1Nodes.Count(), "Existing using directive should have been transplanted into root");

            var directive2Nodes = new NodeLocator(tree).LocateAll(typeof(UsingDirectiveSyntax),
                node => node.IsInRoot() && new UsingDirective(node as UsingDirectiveSyntax).Value == "Namespace1.Namespace12");
            Assert.AreEqual(1, directive2Nodes.Count(), "Existing using directive should have been transplanted into root");
        }

        /// <summary>
        /// When a <see cref="CSharpCompilation"/> is provided, the transformer will try to preserve references
        /// in the final tree by adding special using directives, at root level, to the namespaces which were overriden.
        /// </summary>
        [TestMethod]
        public void OriginalOverridenNamespacesAreAddedAsUsingDirectivesAtRootLevel()
        {
            var source = @"
            namespace OriginalNamespace {
                [ScriptNamespace(""NewNamespace"")]
                public class Class1 { }
            }
            ";

            var tree = CSharpSyntaxTree.ParseText(source) as CSharpSyntaxTree;

            // Loading MSCoreLib
            var compilation = CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference();

            // Transform
            new ScriptNamespaceBasedASTTransformer().Transform(ref tree, ref compilation);

            // Test
            var directiveNodes = new NodeLocator(tree).LocateAll(typeof(UsingDirectiveSyntax),
                node => node.IsInRoot() && new UsingDirective(node as UsingDirectiveSyntax).Value == "OriginalNamespace");
            Assert.AreEqual(1, directiveNodes.Count(), "Original namespace should have been transplanted into root as using directive");
        }
    }
}
