/// <summary>
/// MethodDeclarationSyntaxFactoryTest.cs
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
    public class MethodDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void MethodNameCorrectlyAcquired()
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

            var name = syntaxNode.Identifier.Text;
            Assert.AreEqual("MyMethod", name, "Method name not correctly acquired");
        }

        [TestMethod]
        public void ClassVisibilityCorrectlyAcquired()
        {
            // Public
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        public void MyMethod() {
                        }
                    }
                }
            ", SyntaxKind.PublicKeyword);

            // Private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        private void MyMethod() {
                            var number = 0;
                        }
                    }
                }
            ", SyntaxKind.PrivateKeyword);

            // Protected
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        protected void MyMethod() {
                        }
                    }
                }
            ", SyntaxKind.ProtectedKeyword);

            // Implicitely private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        void MyMethod() {
                        }
                    }
                }
            ", SyntaxKind.PrivateKeyword);
        }

        [TestMethod]
        public void MethodDummyBody()
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
            ");

            // With return
            TestDummyBody(@"
                namespace MyNamespace {
                    public class MyClass {
                        int MyMethod() {
                            return 0;
                        }
                    }
                }
            ");
        }

        private static void TestDummyBody(string source)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

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

            var body = syntaxNode.Body;
            Assert.IsNotNull(body, "Expected a body");

            var statements = body.Statements;
            Assert.IsNotNull(statements, "Expected a non empty body");
            Assert.AreEqual(1, statements.Count(), "Expected a body with 1 statement only");

            var statement = statements.First();
            Assert.IsNotNull(statement, "Expected one single statement");
            Assert.IsInstanceOfType(statement, typeof(ThrowStatementSyntax), "Expected a throw statement");
        }

        private static void TestVisibility(string source, SyntaxKind expectedVisibility)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

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

            var modifiers = syntaxNode.Modifiers;

            Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility), "Method does not have correct visibility");
        }
    }
}
