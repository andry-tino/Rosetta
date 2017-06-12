/// <summary>
/// MethodDeclarationSyntaxFactoryTest.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Factories.UnitTests
{
    using System;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Reflection.ScriptSharp.Factories;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.UnitTests;
    using ReflectionUtils = Rosetta.Reflection.UnitTests.Utils;
    using ScriptSharpReflectionUtils = Rosetta.Reflection.ScriptSharp.UnitTests.Utils;
    using Rosetta.Tests.ScriptSharp.Utils;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class MethodDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void ArgumentsCorrectlyAcquired()
        {
            TestArgumentsCorrectlyAcquired(@"
                namespace MyNamespace {
                    [ScriptNamespace(""NewNamespace"")]
                    public class Type1 { }
                    [ScriptNamespace(""NewNamespace"")]
                    public class Type2 { }
                    [ScriptNamespace(""NewNamespace"")]
                    public class Type3 { }
                    public class MyClass {
                        public void MyMethod(Type1 param1, Type2 param2, Type3 param3) {
                        }
                    }
                }
            ", "MyClass", "MyMethod", 
            new[] { "param1", "param2", "param3" }, new[] { "NewNamespace.Type1", "NewNamespace.Type2", "NewNamespace.Type3" });
        }

        [TestMethod]
        public void ArgumentsCorrectlyAcquiredInRootCompilationUnit()
        {
            TestArgumentsCorrectlyAcquired(@"
                [ScriptNamespace(""NewNamespace"")]
                public class Type1 { }
                [ScriptNamespace(""NewNamespace"")]
                public class Type2 { }
                [ScriptNamespace(""NewNamespace"")]
                public class Type3 { }
                public class MyClass {
                    public void MyMethod(Type1 param1, Type2 param2, Type3 param3) {
                    }
                }
            ", "MyClass", "MyMethod", 
            new[] { "param1", "param2", "param3" }, new[] { "NewNamespace.Type1", "NewNamespace.Type2", "NewNamespace.Type3" });
        }

        [TestMethod]
        public void ReturnTypeCorrectlyAcquired()
        {
            TestReturnTypeCorrectlyAcquired(@"
                namespace MyNamespace {
                    [ScriptNamespace(""NewNamespace"")]
                    public class Type1 { }
                    public class MyClass {
                        public Type1 MyMethod() {
                            return null;
                        }
                    }
                }
            ", "MyClass", "MyMethod", "NewNamespace.Type1");
        }

        [TestMethod]
        public void ReturnTypeCorrectlyAcquiredInRootCompilationUnit()
        {
            TestReturnTypeCorrectlyAcquired(@"
                [ScriptNamespace(""NewNamespace"")]
                public class Type1 { }
                public class MyClass {
                    public Type1 MyMethod() {
                        return null;
                    }
                }
            ", "MyClass", "MyMethod", "NewNamespace.Type1");
        }

        private static void TestReturnTypeCorrectlyAcquired(string source, string className, string methodName, string expectedTypeFullName)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new ScriptSharpReflectionUtils.AsmlDasmlAssemblyLoader(source, 
                ScriptNamespaceAttributeHelper.AttributeSourceCode);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();
            ITypeLookup lookup = new LinearSearchTypeLookup(assembly);

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType(className);
            Assert.IsNotNull(classDefinition);

            // Locating the method
            IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod(methodName);
            Assert.IsNotNull(methodDeclaration);

            // Generating the AST
            var factory = new MethodDeclarationSyntaxFactory(methodDeclaration, lookup);
            var syntaxNode = factory.Create() as MethodDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

            var returnType = syntaxNode.ReturnType;
            Assert.IsNotNull(returnType);

            var typeIdentifier = returnType as QualifiedNameSyntax;
            Assert.IsNotNull(typeIdentifier, "Type expected to be qualified name");
            Assert.AreEqual(expectedTypeFullName, typeIdentifier.ToString(), "Parameter name does not match");
        }

        private static void TestArgumentsCorrectlyAcquired(string source, string className, string methodName, 
            string[] expectedParamNames, string[] expectedParamTypeFullNames)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new ScriptSharpReflectionUtils.AsmlDasmlAssemblyLoader(source,
                ScriptNamespaceAttributeHelper.AttributeSourceCode);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();
            ITypeLookup lookup = new LinearSearchTypeLookup(assembly);

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType(className);
            Assert.IsNotNull(classDefinition);

            // Locating the method
            IMethodInfoProxy methodDeclaration = classDefinition.LocateMethod(methodName);
            Assert.IsNotNull(methodDeclaration);

            // Generating the AST
            var factory = new MethodDeclarationSyntaxFactory(methodDeclaration, lookup);
            var syntaxNode = factory.Create() as MethodDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(MethodDeclarationSyntax), "Expected a method declaration node to be built");

            var args = syntaxNode.ParameterList.Parameters;
            Assert.AreEqual(expectedParamNames.Length, args.Count, $"Expected {expectedParamNames.Length} parameters");

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

            for (int i = 0; i < expectedParamNames.Length; i++)
            {
                ParamChecker(i, expectedParamNames[i], expectedParamTypeFullNames[i]);
            }
        }
    }
}
