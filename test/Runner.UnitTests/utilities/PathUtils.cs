/// <summary>
/// PathUtils.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Utils
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Utils for path handling.
    /// </summary>
    internal static class PathUtils
    {
        /// <summary>
        /// Gets the cirrent directory wildcard.
        /// </summary>
        public static string CurrentDirectoryWildcard
        {
            get { return "."; }
        }

        /// <summary>
        /// Utility method to get the path of the test folder where 
        /// test files and executables have been deployed.
        /// </summary>
        /// <returns>The absolute path to test deployment folder.</returns>
        public static string GetTestDeploymentFolder(this TestContext testContext)
        {
            return Path.GetFullPath(testContext.DeploymentDirectory);
        }

        /// <summary>
        /// Utility method to get the path of the test folder where 
        /// tests are being executed.
        /// </summary>
        /// <returns>The absolute path to test execution folder.</returns>
        public static string GetTestExecutionFolder(this TestContext testContext)
        {
            return Path.GetFullPath(testContext.TestRunDirectory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testContext"></param>
        /// <returns></returns>
        public static string GetTestDeploymentFolderRelativeToExecutionFolder(this TestContext testContext)
        {
            Uri executionPath = new Uri(EnsureDirectoryPath(ApplicationExecutingPath));
            Uri deploymentPath = new Uri(EnsureDirectoryPath(testContext.DeploymentDirectory));

            string path = executionPath.MakeRelativeUri(deploymentPath).OriginalString;

            // Path is URL encoded, spaces for example get encoded in `%20` which is not valid in
            // Windows paths, we need to encode the string in a proper way
            path = Uri.UnescapeDataString(path);

            return path;
        }

        /// <summary>
        /// Gets the absolute path of the executing program.
        /// </summary>
        public static string ApplicationExecutingPath
        {
            get
            {
                // To get the location the assembly is executing from
                string executingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                return Path.GetDirectoryName(executingPath);
            }
        }

        /// <summary>
        /// Gets the absolute path for a resource.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(this string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Strips from the path the last character if it is a folder separator.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string StripLastFolderSeparatorFromPath(this string path)
        {
            if (path.Length <= 2)
            {
                return path;
            }

            if (path[path.Length - 1] == Path.DirectorySeparatorChar)
            {
                return path.Substring(0, path.Length - 1);
            }

            return path;
        }

        private static string EnsureDirectoryPath(string pathToDirectory)
        {
            string path = pathToDirectory;

            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar;
            }

            return path;
        }
    }
}
