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
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Public;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Protected()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Protected;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Protected), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Private()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Private;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Private), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Internal()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Internal;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void Static()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Static;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticPublic()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Static;
            source |= ModifierTokens.Public;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticProtected()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Static;
            source |= ModifierTokens.Protected;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Protected), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticPrivate()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Static;
            source |= ModifierTokens.Private;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Public), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Private), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticInternal()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Static;
            source |= ModifierTokens.Internal;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Static), "A flag was expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void InternalProtected()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Internal;
            source |= ModifierTokens.Protected;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Static), "A flag was not expected in the equivalent modifiers list.");
        }

        [TestMethod]
        public void StaticInternalProtected()
        {
            ModifierTokens source = ModifierTokens.None;
            source |= ModifierTokens.Static;
            source |= ModifierTokens.Internal;
            source |= ModifierTokens.Protected;

            ModifierTokens equivalent = source.ConvertToTypeScriptEquivalent();

            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Public), "A flag was expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Protected), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Private), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsFalse(equivalent.HasFlag(ModifierTokens.Internal), "A flag was not expected in the equivalent modifiers list.");
            Assert.IsTrue(equivalent.HasFlag(ModifierTokens.Static), "A flag was expected in the equivalent modifiers list.");
        }
    }
}
