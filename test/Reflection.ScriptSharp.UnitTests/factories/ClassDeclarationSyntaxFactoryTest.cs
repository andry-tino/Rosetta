/// <summary>
/// ClassDeclarationSyntaxFactoryTest.cs
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
    public class ClassDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void BaseClassFullName()
        {
            TestBaseClassFullName(@"
                namespace MyNamespace1 {
                    [ScriptNamespace(""NewNamespace"")]
                    public class MyClass {
                    }
                    public class MyOtherClass : MyClass {
                    }
                }
            ", "MyOtherClass", "NewNamespace.MyClass");
        }

        [TestMethod]
        public void BaseClassFullNameInRootCompilationUnit()
        {
            TestBaseClassFullName(@"
                [ScriptNamespace(""NewNamespace"")]
                public class MyClass {
                }
                public class MyOtherClass : MyClass {
                }
            ", "MyOtherClass", "NewNamespace.MyClass");
        }

        [TestMethod]
        public void InterfaceFullNames()
        {
            TestInterfaceFullNames(@"
                namespace MyNamespace1 {
                    [ScriptNamespace(""NewNamespace"")]
                    interface MyInterface1 {
                    }
                    [ScriptNamespace(""NewNamespace"")]
                    interface MyInterface2 {
                    }
                    [ScriptNamespace(""NewNamespace"")]
                    interface MyInterface3 {
                    }
                    class MyClass : MyInterface1, MyInterface2, MyInterface3 {
                    }
                }
            ", "MyClass", new[] { "NewNamespace.MyInterface1", "NewNamespace.MyInterface2", "NewNamespace.MyInterface3" });
        }

        [TestMethod]
        public void InterfaceFullNamesInRootCompilationUnit()
        {
            TestInterfaceFullNames(@"
                [ScriptNamespace(""NewNamespace"")]
                interface MyInterface1 {
                }
                [ScriptNamespace(""NewNamespace"")]
                interface MyInterface2 {
                }
                [ScriptNamespace(""NewNamespace"")]
                interface MyInterface3 {
                }
                class MyClass : MyInterface1, MyInterface2, MyInterface3 {
                }
            ", "MyClass", new[] { "NewNamespace.MyInterface1", "NewNamespace.MyInterface2", "NewNamespace.MyInterface3" });
        }

        private static void TestBaseClassFullName(string source, string className, string expectedBaseTypeName)
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

            // Generating the AST
            var factory = new ClassDeclarationSyntaxFactory(classDefinition, lookup);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ClassDeclarationSyntax), "Expected a class declaration node to be built");

            var classDeclarationSyntaxNode = syntaxNode as ClassDeclarationSyntax;
            var baseList = classDeclarationSyntaxNode.BaseList.Types;

            Assert.AreEqual(1, baseList.Count, "Expected one base class only");

            var baseType = classDeclarationSyntaxNode.BaseList.Types.First();
            var baseTypeIdentifier = baseType.Type as QualifiedNameSyntax;

            Assert.IsNotNull(baseTypeIdentifier, "QUalified name expected");

            var baseTypeName = baseTypeIdentifier.ToString();
            Assert.AreEqual(expectedBaseTypeName, baseTypeName, "Base type full name not correct");
        }

        private static void TestInterfaceFullNames(string source, string className, string[] expectedInterfaceFullNames)
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

            // Generating the AST
            var factory = new ClassDeclarationSyntaxFactory(classDefinition, lookup);
            var syntaxNode = factory.Create();

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(ClassDeclarationSyntax), "Expected a class declaration node to be built");

            var classDeclarationSyntaxNode = syntaxNode as ClassDeclarationSyntax;
            var baseList = classDeclarationSyntaxNode.BaseList.Types;

            Assert.AreEqual(expectedInterfaceFullNames.Length, baseList.Count, $"Expected {expectedInterfaceFullNames.Length} interfaces");

            Action<int, string> NameChecker = (index, expectedName) =>
            {
                var baseType = classDeclarationSyntaxNode.BaseList.Types.ElementAt(index);
                var baseTypeIdentifier = baseType.Type as QualifiedNameSyntax;

                Assert.IsNotNull(baseTypeIdentifier, "Qualified name expected");

                var baseTypeName = baseTypeIdentifier.ToString();
                Assert.AreEqual(expectedName, baseTypeName, "Base type full name not correct");
            };

            for (int i = 0; i < expectedInterfaceFullNames.Length; i++)
            {
                NameChecker(i, expectedInterfaceFullNames[i]);
            }
        }
    }
}
