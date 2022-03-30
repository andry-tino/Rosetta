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
    /// Tests for the <see cref="EnumDeclarationTest"/> classes.
    /// </summary>
    [TestClass]
    public class EnumDeclarationTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        [TestMethod]
        public void NameConvertedOnEnumAccordingToScriptSharpRules()
        {
            TestName(@"
            public enum MyEnum {
                MyEnumMember;
            }
            ", "MyEnum");
        }

        [TestMethod]
        public void WhenScriptNameAttributeDetectedOnEnumThenRenderNameAsSpecified()
        {
            TestName(@"
            [ScriptName(""myenum"")]
            public enum MyEnum {
                MyEnumMember;
            }
            ", "myenum");
        }

        /// <summary>
        /// ScriptName attribute constructor argument overrides PreserveCase/PreserveName properties
        /// </summary>
        [TestMethod]
        public void WhenScriptNameWithMultipleAttributesDetectedOnFieldThenOverridenNameTakesPrecedence()
        {
            TestName(@"
            [ScriptName(""myenum"", PreserveCase = true, PreserveName = true)]
            public enum MyEnum {
                MyEnumMember;
            }
            ", "myenum");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(EnumDeclarationSyntax));
            Assert.IsNotNull(node);

            var enumDeclarationNode = node as EnumDeclarationSyntax;
            Assert.IsNotNull(enumDeclarationNode);

            var helper = new EnumDeclaration(enumDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
