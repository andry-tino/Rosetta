/// <summary>
/// InheritableDeclarationTest.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.AST.Helpers.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.AST.Utilities;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for <see cref="InheritableDeclarationTest"/> class.
    /// </summary>
    /// <remarks>
    /// This test suite makes sure that we can distinguish between classes and interfaces in the different cases.
    /// - Odd interface: An interface whose name does not start with `I`.
    /// - Odd class: An interface whose name does start with `I`.
    /// </remarks>
    [TestClass]
    public class InheritableDeclarationTest
    {
        [TestMethod]
        public void ClassWithSemanticModelWithNormalBaseClass()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                class BaseType { }

                class Class1 : BaseType {
                }
            ", true, 1);

            Assert.AreEqual(Helpers.TypeKind.Class, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting a class!");
        }

        [TestMethod]
        public void ClassWithSemanticModelWithOddBaseClass()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                class IBaseType { }

                class Class1 : IBaseType {
                }
            ", true, 1);

            Assert.AreEqual(Helpers.TypeKind.Class, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting a class!");
        }

        [TestMethod]
        public void ClassWithSemanticModelWithNormalInterfaces()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                interface IInterface1 { }
                interface IInterface2 { }

                class Class1 : IInterface1, IInterface2 {
                }
            ", true, 2);
            
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting an interface!");
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(1).Kind, "Expecting an interface!");
        }

        [TestMethod]
        public void ClassWithSemanticModelWithOddInterfaces()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                interface OddInterface1 { }
                interface OddInterface2 { }

                class Class1 : OddInterface1, OddInterface2 {
                }
            ", true, 2);

            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting an interface!");
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(1).Kind, "Expecting an interface!");
        }

        [TestMethod]
        public void ClassWithSemanticModelWithNormalAndOddBaseTypes()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                interface IInterface1 { }
                interface IInterface2 { }
                interface OddInterface1 { }
                interface OddInterface2 { }
                class Base1 { }

                class Class1 : Base1, IInterface1, IInterface2, OddInterface1, OddInterface2 {
                }
            ", true, 5);

            Assert.AreEqual(Helpers.TypeKind.Class, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting a class!");
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(1).Kind, "Expecting an interface!");
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(2).Kind, "Expecting an interface!");
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(3).Kind, "Expecting an interface!");
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(4).Kind, "Expecting an interface!");
        }

        [TestMethod]
        public void ClassWithoutSemanticModelWithNormalBaseClass()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                class BaseType { }

                class Class1 : BaseType {
                }
            ", false, 1);

            Assert.AreEqual(Helpers.TypeKind.Class, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting a class!");
        }

        [TestMethod]
        public void ClassWithoutSemanticModelWithOddBaseClass()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                class IBaseType { }

                class Class1 : IBaseType {
                }
            ", false, 1);

            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting (wrongly) an interface!");
        }

        [TestMethod]
        public void ClassWithoutSemanticModelWithNormalInterfaces()
        {
            var inheritableDeclaration = BuildInheritableDeclaration(@"
                interface IInterface1 { }
                interface IInterface2 { }

                class Class1 : IInterface1, IInterface2 {
                }
            ", false, 2);

            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(0).Kind, "Expecting an interface!");
            Assert.AreEqual(Helpers.TypeKind.Interface, inheritableDeclaration.BaseTypes.ElementAt(1).Kind, "Expecting an interface!");
        }

        [TestMethod]
        [ExpectedException(typeof(SemanticUtilities.UnableToDiscriminateBasetypeCollectionByNameException))]
        public void ClassWithoutSemanticModelWithOddInterfaces()
        {
            BuildInheritableDeclaration(@"
                interface OddInterface1 { }
                interface OddInterface2 { }

                class Class1 : OddInterface1, OddInterface2 {
                }
            ", false, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(SemanticUtilities.UnableToDiscriminateBasetypeCollectionByNameException))]
        public void ClassWithoutSemanticModelWithNormalAndOddBaseTypes()
        {
            BuildInheritableDeclaration(@"
                interface IInterface1 { }
                interface IInterface2 { }
                interface OddInterface1 { }
                interface OddInterface2 { }
                class Base1 { }

                class Class1 : Base1, IInterface1, IInterface2, OddInterface1, OddInterface2 {
                }
            ", false, 5);
        }

        private static InheritableDeclaration BuildInheritableDeclaration(string source, bool withSemanticModel, int expectedBaseTypesNumber)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var semanticModel = withSemanticModel 
                ? CSharpCompilation.Create("TestFile").AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                        .AddSyntaxTrees(syntaxTree).GetSemanticModel(syntaxTree)
                : null;

            SyntaxNode node = new NodeLocator(syntaxTree).LocateLast(typeof(ClassDeclarationSyntax));

            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                    typeof(ClassDeclarationSyntax).Name));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            InheritableDeclaration inheritableDeclaration = new ClassDeclaration(classDeclarationNode, semanticModel);
            Assert.IsNotNull(inheritableDeclaration.BaseTypes, "Expecting list of base types not to be null!");
            Assert.IsTrue(inheritableDeclaration.BaseTypes.Count() > 0, "Expecting base types!");
            Assert.AreEqual(expectedBaseTypesNumber, inheritableDeclaration.BaseTypes.Count(), "Expected number of base types not matched!");

            return inheritableDeclaration;
        }
    }
}
