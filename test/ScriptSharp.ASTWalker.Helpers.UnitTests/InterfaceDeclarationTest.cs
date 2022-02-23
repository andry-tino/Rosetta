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
    /// Tests for the <see cref="InterfaceDeclaration"/> classes.
    /// </summary>
    [TestClass]
    public class InterfaceDeclarationTest
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
        /// Interface names do not get lowercased first chars
        /// </summary>
        [TestMethod]
        public void NameConvertedOnInterfaceAsIs() => TestName(@"public interface IMyInterface {}", "IMyInterface");

        [TestMethod]
        public void WhenInterfaceScriptNameAttributeDetectedOnInterfaceThenRenderNameAsSpecified()
        {
            TestName(@"
            [ScriptName(""imyinterface"")]
            public interface IMyInterface {}
            ", "imyinterface");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(InterfaceDeclarationSyntax));
            Assert.IsNotNull(node);

            var interfaceDeclarationNode = node as InterfaceDeclarationSyntax;
            Assert.IsNotNull(interfaceDeclarationNode);

            var helper = new InterfaceDeclaration(interfaceDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
