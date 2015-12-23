/// <summary>
/// NestingLevelRenderingTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;
    using Rosetta.Translation.UnitTests.Data;
    using Utils = Rosetta.Tests.Utils;

    [TestClass]
    public class NestingLevelRenderingTest
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
        /// TODO: Add for the last line when last line new line space problem has been fixed.
        /// </summary>
        [TestMethod]
        public void OneLevel()
        {
            var translationUnit = new NestedCompoundTranslationUnit();

            string output = translationUnit.Translate();
            string[] lines = GetAllLinesInString(output);

            // Line 1
            Assert.AreNotEqual(" ", lines[0][0].ToString());
            Assert.AreNotEqual(" ", lines[0][1].ToString());

            // Line 2
            //Assert.AreNotEqual(" ", lines[1][0]);
        }

        /// <summary>
        /// TODO: Add for the last line when last line new line space problem has been fixed.
        /// </summary>
        [TestMethod]
        public void TwoLevels()
        {
            var translationUnit = new NestedCompoundTranslationUnit();
            translationUnit.AddTranslationUnit(new NestedCompoundTranslationUnit());

            string output = translationUnit.Translate();
            string[] lines = GetAllLinesInString(output);

            // line 1
            Assert.AreNotEqual(" ", lines[0][0].ToString());
            Assert.AreNotEqual(" ", lines[0][1].ToString());

            // Line 2
            Assert.AreEqual(" ", lines[1][0].ToString());
            Assert.AreEqual(" ", lines[1][1].ToString());

            // Line 3
            Assert.AreEqual(" ", lines[2][0].ToString());
            Assert.AreEqual(" ", lines[2][1].ToString());

            // Line 4
            //Assert.AreNotEqual(" ", lines[3][0]);
        }

        private static string[] GetAllLinesInString(string source)
        {
            return source.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        /// <summary>
        /// Class used for testing.
        /// </summary>
        /// <remarks>
        /// Implements the behavior that all classes should implement when adding elements
        /// and dealling with nesting
        /// </remarks>
        private class NestedCompoundTranslationUnit : NestedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
        {
            public const string HeaderLexem = "header";

            private IEnumerable<ITranslationUnit> innerUnits;

            public NestedCompoundTranslationUnit()
                : base(AutomaticNestingLevel)
            {
                this.innerUnits = new List<ITranslationUnit>();
            }

            public IEnumerable<ITranslationUnit> InnerUnits
            {
                get
                {
                    return this.innerUnits;
                }
            }

            public new void AddTranslationUnit(ITranslationUnit translationUnit)
            {
                if (translationUnit == null)
                {
                    throw new ArgumentNullException(nameof(translationUnit));
                }

                // Very important this to be here
                if (translationUnit as NestedElementTranslationUnit != null)
                {
                    ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
                }

                (this.innerUnits as List<ITranslationUnit>).Add(translationUnit);
            }

            /// <summary>
            /// Following typical scheme.
            /// </summary>
            /// <returns></returns>
            public string Translate()
            {
                FormatWriter writer = new FormatWriter()
                {
                    Formatter = this.Formatter
                };

                // Opening
                writer.WriteLine("{0} {1}",
                    HeaderLexem,
                    Lexems.OpenCurlyBracket);

                foreach (var translationUnit in this.innerUnits)
                {
                    writer.WriteLine("{0}",
                        translationUnit.Translate());
                }

                // Closing
                writer.WriteLine("{0}",
                    Lexems.CloseCurlyBracket);

                return writer.ToString();
            }
        }
    }
}
