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
    /// Tests for the <see cref="FieldDeclaration"/> classes.
    /// </summary>
    [TestClass]
    public class FieldDeclarationTest
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
        public void NameConvertedAccordingToScriptSharpRules()
        {
            TestName(@"
            public class MyClass {
                int MyField;
            }
            ", "myField");
        }

        /// <summary>
        /// ScrptSharp by default converts identifier UILanguageId to uiLanguageId
        /// </summary>
        [TestMethod]
        public void NameConvertedAccordingToScriptSharpCamelCaseRules()
        {
            TestName(@"
            public class MyClass {
                int MYField;
            }
            ", "myField");
        }

        [TestMethod]
        public void WhenPreserveNameAttributeDetectedOnFieldThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [PreserveName]
                int MyField;
            }
            ", "MyField");
        }

        [TestMethod]
        public void WhenScriptNameAttributeDetectedOnFieldThenRenderNameAsSpecified()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(""myfield"")]
                int MyField;
            }
            ", "myfield");
        }

        [TestMethod]
        public void WhenScriptNameWithPreserveNameAttributeDetectedOnFieldThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(PreserveCase = true)]
                int MYFIELD;
            }
            ", "MYFIELD");
        }

        [TestMethod]
        public void WhenScriptNameWithPreserveCaseAttributeDetectedOnFieldThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(PreserveCase = true)]
                int MyField;
            }
            ", "MyField");
        }

        /// <summary>
        /// ScriptName attribute constructor argument overrides PreserveCase/PreserveName properties
        /// </summary>
        [TestMethod]
        public void WhenScriptNameWithMultipleAttributesDetectedOnFieldThenOverridenNameTakesPrecedence()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(""myfield"", PreserveCase = true, PreserveName = true)]
                int MyField;
            }
            ", "myfield");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(FieldDeclarationSyntax));
            Assert.IsNotNull(node);

            var fieldDeclarationNode = node as FieldDeclarationSyntax;
            Assert.IsNotNull(fieldDeclarationNode);

            var helper = new FieldDeclaration(fieldDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
