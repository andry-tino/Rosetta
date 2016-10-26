/// <summary>
/// Visibility.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.AST;
    using Rosetta.ScriptSharp.Definition.AST.UnitTests.Mocks;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for all walkers, checking that non exposed components are not actually emitted.
    /// </summary>
    [TestClass]
    public class VisibilityTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        /// <summary>
        /// Checks that not exposed field members in classes are not actually emitted in the definition file.
        /// </summary>
        [TestMethod]
        public void PublicFieldMembersInCLasses()
        {
            var astWalker = ParseClass(@"
                public class MyClass {
                    private int myPrivateField1;
                    public string myPublicField1;
                    private int myPrivateField2;
                    protected int myProtectedField1;
                    string myPrivateField3;
                }
            ");

            // Checking
            Assert.IsNotNull(astWalker.ClassDefinition);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDefinition.MemberDeclarations);
            Assert.IsTrue(astWalker.ClassDefinition.MemberDeclarations.Count() > 0);
            Assert.AreEqual(1, astWalker.ClassDefinition.MemberDeclarations.Count());
        }

        /// <summary>
        /// Checks that not exposed field members in classes are not actually emitted in the definition file.
        /// </summary>
        [TestMethod]
        public void InternalFieldMembersInCLasses()
        {
            var astWalker = ParseClass(@"
                public class MyClass {
                    private int myPrivateField1;
                    internal string myInternalField1;
                    private int myPrivateField2;
                    protected int myProtectedField1;
                    string myPrivateField3;
                }
            ");

            // Checking
            Assert.IsNotNull(astWalker.ClassDefinition);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDefinition.MemberDeclarations);
            Assert.IsTrue(astWalker.ClassDefinition.MemberDeclarations.Count() > 0);
            Assert.AreEqual(1, astWalker.ClassDefinition.MemberDeclarations.Count());
        }

        /// <summary>
        /// Checks that not exposed properties in classes are not actually emitted in the definition file.
        /// </summary>
        [TestMethod]
        public void PublicPropertiesInCLasses()
        {
            var astWalker = ParseClass(@"
                public class MyClass {
                    private int myPrivateProperty1 { get { return 0; } set { } }
                    public int myPublicProperty1 { get { return 0; } set { } }
                    private int myPrivateField2 { get { return 0; } set { } }
                    protected int myProtectedField1 { get { return 0; } set { } }
                    string myPrivateField3 { get { return 0; } set { } }
                }
            ");

            // Checking
            Assert.IsNotNull(astWalker.ClassDefinition);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDefinition.PropertyDeclarations);
            Assert.IsTrue(astWalker.ClassDefinition.PropertyDeclarations.Count() > 0);
            Assert.AreEqual(1, astWalker.ClassDefinition.PropertyDeclarations.Count());
        }

        /// <summary>
        /// Checks that not exposed properties in classes are not actually emitted in the definition file.
        /// </summary>
        [TestMethod]
        public void InternalPropertiesInCLasses()
        {
            var astWalker = ParseClass(@"
                public class MyClass {
                    private int myPrivateProperty1 { get { return 0; } set { } }
                    internal int myInternalProperty1 { get { return 0; } set { } }
                    private int myPrivateField2 { get { return 0; } set { } }
                    protected int myProtectedField1 { get { return 0; } set { } }
                    string myPrivateField3 { get { return 0; } set { } }
                }
            ");

            // Checking
            Assert.IsNotNull(astWalker.ClassDefinition);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDefinition.PropertyDeclarations);
            Assert.IsTrue(astWalker.ClassDefinition.PropertyDeclarations.Count() > 0);
            Assert.AreEqual(1, astWalker.ClassDefinition.PropertyDeclarations.Count());
        }

        /// <summary>
        /// Checks that not exposed methods in classes are not actually emitted in the definition file.
        /// </summary>
        [TestMethod]
        public void PublicMethodsInCLasses()
        {
            var astWalker = ParseClass(@"
                public class MyClass {
                    private void myPrivateMethod1() {}
                    public void myPublicMethod1() {}
                    private vois myPrivateMethod2() {}
                    protected void myProtectedMethod() {}
                    void myPrivateMethod3() {}
                }
            ");

            // Checking
            Assert.IsNotNull(astWalker.ClassDefinition);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDefinition.MethodDeclarations);
            Assert.IsTrue(astWalker.ClassDefinition.MethodDeclarations.Count() > 0);
            Assert.AreEqual(1, astWalker.ClassDefinition.MethodDeclarations.Count());
        }

        /// <summary>
        /// Checks that not exposed methods in classes are not actually emitted in the definition file.
        /// </summary>
        [TestMethod]
        public void InternalMethodsInCLasses()
        {
            var astWalker = ParseClass(@"
                public class MyClass {
                    private void myPrivateMethod1() {}
                    internal void myInternalMethod1() {}
                    private vois myPrivateMethod2() {}
                    protected void myProtectedMethod() {}
                    void myPrivateMethod3() {}
                }
            ");

            // Checking
            Assert.IsNotNull(astWalker.ClassDefinition);

            // Checking members
            Assert.IsNotNull(astWalker.ClassDefinition.MethodDeclarations);
            Assert.IsTrue(astWalker.ClassDefinition.MethodDeclarations.Count() > 0);
            Assert.AreEqual(1, astWalker.ClassDefinition.MethodDeclarations.Count());
        }

        private static MockedClassDefinitionASTWalker ParseClass(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(ClassDeclarationSyntax));
            var classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = MockedClassDefinitionASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            astWalker.Walk();

            return astWalker;
        }
    }
}
