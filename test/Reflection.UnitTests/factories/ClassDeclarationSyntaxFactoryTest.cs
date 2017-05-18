﻿/// <summary>
/// ClassDeclarationSyntaxFactoryTest.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.UnitTests
{
    using System;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Reflection.Factories;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ClassDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void ClassNameCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                class MyClass {
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");

            Assert.IsNotNull(classDefinition);

            // Generating the AST
            var factory = new ClassDeclarationSyntaxFactory(classDefinition);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ClassDeclarationSyntax), "Expected a class declaration node to be built");

            var name = syntaxNode.Identifier.Text;
            Assert.AreEqual("MyClass", name, "Class name not correctly acquired");
        }

        [TestMethod]
        public void ImplicitObjectCLassInheritanceIsNotGenerated()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                class MyClass {
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");

            Assert.IsNotNull(classDefinition);

            // Generating the AST
            var factory = new ClassDeclarationSyntaxFactory(classDefinition);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ClassDeclarationSyntax), "Expected a class declaration node to be built");

            var baseList = syntaxNode.BaseList;
            Assert.IsNull(baseList, "No base class should have been generated");
        }

        [TestMethod]
        public void ClassVisibilityCorrectlyAcquired()
        {
            // Public
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                    }
                }
            ", SyntaxKind.PublicKeyword);

            // Implicitely internal
            TestVisibility(@"
                namespace MyNamespace {
                    class MyClass {
                    }
                }
            ", null);
        }

        private static void TestVisibility(string source, SyntaxKind? expectedVisibility)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");

            Assert.IsNotNull(classDefinition);

            // Generating the AST
            var factory = new ClassDeclarationSyntaxFactory(classDefinition);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ClassDeclarationSyntax), "Expected a class declaration node to be built");

            var modifiers = syntaxNode.Modifiers;

            if (expectedVisibility.HasValue)
            {
                Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility.Value), "Class does not have correct visibility");
                return;
            }

            Assert.AreEqual(0, modifiers.Count(), "Expected no modifier");
        }
    }
}