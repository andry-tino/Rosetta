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
        private static TestContext testContext;
        
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            testContext = context;
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
            program.Run();

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
            var value = testContext.GetTestDeploymentFolder();

            var program = new MockedProgram(new string[]
            {
                ParameterUtils.FileArgumentParameter,
                value // Absolute path
            });
            program.Run();

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
                PathUtils.CurrentDirectoryWildcard
            });
            program.Run();

            Assert.AreEqual(false, program.ErrorHandled, "Error should not have been handled!");
            Assert.AreEqual(PathUtils.ApplicationExecutingPath, program.OutputFolder,
                "Not matching output path!");
        }

        /// <summary>
        /// Tests that providing an absolute path for parameter `output` works fine.
        /// </summary>
        [TestMethod]
        public void AbsoluteOutputPath()
        {
            string absoluteOutputPath = testContext.GetTestDeploymentFolder();

            var program = new MockedProgram(new string[]
            {
                ParameterUtils.FileArgumentParameter,
                "file1",
                ParameterUtils.OutputArgumentParameter,
                absoluteOutputPath
            });
            program.Run();

            Assert.AreEqual(false, program.ErrorHandled, "No error expected!");
            Assert.AreEqual(absoluteOutputPath, program.OutputFolder, "Wrong acquired output path!");
        }

        /// <summary>
        /// Tests that providing a relative path for parameter `output` works fine.
        /// </summary>
        [TestMethod]
        public void RelativeOutputPath()
        {
            // The execution path is different from the deployment path
            // thus we need to retrieve the relative path to deployment folder from execution path
            string relativePathToDeploymentFolder = 
                testContext.GetTestDeploymentFolderRelativeToExecutionFolder();

            var program = new MockedProgram(new string[]
            {
                ParameterUtils.FileArgumentParameter,
                "file1",
                ParameterUtils.OutputArgumentParameter,
                relativePathToDeploymentFolder
            });
            program.Run();

            Assert.AreEqual(false, program.ErrorHandled, "No error expected!");
            Assert.AreEqual(testContext.GetTestDeploymentFolder(), program.OutputFolder, 
                "Wrong acquired output path!");
        }

        /// <summary>
        /// Tests that a wrong file path raises an error.
        /// </summary>
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
            program.Run();

            Assert.AreEqual(true, program.ErrorHandled, "Error should have been handled!");
        }

        #endregion

        #region File path

        /// <summary>
        /// Tests that providing an absolute path for parameter `file` works fine.
        /// </summary>
        /// <remarks>
        /// For file path, the path existence is checked in <see cref="Program.PrepareFiles"/>, thus
        /// we need to use a different mock which does not inhibit this stage.
        /// </remarks>
        [TestMethod]
        public void AbsoluteFilePath()
        {
            string absoluteFilePath = Path.Combine(PathUtils.ApplicationExecutingPath,
                TestObjects.EmptyFile);

            // This will not inhibit the prepare files routine
            var program = new CustomizableMockedProgram(() => { }, null, new string[]
            {
                ParameterUtils.FileArgumentParameter,
                absoluteFilePath
            });
            program.Run();

            Assert.AreEqual(false, program.ErrorHandled, "No error expected!");
            Assert.AreEqual(absoluteFilePath, program.FilePath, "Wrong acquired file path!");
        }

        /// <summary>
        /// Tests that providing a relative path for parameter `file` works fine.
        /// </summary>
        /// <remarks>
        /// For file path, the path existence is checked in <see cref="Program.PrepareFiles"/>, thus
        /// we need to use a different mock which does not inhibit this stage.
        /// </remarks>
        [TestMethod]
        public void RelativeFilePath()
        {
            // This will not inhibit the prepare files routine
            var program = new CustomizableMockedProgram(() => { }, null, new string[]
            {
                ParameterUtils.FileArgumentParameter,
                Path.Combine(PathUtils.CurrentDirectoryWildcard, TestObjects.EmptyFile)
            });
            program.Run();

            Assert.AreEqual(false, program.ErrorHandled, "No error expected!");
            Assert.AreEqual(Path.Combine(PathUtils.ApplicationExecutingPath, TestObjects.EmptyFile),
                program.FilePath, "Wrong acquired file path!");
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
                Path.Combine(PathUtils.CurrentDirectoryWildcard, TestObjects.NotExistingFile)
            });
            program.Run();

            Assert.AreEqual(true, program.ErrorHandled, "Error should have been handled!");
            Assert.IsNotNull(program.ThrownProgramException, "Program expected to emit exception!");
            Assert.IsInstanceOfType(program.ThrownProgramException, typeof(FileNotFoundException));
        }

        #endregion
    }
}
