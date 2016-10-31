/// <summary>
/// TokenUtilityUnitTest.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;

    [TestClass]
    public class TokenUtilityUnitTest
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
        public void Public()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Public;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Protected()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Protected;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Protected), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Private()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Private;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Private), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Internal()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Internal;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Static()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Static;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticPublic()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Static;
            source |= VisibilityToken.Public;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticProtected()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Static;
            source |= VisibilityToken.Protected;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Protected), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticPrivate()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Static;
            source |= VisibilityToken.Private;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Private), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticInternal()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Static;
            source |= VisibilityToken.Internal;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void InternalProtected()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Internal;
            source |= VisibilityToken.Protected;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticInternalProtected()
        {
            VisibilityToken source = VisibilityToken.None;
            source |= VisibilityToken.Static;
            source |= VisibilityToken.Internal;
            source |= VisibilityToken.Protected;

            VisibilityToken equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(VisibilityToken.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(VisibilityToken.Static), "A flag was expected in the equivalent modifiers list.");
        }
    }
}
