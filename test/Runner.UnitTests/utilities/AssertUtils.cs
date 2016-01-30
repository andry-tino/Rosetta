/// <summary>
/// AssertUtils.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Utils
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Runner;
    using Rosetta.Runner.UnitTests.Mocks;

    /// <summary>
    /// Mock for <see cref="Program"/>.
    /// </summary>
    internal static class AssertUtils
    {
        public static void AssertProgramNotRun(this MockedProgram program)
        {
            Assert.AreEqual(false, program.FileConversionRoutineRun, "File conversion routine was not supposed to run!");
            Assert.AreEqual(false, program.ProjectConversionRoutineRun, "Project conversion routine was not supposed to run!");
        }

        public static void AssertMainProgramRoutineRun(this MockedProgram program)
        {
            Assert.AreEqual(true, program.MainRunRoutineRun, "Program main run routine was supposed to run!");
        }

        public static void AssertMainProgramRoutineNotRun(this MockedProgram program)
        {
            Assert.AreEqual(false, program.MainRunRoutineRun, "Program main run routine was not supposed to run!");
        }
    }
}
