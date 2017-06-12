/// <summary>
/// InterfaceDeclarationSyntaxFactoryTest.cs
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
    public class InterfaceDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void ExtendedInterfacesCorrectlyAcquired()
        {
            TestExtendedInterfacesNames(@"
                namespace MyNamespace1 {
                    [ScriptNamespace(""NewNamespace"")]
                    interface MyExtendedInterface1 {
                    }
                    [ScriptNamespace(""NewNamespace"")]
                    interface MyExtendedInterface2 {
                    }
                    [ScriptNamespace(""NewNamespace"")]
                    interface MyExtendedInterface3 {
                    }
                    interface MyInterface : MyExtendedInterface1, MyExtendedInterface2, MyExtendedInterface3 {
                    }
                }
            ", "MyInterface", new[] { "NewNamespace.MyExtendedInterface1", "NewNamespace.MyExtendedInterface2", "NewNamespace.MyExtendedInterface3" });
        }

        [TestMethod]
        public void ExtendedInterfacesCorrectlyAcquiredInRootCompilationUnit()
        {
            TestExtendedInterfacesNames(@"
                [ScriptNamespace(""NewNamespace"")]
                interface MyExtendedInterface1 {
                }
                [ScriptNamespace(""NewNamespace"")]
                interface MyExtendedInterface2 {
                }
                [ScriptNamespace(""NewNamespace"")]
                interface MyExtendedInterface3 {
                }
                interface MyInterface : MyExtendedInterface1, MyExtendedInterface2, MyExtendedInterface3 {
                }
            ", "MyInterface", new[] { "NewNamespace.MyExtendedInterface1", "NewNamespace.MyExtendedInterface2", "NewNamespace.MyExtendedInterface3" });
        }

        private static void TestExtendedInterfacesNames(string source, string interfaceName, string[] extendedInterfacesFullNames)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new ScriptSharpReflectionUtils.AsmlDasmlAssemblyLoader(source, 
                ScriptNamespaceAttributeHelper.AttributeSourceCode);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();
            ITypeLookup lookup = new LinearSearchTypeLookup(assembly);

            // Locating the class
            ITypeInfoProxy interfaceDefinition = assembly.LocateType(interfaceName);
            Assert.IsNotNull(interfaceDefinition);

            // Generating the AST
            var factory = new InterfaceDeclarationSyntaxFactory(interfaceDefinition, lookup);
            var syntaxNode = factory.Create() as InterfaceDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "An interface declaration node was expected to be built");

            var baseList = syntaxNode.BaseList.Types;
            Assert.AreEqual(extendedInterfacesFullNames.Length, baseList.Count, $"Expected {extendedInterfacesFullNames.Length} extended interfaces");

            Action<int, string> ExtendedInterfaceChecker = (index, expectedName) =>
            {
                var baseType = baseList.ElementAt(index);
                var baseTypeIdentifier = baseType.Type as QualifiedNameSyntax;

                Assert.IsNotNull(baseTypeIdentifier, "Qualified name expected");

                var baseTypeName = baseTypeIdentifier.ToString();
                Assert.AreEqual(expectedName, baseTypeName, "Base type full name not correct");
            };

            for (int i = 0; i < extendedInterfacesFullNames.Length; i++)
            {
                ExtendedInterfaceChecker(i, extendedInterfacesFullNames[i]);
            }
        }
    }
}
