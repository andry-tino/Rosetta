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
    using Rosetta.AST.UnitTests.Mocks;

    /// <summary>
    /// Tests for <see cref="ConditionalStatementASTWalker"/> class.
    /// </summary>
    public partial class StatementTest
    {
        [TestMethod]
        public void EmptyIfStatement()
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
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(IfStatementSyntax));
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
    }
}
