/// <summary>
/// PathHandlingUnitTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Runner.Exceptions;
    using Rosetta.Runner.UnitTests.Mocks;
    using Rosetta.Runner.UnitTests.Utils;

    [TestClass]
    public class PathHandlingUnitTest
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
        public void WhenNoOutputPathIsSpecifiedFilePathIsUsedWhenRelative()
        {
            var value = "file1";

            var program = new MockedProgram(new string[]
            {
                ParameterUtils.FileArgumentParameter,
                value // Relative path
            });

            Assert.IsNotNull(program.OutputFolder, "Expecting a file path!");

            var path = new FileInfo(value).DirectoryName;
            Assert.AreEqual(path, program.OutputFolder);
        }

        [TestMethod]
        public void WhenNoOutputPathIsSpecifiedFilePathIsUsedWhenAbsolute()
        {
            // In the end we get an absolute path we can pass to runner!
            var value = PathUtils.GetTestFolderAbsolutePath();

            var program = new MockedProgram(new string[]
            {
                ParameterUtils.FileArgumentParameter,
                value // Absolute path
            });

            Assert.IsNotNull(program.OutputFolder, "Expecting a file path!");

            // We build the folder path from scratch by design
            var path = new FileInfo(value).DirectoryName;
            Assert.AreEqual(path, program.OutputFolder);
        }
    }
}
