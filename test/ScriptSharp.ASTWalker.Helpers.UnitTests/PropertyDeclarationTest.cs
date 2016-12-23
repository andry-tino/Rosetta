/// <summary>
/// PropertyDeclarationTest.cs
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
    /// Tests for the <see cref="PropertyDeclaration"/> classes.
    /// </summary>
    [TestClass]
    public class PropertyDeclarationTest
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
                public string MyProperty { 
                    get { return null; }
                    set { }
                }
            }
            ", "myProperty");
        }

        [TestMethod]
        public void NameOnAutoPropertyConvertedAccordingToScriptSharpRules()
        {
            TestName(@"
            public class MyClass {
                public string MyProperty { get; set; }
            }
            ", "myProperty");
        }

        [TestMethod]
        public void WhenPreserveNameAttributeDetectedThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [PreserveName]
                public string MyProperty { 
                    get { return null; }
                    set { }
                }
            }
            ", "MyProperty");
        }

        [TestMethod]
        public void WhenPreserveNameAttributeDetectedOnAutoPropertyThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [PreserveName]
                public string MyProperty { get; set; }
            }
            ", "MyProperty");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(PropertyDeclarationSyntax));
            Assert.IsNotNull(node);

            var propertyDeclarationNode = node as PropertyDeclarationSyntax;
            Assert.IsNotNull(propertyDeclarationNode);

            var helper = new PropertyDeclaration(propertyDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
