/// <summary>
/// ProgramUnitTests.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Runner;
    using Rosetta.Runner.UnitTests.Mocks;

    [TestClass]
    public class ProgramUnitTests
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
        public void WhenCalledWithNoParameterHelpIsShown()
        {
            var program = new MockedProgram(new string[]{ });

            Assert.AreEqual(true, program.HelpContentDisplayed, "Help content should have been displayed!");
        }
    }
}
