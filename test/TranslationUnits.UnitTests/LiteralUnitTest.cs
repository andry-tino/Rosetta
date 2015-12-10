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
    public class LiteralUnitTest
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
        public void IntegerLiteral()
        {
            int number = Expressions.RandomInteger;
            ITranslationUnit translationUnit = LiteralTranslationUnit<int>.Create(number);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Code", nameof(this.IntegerLiteral)),
                Utils.FileType.TypeScript);

            Assert.AreEqual(string.Format("{0}", number.ToString()), typescript, "Literal does not match expected!");
        }

        [TestMethod]
        public void StringLiteral()
        {
            string str = "this is a string";
            ITranslationUnit translationUnit = LiteralTranslationUnit<string>.Create(str);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Code", nameof(this.StringLiteral)),
                Utils.FileType.TypeScript);

            Assert.AreEqual(string.Format("{1}{0}{1}", str, Lexems.SingleQuote), 
                typescript, "Literal does not match expected!");
        }
    }
}
