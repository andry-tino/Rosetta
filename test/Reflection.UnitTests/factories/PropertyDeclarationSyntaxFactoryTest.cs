/// <summary>
/// PropertyDeclarationSyntaxFactoryTest.cs
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
    public class PropertyDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void NameCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public int MyProperty {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the property
            IPropertyInfoProxy propertyDeclaration = classDefinition.LocateProperty("MyProperty");
            Assert.IsNotNull(propertyDeclaration);

            // Generating the AST
            var factory = new PropertyDeclarationSyntaxFactory(propertyDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(PropertyDeclarationSyntax), "Expected a property declaration node to be built");

            var propertyDeclarationSyntaxNode = syntaxNode as PropertyDeclarationSyntax;

            var name = propertyDeclarationSyntaxNode.Identifier.Text;
            Assert.AreEqual("MyProperty", name, "Property name not correctly acquired");
        }

        [TestMethod]
        public void StaticModifierCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public static int MyProperty1 {
                            get { return 0; }
                            set { }
                        }
                        public int MyProperty2 {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            Action<string, bool> CheckStatic = (propertyName, expected) =>
            {
                // Locating the property
                IPropertyInfoProxy propertyDeclaration = classDefinition.LocateProperty(propertyName);
                Assert.IsNotNull(propertyDeclaration);

                // Generating the AST
                var factory = new PropertyDeclarationSyntaxFactory(propertyDeclaration);
                var syntaxNode = factory.Create();

                Assert.IsNotNull(syntaxNode, "A node was expected to be built");
                Assert.IsInstanceOfType(syntaxNode, typeof(PropertyDeclarationSyntax), "Expected a property declaration node to be built");

                var propertyDeclarationSyntaxNode = syntaxNode as PropertyDeclarationSyntax;

                var modifiers = propertyDeclarationSyntaxNode.Modifiers;
                Assert.IsNotNull(modifiers);

                var staticModifier = modifiers.Where(modifier => modifier.Kind() == SyntaxKind.StaticKeyword);
                Assert.AreEqual(expected ? 1 : 0, staticModifier.Count(), expected ? "Expected one static modifier" : "No static modifier expected");
            };

            CheckStatic("MyProperty1", true);
            CheckStatic("MyProperty2", false);
        }

        [TestMethod]
        public void ReturnTypeCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public int MyProperty {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the property
            IPropertyInfoProxy propertyDeclaration = classDefinition.LocateProperty("MyProperty");
            Assert.IsNotNull(propertyDeclaration);

            // Generating the AST
            var factory = new PropertyDeclarationSyntaxFactory(propertyDeclaration);
            var syntaxNode = factory.Create() as PropertyDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(PropertyDeclarationSyntax), "Expected a property declaration node to be built");

            var returnType = syntaxNode.Type;
            Assert.IsNotNull(returnType);

            var typeIdentifier = returnType as QualifiedNameSyntax;
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
                        public int MyProperty {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ", "MyClass", "MyProperty", SyntaxKind.PublicKeyword);

            // Private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        private int MyProperty {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ", "MyClass", "MyProperty", SyntaxKind.PrivateKeyword);

            // Protected
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        protected int MyProperty {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ", "MyClass", "MyProperty", SyntaxKind.ProtectedKeyword);

            // Implicitely private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        int MyProperty {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ", "MyClass", "MyProperty", SyntaxKind.PrivateKeyword);
        }

        [TestMethod]
        public void DummyBody()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        int MyProperty {
                            get { return 0; }
                            set { }
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the property
            IPropertyInfoProxy propertyDeclaration = classDefinition.LocateProperty("MyProperty");
            Assert.IsNotNull(propertyDeclaration);

            // Generating the AST
            var factory = new PropertyDeclarationSyntaxFactory(propertyDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(PropertyDeclarationSyntax), "Expected a property declaration node to be built");

            var propertyDeclarationSyntaxNode = syntaxNode as PropertyDeclarationSyntax;

            Assert.IsNotNull(propertyDeclarationSyntaxNode.AccessorList);
            var accessors = propertyDeclarationSyntaxNode.AccessorList.Accessors;
            Assert.IsNotNull(accessors);
            Assert.AreEqual(2, accessors.Count, "2 accessors expected");

            var body1 = accessors.First().Body;
            var body2 = accessors.Last().Body;
            Assert.IsNotNull(body1, "Expected a body");
            Assert.IsNotNull(body2, "Expected a body");

            var statements1 = body1.Statements;
            var statements2 = body2.Statements;
            Assert.IsNotNull(statements1, "Expected a non empty body");
            Assert.AreEqual(1, statements1.Count(), "Expected a body with 1 statement only");
            Assert.IsNotNull(statements2, "Expected a non empty body");
            Assert.AreEqual(1, statements2.Count(), "Expected a body with 1 statement only");

            var statement1 = statements1.First();
            var statement2 = statements2.First();
            Assert.IsNotNull(statement1, "Expected one single statement");
            Assert.IsInstanceOfType(statement1, typeof(ThrowStatementSyntax), "Expected a throw statement");
            Assert.IsNotNull(statement2, "Expected one single statement");
            Assert.IsInstanceOfType(statement2, typeof(ThrowStatementSyntax), "Expected a throw statement");
        }

        [TestMethod]
        public void NoBody()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public interface MyInterface {
                        int MyProperty { get; set; }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy interfaceDefinition = assembly.LocateType("MyInterface");
            Assert.IsNotNull(interfaceDefinition);

            // Locating the property
            IPropertyInfoProxy propertyDeclaration = interfaceDefinition.LocateProperty("MyProperty");
            Assert.IsNotNull(propertyDeclaration);

            // Generating the AST
            var factory = new PropertyDeclarationSyntaxFactory(propertyDeclaration, false);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(PropertyDeclarationSyntax), "Expected a property declaration node to be built");

            var propertyDeclarationSyntaxNode = syntaxNode as PropertyDeclarationSyntax;

            Assert.IsNotNull(propertyDeclarationSyntaxNode.AccessorList);
            var accessors = propertyDeclarationSyntaxNode.AccessorList.Accessors;
            Assert.IsNotNull(accessors);
            Assert.AreEqual(2, accessors.Count, "2 accessors expected");

            var body1 = accessors.First().Body;
            var body2 = accessors.Last().Body;
            Assert.IsNull(body1, "Expected a null body");
            Assert.IsNull(body2, "Expected a null body");
        }

        private static void TestVisibility(string source, string className, string propertyName, SyntaxKind expectedVisibility)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType(className);
            Assert.IsNotNull(classDefinition);

            // Locating the method
            IPropertyInfoProxy propertyDeclaration = classDefinition.LocateProperty(propertyName);
            Assert.IsNotNull(propertyDeclaration);

            // Generating the AST
            var factory = new PropertyDeclarationSyntaxFactory(propertyDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(PropertyDeclarationSyntax), "Expected a property declaration node to be built");

            var propertyDeclarationSyntaxNode = syntaxNode as PropertyDeclarationSyntax;

            var modifiers = propertyDeclarationSyntaxNode.Modifiers;

            Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility), "Method does not have correct visibility");
        }
    }
}
