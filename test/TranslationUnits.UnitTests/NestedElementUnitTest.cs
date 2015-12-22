/// <summary>
/// NestedElementUnitTest.cs
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
    public class NestedElementUnitTest
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
        /// Tests that the default formatter is the <see cref="WhiteSpaceFormatter"/>.
        /// </summary>
        [TestMethod]
        public void DefaultFormatter()
        {
            NestedElementTranslationUnit translationUnit = new NestedTranslationUnit();

            Assert.IsInstanceOfType(translationUnit.Formatter, typeof(WhiteSpaceFormatter), 
                string.Format("Default formatter should be of type {0}!", typeof(WhiteSpaceFormatter).Name));
            Assert.IsTrue(translationUnit.Formatter.GetType() == typeof(WhiteSpaceInvariantFormatter), 
                string.Format("Default formatter should be of type {0}!", typeof(WhiteSpaceInvariantFormatter).Name));
        }

        /// <summary>
        /// Tests that the default formatter provider returns a <see cref="WhiteSpaceFormatter"/>.
        /// </summary>
        [TestMethod]
        public void DefaultFormatterProvider()
        {
            NestedElementTranslationUnit translationUnit = new NestedTranslationUnit();

            Func<int, IFormatter> formatterProvider = translationUnit.FormatterProvider;
            Assert.IsNotNull(formatterProvider, "Formatter provider cannot be null!");

            IFormatter formatter = formatterProvider(0);
            Assert.IsNotNull(formatter, "Returned formatter cannot be null!");
            Assert.IsInstanceOfType(formatter, typeof(WhiteSpaceFormatter),
                string.Format("Default formatter should be of type {0}!", typeof(WhiteSpaceFormatter).Name));
            Assert.IsTrue(formatter.GetType() == typeof(WhiteSpaceInvariantFormatter), 
                string.Format("Default formatter should be of type {0}!", typeof(WhiteSpaceInvariantFormatter).Name));
        }

        /// <summary>
        /// Class used for testing
        /// </summary>
        private class NestedTranslationUnit : NestedElementTranslationUnit
        {
            public NestedTranslationUnit() 
                : base()
            {
            }
        }
    }
}
