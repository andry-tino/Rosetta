/// <summary>
/// TypesWithScriptNamespaceAttribute.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Tests.Utils;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TypesWithScriptNamespaceAttribute
    {
        [TestMethod]
        public void ClassWithScriptNamespaceAttribute()
        {
            var node = (@"
                namespace Root.MyNamespace1 {
                    [ScriptNamespace(""NewNamespace"")]
                    public class MyClass {
                    }
                }
            ").ExtractASTRoot();

            var namespaces = new NodeLocator(node).LocateAll(typeof(NamespaceDeclarationSyntax), true);
            Assert.AreEqual(1, namespaces.Count(), "Expecting only one namespace at root level");

            var @namespace = namespaces.First() as NamespaceDeclarationSyntax;
            var namespaceName = @namespace.Name.ToString();
            Assert.AreEqual("NewNamespace", namespaceName, "Wrong namespace name");

            var classes = new NodeLocator(@namespace).LocateAll(typeof(ClassDeclarationSyntax), true);
            Assert.AreEqual(1, classes.Count(), "Expecting only one class in namespace");

            var @class = classes.First() as ClassDeclarationSyntax;
            var className = @class.Identifier.Text;
            Assert.AreEqual("MyClass", className, "Wrong class name");
        }
    }
}
