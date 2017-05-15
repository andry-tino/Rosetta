/// <summary>
/// ExpressionTranslationUnitBuilderTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories.UnitTests
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
    using Rosetta.Translation.Mocks;

    /// <summary>
    /// Tests for <see cref="ExpressionTranslationUnitBuilder"/> class.
    /// </summary>
    [TestClass]
    public class ExpressionTranslationUnitBuilderTest
    {
        // TODO: This is testing a class which is currently misplaced among helpers, however in order to be proactive in this, the test (this class) is in the correct place.

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        [TestMethod]
        public void NumericLiteralExpression()
        {
            Test("0");
        }

        [TestMethod]
        public void StringLiteralExpression()
        {
            Test("'my string'", @"""my string""");
        }

        [TestMethod]
        public void NullLiteralExpression()
        {
            Test("null");
        }

        [TestMethod]
        public void BooleanLiteralExpression()
        {
            Test("false");
            Test("true");
        }

        private static void Test(string expression)
        {
            Test(expression, expression);
        }

        private static void Test(string expectedExpression, string expression)
        {
            // Create expression node
            ExpressionSyntax expressionNode = SyntaxFactory.ParseExpression(expression);

            // Generate
            ITranslationUnit translationUnit = new ExpressionTranslationUnitBuilder(expressionNode).Build();

            // Asserting
            Assert.AreEqual(expectedExpression, translationUnit.Translate(), 
                $"Expression '{expression}' does not match with expected '{expectedExpression}'");
        }
    }
}
