/// <summary>
/// StatementTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;
    using Rosetta.Tests.Data;
    using Rosetta.Tests.Utils;
    using Rosetta.AST.Mocks;

    /// <summary>
    /// Tests for <see cref="ConditionalStatementASTWalker"/> class.
    /// </summary>
    public partial class StatementTest
    {
        [TestMethod]
        public void EmptyIfStatementWithBlock()
        {
            string source = @"
                public class Class1 {
                    public void Method1() {
                        if (true) {
                        }
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(IfStatementSyntax));
            IfStatementSyntax ifStatementNode = node as IfStatementSyntax;

            // Creating the walker
            var astWalker = MockedConditionalStatementASTWalker.Create(ifStatementNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.Statement);

            // Checking members
            Assert.IsNotNull(astWalker.Statement.Bodies);
            Assert.IsTrue(astWalker.Statement.Bodies.Count() > 0);
            Assert.AreEqual(1, astWalker.Statement.Bodies.Count());

            Assert.IsInstanceOfType(astWalker.Statement.Bodies.ElementAt(0), typeof(StatementsGroupTranslationUnit));

            Assert.IsNotNull(astWalker.Statement.TestExpressions);
            Assert.IsTrue(astWalker.Statement.TestExpressions.Count() > 0);
            Assert.AreEqual(1, astWalker.Statement.TestExpressions.Count());

            Assert.IsInstanceOfType(astWalker.Statement.TestExpressions.ElementAt(0), typeof(LiteralTranslationUnit<bool>));

            Assert.IsNull(astWalker.Statement.LastBody);
        }

        [TestMethod]
        public void Empty2IfsStatementsWithBlock()
        {
            string source = @"
                public class Class1 {
                    public void Method1() {
                        if (true) {
                        } else if (false) {
                        }
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(IfStatementSyntax));
            IfStatementSyntax ifStatementNode = node as IfStatementSyntax;

            // Creating the walker
            var astWalker = MockedConditionalStatementASTWalker.Create(ifStatementNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.Statement);

            // Checking members
            Assert.IsNotNull(astWalker.Statement.Bodies);
            Assert.IsTrue(astWalker.Statement.Bodies.Count() > 0);
            Assert.AreEqual(2, astWalker.Statement.Bodies.Count());

            Assert.IsInstanceOfType(astWalker.Statement.Bodies.ElementAt(0), typeof(StatementsGroupTranslationUnit));
            Assert.IsInstanceOfType(astWalker.Statement.Bodies.ElementAt(1), typeof(StatementsGroupTranslationUnit));

            Assert.IsNotNull(astWalker.Statement.TestExpressions);
            Assert.IsTrue(astWalker.Statement.TestExpressions.Count() > 0);
            Assert.AreEqual(2, astWalker.Statement.TestExpressions.Count());

            Assert.IsInstanceOfType(astWalker.Statement.TestExpressions.ElementAt(0), typeof(LiteralTranslationUnit<bool>));
            Assert.IsInstanceOfType(astWalker.Statement.TestExpressions.ElementAt(1), typeof(LiteralTranslationUnit<bool>));

            Assert.IsNull(astWalker.Statement.LastBody);
        }

        [TestMethod]
        public void EmptyIfElseStatementWithBlocks()
        {
            string source = @"
                public class Class1 {
                    public void Method1() {
                        if (true) {
                        } else {
                        }
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(IfStatementSyntax));
            IfStatementSyntax ifStatementNode = node as IfStatementSyntax;

            // Creating the walker
            var astWalker = MockedConditionalStatementASTWalker.Create(ifStatementNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.Statement);

            // Checking members
            Assert.IsNotNull(astWalker.Statement.Bodies);
            Assert.IsTrue(astWalker.Statement.Bodies.Count() > 0);
            Assert.AreEqual(1, astWalker.Statement.Bodies.Count());

            Assert.IsInstanceOfType(astWalker.Statement.Bodies.ElementAt(0), typeof(StatementsGroupTranslationUnit));

            Assert.IsNotNull(astWalker.Statement.TestExpressions);
            Assert.IsTrue(astWalker.Statement.TestExpressions.Count() > 0);
            Assert.AreEqual(1, astWalker.Statement.TestExpressions.Count());

            Assert.IsInstanceOfType(astWalker.Statement.TestExpressions.ElementAt(0), typeof(LiteralTranslationUnit<bool>));

            Assert.IsNotNull(astWalker.Statement.LastBody);

            Assert.IsInstanceOfType(astWalker.Statement.LastBody, typeof(StatementsGroupTranslationUnit));
        }

        [TestMethod]
        public void Empty2IfsElseStatementWithBlocks()
        {
            string source = @"
                public class Class1 {
                    public void Method1() {
                        if (true) {
                        } else if (false) {
                        } else {
                        }
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(IfStatementSyntax));
            IfStatementSyntax ifStatementNode = node as IfStatementSyntax;

            // Creating the walker
            var astWalker = MockedConditionalStatementASTWalker.Create(ifStatementNode);

            // Getting the translation unit
            astWalker.Walk();

            // Checking
            Assert.IsNotNull(astWalker.Statement);

            // Checking members
            Assert.IsNotNull(astWalker.Statement.Bodies);
            Assert.IsTrue(astWalker.Statement.Bodies.Count() > 0);
            Assert.AreEqual(2, astWalker.Statement.Bodies.Count());

            Assert.IsInstanceOfType(astWalker.Statement.Bodies.ElementAt(0), typeof(StatementsGroupTranslationUnit));
            Assert.IsInstanceOfType(astWalker.Statement.Bodies.ElementAt(1), typeof(StatementsGroupTranslationUnit));

            Assert.IsNotNull(astWalker.Statement.TestExpressions);
            Assert.IsTrue(astWalker.Statement.TestExpressions.Count() > 0);
            Assert.AreEqual(2, astWalker.Statement.TestExpressions.Count());

            Assert.IsInstanceOfType(astWalker.Statement.TestExpressions.ElementAt(0), typeof(LiteralTranslationUnit<bool>));
            Assert.IsInstanceOfType(astWalker.Statement.TestExpressions.ElementAt(1), typeof(LiteralTranslationUnit<bool>));

            Assert.IsNotNull(astWalker.Statement.LastBody);

            Assert.IsInstanceOfType(astWalker.Statement.LastBody, typeof(StatementsGroupTranslationUnit));
        }
    }
}
