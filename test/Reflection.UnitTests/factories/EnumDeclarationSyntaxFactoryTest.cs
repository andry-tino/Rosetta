/// <summary>
/// EnumDeclarationSyntaxFactoryTest.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Factories.UnitTests
{
    using System;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Reflection.Factories;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.UnitTests;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class EnumDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void NameCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public enum MyEnum {
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the enum
            ITypeInfoProxy enumDeclaration = assembly.LocateType("MyEnum");
            Assert.IsNotNull(enumDeclaration);

            // Generating the AST
            var factory = new EnumDeclarationSyntaxFactory(enumDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(EnumDeclarationSyntax), "Expected an enum declaration node to be built");

            var enumDeclarationSyntaxNode = syntaxNode as EnumDeclarationSyntax;

            var name = enumDeclarationSyntaxNode.Identifier.Text;
            Assert.AreEqual("MyEnum", name, "Enum name not correctly acquired");
        }

        [TestMethod]
        public void VisibilityCorrectlyAcquired()
        {
            // Public
            TestVisibility(@"
                namespace MyNamespace {
                    public enum MyEnum {
                    }
                }
            ", "MyEnum", SyntaxKind.PublicKeyword);

            // Implicitely internal
            TestVisibility(@"
                namespace MyNamespace {
                    enum MyEnum {
                    }
                }
            ", "MyEnum", null);
        }

        [TestMethod]
        public void Body()
        {
            
        }

        private static void TestVisibility(string source, string enumName, SyntaxKind? expectedVisibility)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the enum
            ITypeInfoProxy enumDefinition = assembly.LocateType(enumName);

            Assert.IsNotNull(enumDefinition);

            // Generating the AST
            var factory = new EnumDeclarationSyntaxFactory(enumDefinition);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(EnumDeclarationSyntax), "Expected an enum declaration node to be built");

            var enumDeclarationSyntaxNode = syntaxNode as EnumDeclarationSyntax;

            var modifiers = enumDeclarationSyntaxNode.Modifiers;

            if (expectedVisibility.HasValue)
            {
                Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility.Value), "Enum does not have correct visibility");
                return;
            }

            Assert.AreEqual(0, modifiers.Count(), "Expected no modifier");
        }
    }
}
