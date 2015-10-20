/// <summary>
/// TestSuite.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestSuite
    {
        /// <summary>
        /// The test suite context.
        /// </summary>
        internal static TestContext Context
        {
            get; private set;
        }

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Context = context;
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
        }
    }
}
