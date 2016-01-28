/// <summary>
/// ProgramUnitTests.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
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
        public void WhenCalledWithNoParameterThenHelpIsShown()
        {
            var program = new MockedProgram(new string[]{ });

            Assert.AreEqual(true, program.HelpContentDisplayed, "Help content should have been displayed!");
            AssertProgramNotRun(program);
        }

        [TestMethod]
        public void WhenHelpParameterSpecifiedThenHelpIsShown()
        {
            var program = new MockedProgram(new string[] 
            {
                "--help"
            });

            Assert.AreEqual(true, program.HelpContentDisplayed, "Help content should have been displayed!");
            AssertProgramNotRun(program);
        }

        [TestMethod]
        public void WhenHelpShortParameterSpecifiedThenHelpIsShown()
        {
            var program = new MockedProgram(new string[]
            {
                "-h"
            });

            Assert.AreEqual(true, program.HelpContentDisplayed, "Help content should have been displayed!");
            AssertProgramNotRun(program);
        }

        [TestMethod]
        public void WhenHelpParameterSpecifiedWithOtherParametersThenHelpIsShown()
        {
            var parameters = new string[][]
            {
                new string[] { "--help", "--file", "file" },
                new string[] { "--help", "--project", "project" },
                new string[] { "--help", "--verbose" },
                new string[] { "--help", "--project", "project", "--file", "file" }
            };

            foreach (var input in parameters)
            {
                var program = new MockedProgram(input);

                Assert.AreEqual(true, program.HelpContentDisplayed, "Help content should have been displayed!");
                AssertProgramNotRun(program);
            }
        }

        #region Helpers

        private static void AssertProgramNotRun(MockedProgram program)
        {
            Assert.AreEqual(false, program.FileConversionRoutineRun, "File conversion routine was not supposed to run!");
            Assert.AreEqual(false, program.ProjectConversionRoutineRun, "Project conversion routine was not supposed to run!");
        }

        #endregion
    }
}
