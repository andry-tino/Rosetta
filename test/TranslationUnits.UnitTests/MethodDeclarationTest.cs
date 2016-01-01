/// <summary>
/// MethodDeclarationTest.cs
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
    public class MethodDeclarationTest
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
        public void PublicVisibilityTokenApplied()
        {
            VisibilityToken visibility = VisibilityToken.Public;
            ITranslationUnit translationUnit = MethodDeclarationTranslationUnit.Create(
                visibility, TypeIdentifierTranslationUnit.Void, IdentifierTranslationUnit.Create("SampleMethod"));

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Method", nameof(this.PublicVisibilityTokenApplied)),
                Utils.FileType.TypeScript);

            Assert.IsTrue(typescript.Contains(TokenUtility.PublicVisibilityToken),
                string.Format("Token {0} expected!", TokenUtility.PublicVisibilityToken));
        }

        [TestMethod]
        public void PrivateVisibilityTokenApplied()
        {
            VisibilityToken visibility = VisibilityToken.Private;
            ITranslationUnit translationUnit = MethodDeclarationTranslationUnit.Create(
                visibility, TypeIdentifierTranslationUnit.Void, IdentifierTranslationUnit.Create("SampleMethod"));

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Method", nameof(this.PrivateVisibilityTokenApplied)),
                Utils.FileType.TypeScript);

            Assert.IsTrue(typescript.Contains(TokenUtility.PrivateVisibilityToken),
                string.Format("Token {0} expected!", TokenUtility.PrivateVisibilityToken));
        }

        [TestMethod]
        public void NoVisibilityTokenApplied()
        {
            VisibilityToken visibility = VisibilityToken.None;
            ITranslationUnit translationUnit = MethodDeclarationTranslationUnit.Create(
                visibility, TypeIdentifierTranslationUnit.Void, IdentifierTranslationUnit.Create("SampleMethod"));

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript,
                string.Format("{0}.Method", nameof(this.NoVisibilityTokenApplied)),
                Utils.FileType.TypeScript);

            Assert.IsFalse(typescript.Contains(TokenUtility.PublicVisibilityToken),
                string.Format("Token {0} not expected!", TokenUtility.PublicVisibilityToken));
            Assert.IsFalse(typescript.Contains(TokenUtility.PrivateVisibilityToken),
                string.Format("Token {0} not expected!", TokenUtility.PrivateVisibilityToken));
        }
    }
}
