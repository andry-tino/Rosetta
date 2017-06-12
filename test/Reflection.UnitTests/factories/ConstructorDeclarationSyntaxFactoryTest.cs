/// <summary>
/// ConstructorDeclarationSyntaxFactoryTest.cs
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
    public class ConstructorDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void ConstructorCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public MyClass() {
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the ctor
            IConstructorInfoProxy ctorDeclaration = classDefinition.LocateConstructor(0);
            Assert.IsNotNull(ctorDeclaration);

            // Generating the AST
            var factory = new ConstructorDeclarationSyntaxFactory(ctorDeclaration, classDefinition);
            var syntaxNode = factory.Create() as ConstructorDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ConstructorDeclarationSyntax), "Expected a constructor declaration node to be built");
        }

        [TestMethod]
        public void ArgumentsCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public MyClass(string param1, int param2, double param3) {
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the ctor
            IConstructorInfoProxy ctorDeclaration = classDefinition.LocateConstructor(3);
            Assert.IsNotNull(ctorDeclaration);

            // Generating the AST
            var factory = new ConstructorDeclarationSyntaxFactory(ctorDeclaration, classDefinition);
            var syntaxNode = factory.Create() as ConstructorDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ConstructorDeclarationSyntax), "Expected a constructor declaration node to be built");

            var args = syntaxNode.ParameterList.Parameters;
            Assert.AreEqual(3, args.Count, "Expected 3 parameters");

            Action<int, string, string> ParamChecker = (index, expectedName, expectedTypeFullName) =>
            {
                var @param = args[index];
                Assert.IsNotNull(@param, "Parameter expected");

                var identifier = @param.Identifier;
                Assert.IsNotNull(identifier, "Identifier expected");
                Assert.AreEqual(identifier.ToString(), expectedName, "Parameter name does not match");

                var type = @param.Type;
                Assert.IsNotNull(type, "Type expected");

                var typeIdentifier = type as QualifiedNameSyntax;
                Assert.IsNotNull(typeIdentifier, "Type expected to be qualified name");
                Assert.AreEqual(type.ToString(), expectedTypeFullName, "Parameter name does not match");
            };

            ParamChecker(0, "param1", "System.String");
            ParamChecker(1, "param2", "System.Int32");
            ParamChecker(2, "param3", "System.Double");
        }

        [TestMethod]
        public void VisibilityCorrectlyAcquired()
        {
            // Public
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        public MyClass() {
                        }
                    }
                }
            ", "MyClass", SyntaxKind.PublicKeyword);

            // Private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        private MyClass() {
                        }
                    }
                }
            ", "MyClass", SyntaxKind.PrivateKeyword);

            // Protected
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        protected MyClass() {
                        }
                    }
                }
            ", "MyClass", SyntaxKind.ProtectedKeyword);

            // Implicitely private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        MyClass() {
                        }
                    }
                }
            ", "MyClass", SyntaxKind.PrivateKeyword);
        }

        [TestMethod]
        public void DummyBody()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        MyClass() {
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            // Locating the method
            IConstructorInfoProxy ctorDeclaration = classDefinition.LocateConstructor(0);
            Assert.IsNotNull(ctorDeclaration);

            // Generating the AST
            var factory = new ConstructorDeclarationSyntaxFactory(ctorDeclaration, classDefinition);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ConstructorDeclarationSyntax), "Expected a constructor declaration node to be built");

            var constructorDeclarationSyntaxNode = syntaxNode as ConstructorDeclarationSyntax;

            var body = constructorDeclarationSyntaxNode.Body;
            Assert.IsNotNull(body, "Expected a body");

            var statements = body.Statements;
            Assert.IsNotNull(statements, "Expected a non empty body");
            Assert.AreEqual(1, statements.Count(), "Expected a body with 1 statement only");

            var statement = statements.First();
            Assert.IsNotNull(statement, "Expected one single statement");
            Assert.IsInstanceOfType(statement, typeof(ThrowStatementSyntax), "Expected a throw statement");
        }

        private static void TestVisibility(string source, string className, SyntaxKind expectedVisibility)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType(className);
            Assert.IsNotNull(classDefinition);

            // Locating the method
            IConstructorInfoProxy ctorDeclaration = classDefinition.LocateConstructor(0);
            Assert.IsNotNull(ctorDeclaration);

            // Generating the AST
            var factory = new ConstructorDeclarationSyntaxFactory(ctorDeclaration, classDefinition);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ConstructorDeclarationSyntax), "Expected a constructor declaration node to be built");

            var constructorDeclarationSyntaxNode = syntaxNode as ConstructorDeclarationSyntax;

            var modifiers = constructorDeclarationSyntaxNode.Modifiers;

            Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility), "Constructor does not have correct visibility");
        }
    }
}
