/// <summary>
/// MethodDeclarationSyntaxFactoryTest.cs
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
    public class MethodDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void NameCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public void MyMethod() {
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
            IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod("MyMethod");
            Assert.IsNotNull(methodDeclaration);

            // Generating the AST
            var factory = new MethodDeclarationSyntaxFactory(methodDeclaration);
            var syntaxNode = factory.Create();
            
            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

            var methodDeclarationSyntaxNode = syntaxNode as MethodDeclarationSyntax;

            var name = methodDeclarationSyntaxNode.Identifier.Text;
            Assert.AreEqual("MyMethod", name, "Method name not correctly acquired");
        }

        [TestMethod]
        public void ArgumentsCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public void MyMethod(string param1, int param2, double param3) {
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
            IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod("MyMethod");
            Assert.IsNotNull(methodDeclaration);

            // Generating the AST
            var factory = new MethodDeclarationSyntaxFactory(methodDeclaration);
            var syntaxNode = factory.Create() as MethodDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

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
        public void ReturnTypeCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public int MyMethod() {
                            return 0;
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
            IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod("MyMethod");
            Assert.IsNotNull(methodDeclaration);

            // Generating the AST
            var factory = new MethodDeclarationSyntaxFactory(methodDeclaration);
            var syntaxNode = factory.Create() as MethodDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

            var returnType = syntaxNode.ReturnType;
            Assert.IsNotNull(returnType);

            var typeIdentifier = returnType as QualifiedNameSyntax;
            Assert.IsNotNull(typeIdentifier, "Type expected to be qualified name");
            Assert.AreEqual("System.Int32", typeIdentifier.ToString(), "Parameter name does not match");
        }

        [TestMethod]
        public void StaticModifierCorrectlyAcquired()
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(@"
                namespace MyNamespace {
                    public class MyClass {
                        public static int MyMethod1() {
                            return 0;
                        }
                        public int MyMethod2() {
                            return 0;
                        }
                    }
                }
            ");

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            Action<string, bool> CheckStatic = (methodName, expected) => 
            {
                // Locating the method
                IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod(methodName);
                Assert.IsNotNull(methodDeclaration);

                // Generating the AST
                var factory = new MethodDeclarationSyntaxFactory(methodDeclaration);
                var syntaxNode = factory.Create() as MethodDeclarationSyntax;

                Assert.IsNotNull(syntaxNode, "A node was expected to be built");
                Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

                var modifiers = syntaxNode.Modifiers;
                Assert.IsNotNull(modifiers);

                var staticModifier = modifiers.Where(modifier => modifier.Kind() == SyntaxKind.StaticKeyword);
                Assert.AreEqual(expected ? 1 : 0, staticModifier.Count(), expected ? "Expected one static modifier" : "No static modifier expected");
            };

            CheckStatic("MyMethod1", true);
            CheckStatic("MyMethod2", false);
        }

        [TestMethod]
        public void VisibilityCorrectlyAcquired()
        {
            // Public
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        public void MyMethod() {
                        }
                    }
                }
            ", "MyClass", "MyMethod", SyntaxKind.PublicKeyword);

            // Private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        private void MyMethod() {
                            var number = 0;
                        }
                    }
                }
            ", "MyClass", "MyMethod", SyntaxKind.PrivateKeyword);

            // Protected
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        protected void MyMethod() {
                        }
                    }
                }
            ", "MyClass", "MyMethod", SyntaxKind.ProtectedKeyword);

            // Implicitely private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        void MyMethod() {
                        }
                    }
                }
            ", "MyClass", "MyMethod", SyntaxKind.PrivateKeyword);
        }

        [TestMethod]
        public void DummyBody()
        {
            // Void
            TestDummyBody(@"
                namespace MyNamespace {
                    public class MyClass {
                        void MyMethod() {
                            var number = 0;
                        }
                    }
                }
            ", "MyClass", "MyMethod");

            // With return
            TestDummyBody(@"
                namespace MyNamespace {
                    public class MyClass {
                        int MyMethod() {
                            return 0;
                        }
                    }
                }
            ", "MyClass", "MyMethod");
        }

        private static void TestDummyBody(string source, string className, string methodName)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType(className);
            Assert.IsNotNull(classDefinition);

            // Locating the method
            IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod(methodName);
            Assert.IsNotNull(methodDeclaration);

            // Generating the AST
            var factory = new MethodDeclarationSyntaxFactory(methodDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

            var methodDeclarationSyntaxNode = syntaxNode as MethodDeclarationSyntax;

            var body = methodDeclarationSyntaxNode.Body;
            Assert.IsNotNull(body, "Expected a body");

            var statements = body.Statements;
            Assert.IsNotNull(statements, "Expected a non empty body");
            Assert.AreEqual(1, statements.Count(), "Expected a body with 1 statement only");

            var statement = statements.First();
            Assert.IsNotNull(statement, "Expected one single statement");
            Assert.IsInstanceOfType(statement, typeof(ThrowStatementSyntax), "Expected a throw statement");
        }

        private static void TestVisibility(string source, string className, string methodName, SyntaxKind expectedVisibility)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType(className);
            Assert.IsNotNull(classDefinition);

            // Locating the method
            IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod(methodName);
            Assert.IsNotNull(methodDeclaration);

            // Generating the AST
            var factory = new MethodDeclarationSyntaxFactory(methodDeclaration);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

            var methodDeclarationSyntaxNode = syntaxNode as MethodDeclarationSyntax;

            var modifiers = methodDeclarationSyntaxNode.Modifiers;

            Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility), "Method does not have correct visibility");
        }
    }
}
