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
    /// Tests for the <see cref="EventFieldDeclarationTest"/> classes.
    /// </summary>
    [TestClass]
    public class EventFieldDeclarationTest
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
                event Action MyEvent;
            }
            ", "myEvent");
        }
        
        [TestMethod]
        public void WhenPreserveNameAttributeDetectedOnFieldThenRenderNameAsItIs()
        {
            TestName(@"
            public class MyClass {
                [PreserveName]
                event Action MyEvent;
            }
            ", "MyEvent");
        }

        [TestMethod]
        public void WhenScriptNameAttributeDetectedOnFieldThenRenderNameAsSpecified()
        {
            TestName(@"
            public class MyClass {
                [ScriptName(""myevent"")]
                event Action MyEvent;
            }
            ", "myevent");
        }

        private static void TestName(string source, string expectedName)
        {
            var tree = CSharpSyntaxTree.ParseText(source);

            var node = new NodeLocator(tree).LocateFirst(typeof(EventFieldDeclarationSyntax));
            Assert.IsNotNull(node);

            var eventFieldDeclarationNode = node as EventFieldDeclarationSyntax;
            Assert.IsNotNull(eventFieldDeclarationNode);

            var helper = new EventFieldDeclaration(eventFieldDeclarationNode);

            Assert.AreEqual(expectedName, helper.Name, "Name was not converted according to ScriptSharp rules!");
        }
    }
}
