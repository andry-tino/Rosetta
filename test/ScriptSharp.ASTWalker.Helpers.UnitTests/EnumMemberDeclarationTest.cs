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
    /// Tests for the <see cref="EnumMemberDeclarationTest"/> classes.
    /// </summary>
    [TestClass]
    public class EnumMemberDeclarationTest
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
        public void NameConvertedOnEnumMemberAccordingToScriptSharpRules()
        {
            TestName(@"
            public enum MyEnum {
                MyEnumMember;
            }
            ", "myEnumMember");
        }

        /// <summary>
        /// ScrptSharp by default converts identifier UILanguageId to uiLanguageId
        /// </summary>
        [TestMethod]
        public void NameConvertedOnEnumMemberAccordingToScriptSharpCamelCaseRules()
        {
            TestName(@"
            public enum MyEnum {
                MYEnumMember;
            }
            ", "myEnumMember");
        }

        [TestMethod]
        public void WhenScriptNameAttributeDetectedOnEnumMemberThenRenderNameAsSpecified()
        {
            TestName(@"
            public enum MyEnum {
                [ScriptName(""myenummember"")
                MyEnumMember;
            }
            ", "myenummember");
        }

        [TestMethod]
        public void WhenScriptNameWithPreserveCaseAttributeDetectedOnEnumMemberThenRenderNameAsItIs()
        {
            TestName(@"
            public enum MyEnum {
                [ScriptName(PreserveCase = true)]
                MyEnumMember;
            }
            ", "MyEnumMember");
        }

        /// <summary>
        /// ScriptName attribute constructor argument overrides PreserveCase/PreserveName properties
        /// </summary>
        [TestMethod]
        public void WhenScriptNameWithMultipleAttributesDetectedOnEnumMemberThenOverridenNameTakesPrecedence()
        {
            TestName(@"
            public enum MyEnum {
                [ScriptName(""myenummember"", PreserveCase = true, PreserveName = true)]
                MyEnumMember;
            }
            ", "myenummember");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(EnumMemberDeclarationSyntax));
            Assert.IsNotNull(node);

            var enumMemberDeclarationNode = node as EnumMemberDeclarationSyntax;
            Assert.IsNotNull(enumMemberDeclarationNode);

            var helper = new EnumMemberDeclaration(enumMemberDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
