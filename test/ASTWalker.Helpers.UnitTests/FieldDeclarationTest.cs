/// <summary>
/// FieldDeclarationTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Tests.Utils;
    using Rosetta.Translation;

    /// <summary>
    /// Tests for <see cref="FieldDeclaration"/> class.
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

        /// <summary>
        /// Tests that we can successfully retrieve the method parameters when there are no parameters.
        /// </summary>
        [TestMethod]
        public void DetectModifiers()
        {
            TestModifiers(@"
                public class Class1 {
                    public int Field1;
                }
            ", new[] { VisibilityToken.Public });

            TestModifiers(@"
                public class Class1 {
                    private int Field1;
                }
            ", new[] { VisibilityToken.Private });

            TestModifiers(@"
                public class Class1 {
                    protected int Field1;
                }
            ", new[] { VisibilityToken.Protected });

            TestModifiers(@"
                public class Class1 {
                    internal int Field1;
                }
            ", new[] { VisibilityToken.Internal });

            TestModifiers(@"
                public class Class1 {
                    static int Field1;
                }
            ", new[] { VisibilityToken.Static });

            TestModifiers(@"
                public class Class1 {
                    public static int Field1;
                }
            ", new[] { VisibilityToken.Public, VisibilityToken.Static });

            TestModifiers(@"
                public class Class1 {
                    private static int Field1;
                }
            ", new[] { VisibilityToken.Private, VisibilityToken.Static });

            TestModifiers(@"
                public class Class1 {
                    protected static int Field1;
                }
            ", new[] { VisibilityToken.Protected, VisibilityToken.Static });

            TestModifiers(@"
                public class Class1 {
                    internal static int Field1;
                }
            ", new[] { VisibilityToken.Internal, VisibilityToken.Static });
        }

        private static void TestModifiers(string source, VisibilityToken[] expectedModifiers)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);

            SyntaxNode node = new NodeLocator(syntaxTree).LocateFirst(typeof(FieldDeclarationSyntax));
            Assert.IsNotNull(node, string.Format("Node of type `{0}` should be found!",
                typeof(FieldDeclarationSyntax).Name));

            var fieldDeclarationNode = node as FieldDeclarationSyntax;
            Assert.IsNotNull(fieldDeclarationNode);

            var fieldDeclaration = new FieldDeclaration(fieldDeclarationNode);

            Assert.IsNotNull(fieldDeclaration.Visibility, "Expecting list of visibility tokens not to be null");
            foreach (var modifier in expectedModifiers)
            {
                Assert.IsTrue(fieldDeclaration.Visibility.HasFlag(modifier), $"Visibility modifier {modifier.ToString()} expected");
            }
        }
    }
}
