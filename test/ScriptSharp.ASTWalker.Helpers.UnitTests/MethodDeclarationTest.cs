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

        [TestMethod]
        public void NameConvertedAccordingToScriptSharpRules()
        {
            TestName(@"
            public class MyClass {
                public void MyMethod() { }
            }
            ", "myMethod");
        }

        [TestMethod]
        public void WhenPreserveNameAttributeDetectedThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [PreserveName]
                public void MyMethod() { }
            }
            ", "MyMethod");
        }

        [TestMethod]
        public void WhenScriptNameAttributeDetectedOnMethodThenRenderNameAsSpecified()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(""mymethod"")]
                public void MyMethod() { }
            }
            ", "mymethod");
        }

        [TestMethod]
        public void WhenScriptNameWithPreserveNameAttributeDetectedOnMethodThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(PreserveCase = true)]
                public void MYMETHOD() { }
            }
            ", "MYMETHOD");
        }

        [TestMethod]
        public void WhenScriptNameWithPreserveCaseAttributeDetectedOnMethodThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(PreserveCase = true)]
                public void MyMethod() { }
            }
            ", "MyMethod");
        }

        /// <summary>
        /// ScriptName attribute constructor argument overrides PreserveCase/PreserveName properties
        /// </summary>
        [TestMethod]
        public void WhenScriptNameWithMultipleAttributesDetectedOnMethodThenOverridenNameTakesPrecedence()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(""mymethod"", PreserveCase = true, PreserveName = true)]
                public void MyMethod() { }
            }
            ", "mymethod");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(MethodDeclarationSyntax));
            Assert.IsNotNull(node);

            var methodDeclarationNode = node as MethodDeclarationSyntax;
            Assert.IsNotNull(methodDeclarationNode);

            var helper = new MethodDeclaration(methodDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
