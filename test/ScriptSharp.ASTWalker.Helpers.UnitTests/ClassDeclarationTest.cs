/// <summary>
/// FieldDeclarationTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.ScriptSharp.AST.Helpers;
    using Rosetta.Tests.ScriptSharp.Utils;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for the <see cref="ClassDeclaration"/> classes.
    /// </summary>
    [TestClass]
    public class ClassDeclarationTest
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
        /// Class names do not get lowercased first chars
        /// </summary>
        [TestMethod]
        public void NameConvertedOnClassAsIs()
        {
            TestName(@"
            public class MyClass {
                int MyField;
            }
            ", "MyClass");
        }

        [TestMethod]
        public void WhenScriptNameAttributeDetectedOnClassThenRenderNameAsSpecified()
        {
            TestName(@"
            [ScriptName(""myclass"")]
            public class MyClass {
                int MyField;
            }
            ", "myclass");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(ClassDeclarationSyntax));
            Assert.IsNotNull(node);

            var classDeclarationNode = node as ClassDeclarationSyntax;
            Assert.IsNotNull(classDeclarationNode);

            var helper = new ClassDeclaration(classDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
