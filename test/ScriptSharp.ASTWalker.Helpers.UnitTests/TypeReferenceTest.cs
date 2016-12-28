/// <summary>
/// TypeReferenceTest.cs
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

    using Rosetta.ScriptSharp.AST.Helpers;
    using Rosetta.Tests.ScriptSharp.Utils;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for the <see cref="TypeReference"/> classes.
    /// </summary>
    [TestClass]
    public class TypeReferenceTest
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
        public void ScriptNamespaceRetrievedOnClassMethodReturnType()
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

        [TestMethod]
        public void ScriptNamespaceRetrievedOnClassMethodArgumentType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public class Class2 {
                    public void MyMethod(Class1 parameter) { }
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

            var parameters = new MethodDeclaration(methodDeclarationNode, semanticModel).Parameters;
            Assert.AreEqual(1, parameters.Count(), "1 argument expected");
            var typeReference = parameters.ElementAt(0).Type;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }

        [TestMethod]
        public void ScriptNamespaceRetrievedOnConstructorArgumentType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public class Class2 {
                    public Class2(Class1 parameter) { }
                }
            }
            ");

            // Loading MSCoreLib
            var semanticModel = (CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference().GetSemanticModel(tree));

            var node = new NodeLocator(tree).LocateFirst(typeof(ConstructorDeclarationSyntax));
            Assert.IsNotNull(node);

            var ctorDeclarationNode = node as ConstructorDeclarationSyntax;
            Assert.IsNotNull(ctorDeclarationNode);

            var parameters = new ConstructorDeclaration(ctorDeclarationNode, semanticModel).Parameters;
            Assert.AreEqual(1, parameters.Count(), "1 argument expected");
            var typeReference = parameters.ElementAt(0).Type;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }

        [TestMethod]
        public void ScriptNamespaceRetrievedOnClassFieldType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public class Class2 {
                    protected Class1 myField;
                }
            }
            ");

            // Loading MSCoreLib
            var semanticModel = (CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference().GetSemanticModel(tree));

            var node = new NodeLocator(tree).LocateFirst(typeof(FieldDeclarationSyntax));
            Assert.IsNotNull(node);

            var fieldDeclarationNode = node as FieldDeclarationSyntax;
            Assert.IsNotNull(fieldDeclarationNode);

            var typeReference = new FieldDeclaration(fieldDeclarationNode, semanticModel).Type;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }

        [TestMethod]
        public void ScriptNamespaceRetrievedOnClassPropertyType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
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
            ");

            // Loading MSCoreLib
            var semanticModel = (CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference().GetSemanticModel(tree));

            var node = new NodeLocator(tree).LocateFirst(typeof(PropertyDeclarationSyntax));
            Assert.IsNotNull(node);

            var propertyDeclarationNode = node as PropertyDeclarationSyntax;
            Assert.IsNotNull(propertyDeclarationNode);

            var typeReference = new PropertyDeclaration(propertyDeclarationNode, semanticModel).Type;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }

        [TestMethod]
        public void ScriptNamespaceRetrievedOnClassAutoPropertyType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public class Class2 {
                    public Class1 MyProperty { get; set; }
                }
            }
            ");

            // Loading MSCoreLib
            var semanticModel = (CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference().GetSemanticModel(tree));

            var node = new NodeLocator(tree).LocateFirst(typeof(PropertyDeclarationSyntax));
            Assert.IsNotNull(node);

            var propertyDeclarationNode = node as PropertyDeclarationSyntax;
            Assert.IsNotNull(propertyDeclarationNode);

            var typeReference = new PropertyDeclaration(propertyDeclarationNode, semanticModel).Type;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }

        [TestMethod]
        public void ScriptNamespaceRetrievedOnInterfaceMethodReturnType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public interface Interface1 {
                    Class1 MyMethod();
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

        [TestMethod]
        public void ScriptNamespaceRetrievedOnInterfaceMethodArgumentType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public interface Interface1 {
                    void MyMethod(Class1 parameter);
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

            var parameters = new MethodDeclaration(methodDeclarationNode, semanticModel).Parameters;
            Assert.AreEqual(1, parameters.Count(), "1 argument expected");
            var typeReference = parameters.ElementAt(0).Type;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }

        [TestMethod]
        public void ScriptNamespaceRetrievedOnInterfacePropertyType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            namespace Namespace1 {
                [ScriptNamespace(""OverridenNamespace"")]
                public class Class1 { }
                public interface Interface1 {
                    Class1 MyProperty { get; set; }
                }
            }
            ");

            // Loading MSCoreLib
            var semanticModel = (CSharpCompilation.Create("TestAssembly")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree).AddScriptNamespaceReference().GetSemanticModel(tree));

            var node = new NodeLocator(tree).LocateFirst(typeof(PropertyDeclarationSyntax));
            Assert.IsNotNull(node);

            var propertyDeclarationNode = node as PropertyDeclarationSyntax;
            Assert.IsNotNull(propertyDeclarationNode);

            var typeReference = new PropertyDeclaration(propertyDeclarationNode, semanticModel).Type;

            Assert.AreEqual("OverridenNamespace.Class1", typeReference.FullName, "ScriptNamespace overriden namespace not detected!");
        }
    }
}
