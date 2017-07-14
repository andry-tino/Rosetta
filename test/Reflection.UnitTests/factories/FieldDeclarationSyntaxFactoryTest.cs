/// <summary>
/// FieldDeclarationSyntaxFactoryTest.cs
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
    public class FieldDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void NameCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public int myField;
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the field
            IFieldInfoProxy fieldDeclaration = classDefinition.LocateField("myField");
            Assert.IsNotNull(fieldDeclaration);

            // Generating the AST
            var factory = new FieldDeclarationSyntaxFactory(fieldDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(FieldDeclarationSyntax), "Expected a field declaration node to be built");

            var fieldDeclarationSyntaxNode = syntaxNode as FieldDeclarationSyntax;

            var variables = fieldDeclarationSyntaxNode.Declaration.Variables;
            Assert.IsNotNull(variables);
            Assert.IsTrue(variables.Any());

            var variable = fieldDeclarationSyntaxNode.Declaration.Variables.First();
            Assert.IsNotNull(variable);

            var name = variable.Identifier.Text;
            Assert.AreEqual("myField", name, "Field name not correctly acquired");
        }

        [TestMethod]
        public void StaticModifierCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public static int myField1;
                        public int myField2;
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            Action<string, bool> CheckStatic = (fieldName, expected) =>
            {
                // Locating the field
                IFieldInfoProxy fieldDeclaration = classDefinition.LocateField(fieldName);
                Assert.IsNotNull(fieldDeclaration);

                // Generating the AST
                var factory = new FieldDeclarationSyntaxFactory(fieldDeclaration);
                var syntaxNode = factory.Create();

                Assert.IsNotNull(syntaxNode, "A node was expected to be built");
                Assert.IsInstanceOfType(syntaxNode, typeof(FieldDeclarationSyntax), "Expected a field declaration node to be built");

                var fieldDeclarationSyntaxNode = syntaxNode as FieldDeclarationSyntax;

                var modifiers = fieldDeclarationSyntaxNode.Modifiers;
                Assert.IsNotNull(modifiers);

                var staticModifier = modifiers.Where(modifier => modifier.Kind() == SyntaxKind.StaticKeyword);
                Assert.AreEqual(expected ? 1 : 0, staticModifier.Count(), expected ? "Expected one static modifier" : "No static modifier expected");
            };

            CheckStatic("myField1", true);
            CheckStatic("myField2", false);
        }

        [TestMethod]
        public void TypeCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public int myField;
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the field
            IFieldInfoProxy fieldDeclaration = classDefinition.LocateField("myField");
            Assert.IsNotNull(fieldDeclaration);

            // Generating the AST
            var factory = new FieldDeclarationSyntaxFactory(fieldDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(FieldDeclarationSyntax), "Expected a field declaration node to be built");

            var fieldDeclarationSyntaxNode = syntaxNode as FieldDeclarationSyntax;

            var type = fieldDeclarationSyntaxNode.Declaration.Type;
            Assert.IsNotNull(type);

            var typeIdentifier = type as QualifiedNameSyntax;
            Assert.IsNotNull(typeIdentifier, "Type expected to be qualified name");
            Assert.AreEqual("System.Int32", typeIdentifier.ToString(), "Parameter name does not match");
        }

        [TestMethod]
        public void VisibilityCorrectlyAcquired()
        {
            // Public
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        public int myField;
                    }
                }
            ", "MyClass", "myField", SyntaxKind.PublicKeyword);

            // Private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        private int myField;
                    }
                }
            ", "MyClass", "myField", SyntaxKind.PrivateKeyword);

            // Protected
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        protected int myField;
                    }
                }
            ", "MyClass", "myField", SyntaxKind.ProtectedKeyword);

            // Implicitely private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        int myField;
                    }
                }
            ", "MyClass", "myField", SyntaxKind.PrivateKeyword);
        }

        private static void TestVisibility(string source, string className, string propertyName, SyntaxKind expectedVisibility)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the field
            IFieldInfoProxy fieldDeclaration = classDefinition.LocateField("myField");
            Assert.IsNotNull(fieldDeclaration);

            // Generating the AST
            var factory = new FieldDeclarationSyntaxFactory(fieldDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(FieldDeclarationSyntax), "Expected a field declaration node to be built");

            var fieldDeclarationSyntaxNode = syntaxNode as FieldDeclarationSyntax;

            var modifiers = fieldDeclarationSyntaxNode.Modifiers;

            Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility), "Method does not have correct visibility");
        }
    }
}
