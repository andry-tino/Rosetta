/// <summary>
/// ModifiersTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Tests.Utils;
    using Rosetta.Translation;

    /// <summary>
    /// Tests for <see cref="Modifiers"/> class.
    /// </summary>
    [TestClass]
    public class ModifiersTest
    {
        /// <summary>
        /// Tests a specific combination of modifiers: [public].
        /// </summary>
        [TestMethod]
        public void Public()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Public));
        }

        /// <summary>
        /// Tests a specific combination of modifiers: [protected].
        /// </summary>
        [TestMethod]
        public void Protected()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Protected));
        }

        /// <summary>
        /// Tests a specific combination of modifiers: [private].
        /// </summary>
        [TestMethod]
        public void Private()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Private));
        }

        /// <summary>
        /// Tests a specific combination of modifiers: [internal].
        /// </summary>
        [TestMethod]
        public void Internal()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.InternalKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Internal));
        }

        /// <summary>
        /// Tests a specific combination of modifiers: [static, public].
        /// </summary>
        [TestMethod]
        public void StaticPublic()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Public));
            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Static));
        }

        /// <summary>
        /// Tests a specific combination of modifiers: [static, protected].
        /// </summary>
        [TestMethod]
        public void StaticProtected()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Protected));
            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Static));
        }

        /// <summary>
        /// Tests a specific combination of modifiers: [static, private].
        /// </summary>
        [TestMethod]
        public void StaticPrivate()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Private));
            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Static));
        }

        /// <summary>
        /// Tests a specific combination of modifiers: [static, internal].
        /// </summary>
        [TestMethod]
        public void StaticInternal()
        {
            var syntaxTokenList = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.InternalKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            var visibilityToken = Modifiers.Get(syntaxTokenList);

            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Internal));
            Assert.IsTrue(visibilityToken.HasFlag(ModifierTokens.Static));
        }
    }
}
