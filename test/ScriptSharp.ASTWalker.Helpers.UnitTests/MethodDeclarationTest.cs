/// <summary>
/// MethodDeclarationTest.cs
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
    /// Tests for the <see cref="MethodDeclaration"/> classes.
    /// </summary>
    [TestClass]
    public class MethodDeclarationTest
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
        /// Tests that we render the name and full name when not specifying the semantic model and when doing so.
        /// </summary>
        [TestMethod]
        public void NameConvertedAccordingToScriptSharpRules()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
            public class MyClass {
                public void MyMethod() { }
            }
            ");

            var node = new NodeLocator(tree).LocateFirst(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node);

            var methodDeclarationNode = node as MethodDeclarationSyntax;
            Assert.IsNotNull(methodDeclarationNode);

            var helper = new MethodDeclaration(methodDeclarationNode);

            Assert.AreEqual("myMethod", helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
