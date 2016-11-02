/// <summary>
/// MultiPurposeASTWalkerTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.AST.Utilities;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Tests for <see cref="MultiPurposeASTWalker"/> class.
    /// </summary>
    [TestClass]
    public class MultiPurposeASTWalkerTest
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
        public void WalkASTDeep()
        {
            string source = @"
                namespace MyNamespaceA {
                    public class MyClassA { }
                    public class MyClassB { }

                    namespace MyNamespaceB {
                        public class MyClassC { }
                        public class MyClassD { }
                    }
                }
            ";

            int traversedClassesCount = 0;

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            var node = new NodeLocator(tree).LocateLast(typeof(CompilationUnitSyntax)) as CSharpSyntaxNode;

            // Calling the walker
            var astWalker = new MultiPurposeASTWalker(node, 
                syntaxNode => syntaxNode as ClassDeclarationSyntax != null, 
                delegate 
                {
                    traversedClassesCount++;
                }, 
                false);
            astWalker.Start();

            // Checking
            Assert.AreEqual(4, traversedClassesCount, "Expected walker to deep traverse AST!");
        }

        // TODO: Improve this test
        [TestMethod]
        public void WalkASTOnlyRootLevel()
        {
            string source = @"
                namespace MyNamespaceA {
                    public class MyClassA { }
                    public class MyClassB { }

                    namespace MyNamespaceB {
                        public class MyClassC { }
                        public class MyClassD { }
                    }
                }
                namespace MyNamespaceC { }
            ";

            int traversedClassesCount = 0;
            int traversedNamespacesCount = 0;

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            var node = new NodeLocator(tree).LocateLast(typeof(CompilationUnitSyntax)) as CSharpSyntaxNode;

            // Calling the walker
            var astClassWalker = new MultiPurposeASTWalker(node,
                syntaxNode => syntaxNode as ClassDeclarationSyntax != null,
                delegate
                {
                    traversedClassesCount++;
                },
                true);
            astClassWalker.Start();

            //var astNamespaceWalker = new MultiPurposeASTWalker(node,
            //    syntaxNode => syntaxNode as NamespaceDeclarationSyntax != null,
            //    delegate
            //    {
            //        traversedNamespacesCount++;
            //    },
            //    true);
            //astNamespaceWalker.Start();

            // Checking
            Assert.AreEqual(0, traversedClassesCount, "Expected walker to root level traverse AST!");
            //Assert.AreEqual(2, traversedNamespacesCount, "Expected walker to root level traverse AST!");
        }
    }
}
