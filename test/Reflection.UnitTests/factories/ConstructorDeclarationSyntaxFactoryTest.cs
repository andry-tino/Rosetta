/// <summary>
/// ConstructorDeclarationSyntaxFactoryTest.cs
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
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ConstructorDeclarationSyntax), "Expected a constructor declaration node to be built");
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
            ", SyntaxKind.PublicKeyword);

            // Private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        private MyClass() {
                        }
                    }
                }
            ", SyntaxKind.PrivateKeyword);

            // Protected
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        protected MyClass() {
                        }
                    }
                }
            ", SyntaxKind.ProtectedKeyword);

            // Implicitely private
            TestVisibility(@"
                namespace MyNamespace {
                    public class MyClass {
                        MyClass() {
                        }
                    }
                }
            ", SyntaxKind.PrivateKeyword);
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
            IConstructorInfoProxy ctorDeclaration = classDefinition.LocateConstructor(0);
            Assert.IsNotNull(ctorDeclaration);

            // Generating the AST
            var factory = new ConstructorDeclarationSyntaxFactory(ctorDeclaration, classDefinition);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ConstructorDeclarationSyntax), "Expected a constructor declaration node to be built");

            var modifiers = syntaxNode.Modifiers;

            Assert.IsTrue(Utils.CheckModifier(modifiers, expectedVisibility), "Constructor does not have correct visibility");
        }
    }
}
