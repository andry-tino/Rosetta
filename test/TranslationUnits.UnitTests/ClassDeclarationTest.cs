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
    using Utils = Rosetta.Tests.Utils;

    [TestClass]
    public class ClassDeclarationTest
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
            ModifierTokens visibility = ModifierTokens.Public;
            ITranslationUnit translationUnit = ClassDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create("SampleClass"), null);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript, 
                string.Format("{0}.Class", nameof(this.PublicVisibilityTokenApplied)), 
                Utils.FileType.TypeScript);

            Assert.IsTrue(typescript.Contains(TokenUtility.PublicVisibilityToken),
                string.Format("Token {0} expected!", TokenUtility.PublicVisibilityToken));
        }

        [TestMethod]
        public void PrivateVisibilityTokenApplied()
        {
            ModifierTokens visibility = ModifierTokens.Private;
            ITranslationUnit translationUnit = ClassDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create("SampleClass"), null);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript, 
                string.Format("{0}.Class", nameof(this.PrivateVisibilityTokenApplied)),
                Utils.FileType.TypeScript);

            Assert.IsTrue(typescript.Contains(TokenUtility.PrivateVisibilityToken),
                string.Format("Token {0} expected!", TokenUtility.PrivateVisibilityToken));
        }

        [TestMethod]
        public void NoVisibilityTokenApplied()
        {
            ModifierTokens visibility = ModifierTokens.None;
            ITranslationUnit translationUnit = ClassDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create("SampleClass"), null);

            string typescript = translationUnit.Translate();
            new Utils.FileWriter(TestSuite.Context).WriteToFile(typescript, 
                string.Format("{0}.Class", nameof(this.NoVisibilityTokenApplied)),
                Utils.FileType.TypeScript);

            Assert.IsFalse(typescript.Contains(TokenUtility.PublicVisibilityToken),
                string.Format("Token {0} not expected!", TokenUtility.PublicVisibilityToken));
            Assert.IsFalse(typescript.Contains(TokenUtility.PrivateVisibilityToken),
                string.Format("Token {0} not expected!", TokenUtility.PrivateVisibilityToken));
        }
    }
}
