/// <summary>
/// ExpressionUnitTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;
    using Rosetta.Translation.UnitTests.Data;
    using Utils = Rosetta.Tests.Utils;

    [TestClass]
    public class ExpressionUnitTest
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
        public void IntegerAddition()
        {
            ITranslationUnit number1 = Expressions.RandomIntegerLiteral;
            ITranslationUnit number2 = Expressions.RandomIntegerLiteral;

            ITranslationUnit translationUnit = BinaryExpressionTranslationUnit.Create(number1, number2, OperatorToken.Addition);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Code", nameof(this.IntegerAddition)),
                Utils.FileType.TypeScript);

            Assert.AreEqual(string.Format("{0} + {1}", number1.Translate(), number2.Translate()), 
                typescript, "Expression does not match expected!");
        }

        [TestMethod]
        public void IntegerMultiply()
        {
            ITranslationUnit number1 = Expressions.RandomIntegerLiteral;
            ITranslationUnit number2 = Expressions.RandomIntegerLiteral;

            ITranslationUnit translationUnit = BinaryExpressionTranslationUnit.Create(number1, number2, OperatorToken.Multiplication);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Code", nameof(this.IntegerMultiply)),
                Utils.FileType.TypeScript);

            Assert.AreEqual(string.Format("{0} * {1}", number1.Translate(), number2.Translate()),
                typescript, "Expression does not match expected!");
        }

        [TestMethod]
        public void IntegerSubtract()
        {
            ITranslationUnit number1 = Expressions.RandomIntegerLiteral;
            ITranslationUnit number2 = Expressions.RandomIntegerLiteral;

            ITranslationUnit translationUnit = BinaryExpressionTranslationUnit.Create(number1, number2, OperatorToken.Subtraction);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Code", nameof(this.IntegerSubtract)),
                Utils.FileType.TypeScript);

            Assert.AreEqual(string.Format("{0} - {1}", number1.Translate(), number2.Translate()),
                typescript, "Expression does not match expected!");
        }

        [TestMethod]
        public void IntegerDivide()
        {
            ITranslationUnit number1 = Expressions.RandomIntegerLiteral;
            ITranslationUnit number2 = Expressions.RandomIntegerLiteral;

            ITranslationUnit translationUnit = BinaryExpressionTranslationUnit.Create(number1, number2, OperatorToken.Divide);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Code", nameof(this.IntegerDivide)),
                Utils.FileType.TypeScript);

            Assert.AreEqual(string.Format("{0} / {1}", number1.Translate(), number2.Translate()),
                typescript, "Expression does not match expected!");
        }

        [TestMethod]
        public void Parenthesized()
        {
            ITranslationUnit number = Expressions.RandomIntegerLiteral;

            ITranslationUnit translationUnit = ParenthesizedExpressionTranslationUnit.Create(number);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Code", nameof(this.Parenthesized)),
                Utils.FileType.TypeScript);

            Assert.AreEqual(string.Format("({0})", number.Translate()),
                typescript, "Expression does not match expected!");
        }
    }
}
