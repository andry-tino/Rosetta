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

    /// <summary>
    /// Testing paths.
    /// </summary>
    /// <remarks>
    /// A conversion process must be chosen. 
    /// For verifying paths, we choose to test only when converting files, the procedure 
    /// to get paths is the same when converting projects anyway!
    /// </remarks>
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

        #region Output path

        /// <summary>
        /// Testing that file path is used as output path when file path is relative.
        /// </summary>
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

        /// <summary>
        /// Testing that file path is used as output path when file path is absolute.
        /// </summary>
        [TestMethod]
        public void WhenNoOutputPathIsSpecifiedFilePathIsUsedWhenAbsolute()
        {
            // In the end we get an absolute path we can pass to runner!
            var value = PathUtils.TestFolderAbsolutePath;

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

        [TestMethod]
        public void CurrentDirectoryWildcardAsOutputPath()
        {
            var program = new MockedProgram(new string[]
            {
                ParameterUtils.FileArgumentParameter,
                "file1",
                ParameterUtils.OutputArgumentParameter,
                "."
            });

            Assert.AreEqual(false, program.ErrorHandled, "Error should not have been handled!");
            Assert.AreEqual(PathUtils.ApplicationExecutingPath, program.OutputFolder,
                "Not matching output path!");
        }

        [TestMethod]
        [Ignore]
        public void AbsoluteOutputPath()
        {
        }

        [TestMethod]
        [Ignore]
        public void RelativeOutputPath()
        {
        }

        [TestMethod]
        public void WrongOutputPath()
        {
            var program = new MockedProgram(new string[]
            {
                ParameterUtils.FileArgumentParameter,
                "file1",
                ParameterUtils.OutputArgumentParameter,
                "wrongFolder1"
            });

            Assert.AreEqual(true, program.ErrorHandled, "Error should have been handled!");
        }

        #endregion

        #region File path

        [TestMethod]
        [Ignore]
        public void AbsoluteFilePath()
        {
        }

        [TestMethod]
        [Ignore]
        public void RelativeFilePath()
        {
        }

        /// <summary>
        /// Tests that a wrong file path raises an error.
        /// </summary>
        /// <remarks>
        /// For file path, the path existence is checked in <see cref="Program.PrepareFiles"/>, thus
        /// we need to use a different mock which does not inhibit this stage.
        /// </remarks>
        [TestMethod]
        public void WrongFilePath()
        {
            // This will not inhibit the prepare files routine
            var program = new CustomizableMockedProgram(() => { }, null, new string[]
            {
                ParameterUtils.FileArgumentParameter,
                "file1"
            });

            Assert.AreEqual(true, program.ErrorHandled, "Error should have been handled!");
            Assert.IsNotNull(program.ThrownProgramException, "Program expected to emit exception!");
            Assert.IsInstanceOfType(program.ThrownProgramException, typeof(FileNotFoundException));
        }

        #endregion
    }
}
