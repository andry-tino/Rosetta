/// <summary>
/// ClassDeclarationTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;
    using Rosetta.Translation.UnitTests.Data;

    [TestClass]
    internal class ClassDeclarationTest
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
        public void VisibilityTokenApplied()
        {
            TestVisibilityTokenApplied(VisibilityToken.Public);
            TestVisibilityTokenApplied(VisibilityToken.Private);
            TestVisibilityTokenApplied(VisibilityToken.Protected);
            TestVisibilityTokenApplied(VisibilityToken.None);
        }

        /// <summary>
        /// Tests a specific visibility for class declaration.
        /// </summary>
        /// <param name="visibility">Visibility to test.</param>
        private static void TestVisibilityTokenApplied(VisibilityToken visibility)
        {
            ITranslationUnit translationUnit = ClassDeclarationTranslationUnit.Create(VisibilityToken.Public, "SampleClass", null);
            string typescript = translationUnit.Translate();

            Assert.IsTrue(typescript.Contains(TokenUtility.ToString(visibility)), 
                string.Format("Token {0} expected!", TokenUtility.ToString(visibility)));
        }
    }
}
