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
            Uri executionPath = new Uri(ApplicationExecutingPath);
            Uri deploymentPath = new Uri(testContext.DeploymentDirectory);
            
            return executionPath.MakeRelativeUri(deploymentPath).OriginalString;
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
        public static string GetAbsolutePath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            return Path.GetFullPath(path);
        }
    }
}
