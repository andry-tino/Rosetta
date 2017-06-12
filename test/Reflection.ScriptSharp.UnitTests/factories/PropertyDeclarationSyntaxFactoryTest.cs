/// <summary>
/// PropertyDeclarationSyntaxFactoryTest.cs
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
    public class PropertyDeclarationSyntaxFactoryTest
    {
        [TestMethod]
        public void ReturnTypeCorrectlyAcquired()
        {
            TestReturnTypeCorrectlyAcquired(@"
                namespace MyNamespace {
                    [ScriptNamespace(""NewNamespace"")]
                    public class Type1 { }
                    public class MyClass {
                        public Type1 MyProperty {
                            get { return null; }
                            set { }
                        }
                    }
                }
            ", "MyClass", "MyProperty", "NewNamespace.Type1");
        }

        [TestMethod]
        public void ReturnTypeCorrectlyAcquiredInRootCompilationUnit()
        {
            TestReturnTypeCorrectlyAcquired(@"
                [ScriptNamespace(""NewNamespace"")]
                public class Type1 { }
                public class MyClass {
                    public Type1 MyProperty {
                        get { return null; }
                        set { }
                    }
                }
            ", "MyClass", "MyProperty", "NewNamespace.Type1");
        }

        private static void TestReturnTypeCorrectlyAcquired(string source, string className, string propertyName, string expectedTypeFullName)
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

            // Locating the property
            IPropertyInfoProxy propertyDeclaration = classDefinition.LocateProperty(propertyName);
            Assert.IsNotNull(propertyDeclaration);

            // Generating the AST
            var factory = new PropertyDeclarationSyntaxFactory(propertyDeclaration, lookup);
            var syntaxNode = factory.Create() as PropertyDeclarationSyntax;

            Assert.IsNotNull(syntaxNode, "A node was expected to be built");
            Assert.IsInstanceOfType(syntaxNode, typeof(PropertyDeclarationSyntax), "Expected a property declaration node to be built");

            var returnType = syntaxNode.Type;
            Assert.IsNotNull(returnType);

            var typeIdentifier = returnType as QualifiedNameSyntax;
            Assert.IsNotNull(typeIdentifier, "Type expected to be qualified name");
            Assert.AreEqual(expectedTypeFullName, typeIdentifier.ToString(), "Parameter name does not match");
        }
    }
}
