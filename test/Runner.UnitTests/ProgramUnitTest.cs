/// <summary>
/// ProgramUnitTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Executable.Exceptions;
    using Rosetta.Runner.UnitTests.Mocks;
    using Rosetta.Runner.UnitTests.Utils;

    [TestClass]
    public class ProgramUnitTest
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
        /// This tests that passing no parameter causes runner not to execute the main routine.
        /// </summary>
        [TestMethod]
        public void WhenConstructedProgramIsNotRun()
        {
            var program = new MockedProgram(new string[] { });

            Assert.AreEqual(false, program.MainRunRoutineRun, "User should be notified about not feasible execution!");
        }
    }
}
