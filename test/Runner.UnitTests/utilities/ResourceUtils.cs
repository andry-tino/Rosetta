/// <summary>
/// ResourceUtils.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Utils for resource handling.
    /// </summary>
    internal class ResourceUtils : IDisposable
    {
        private readonly string[] names;
        private readonly string deploymentDirectory;

        private bool deployed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceUtils"/> class.
        /// </summary>
        /// <param name="names">Names of files to create. No paths, just names!</param>
        /// <param name="testContext"></param>
        /// <remarks>
        /// This component will use the deployment directory from the <see cref="TestContext"/>.
        /// </remarks>
        public ResourceUtils(TestContext testContext, string[] names)
        {
            if (testContext == null)
            {
                throw new ArgumentNullException(nameof(testContext));
            }

            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }
            if (names.Length == 0)
            {
                throw new ArgumentException("Input array must contain at least a value!", nameof(names));
            }

            foreach (string name in names)
            {
                if (!CheckIsNotPath(name))
                {
                    throw new ArgumentException("No paths allowed!", nameof(names));
                }
            }

            this.deploymentDirectory = testContext.DeploymentDirectory;
            this.names = names.Select(name => GetPath(this.deploymentDirectory, name)).ToArray();
            this.deployed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Deploy()
        {
            if (this.deployed)
            {
                return;
            }

            foreach (string name in this.names)
            {
                using (File.Open(name, FileMode.CreateNew, FileAccess.ReadWrite)) { }
            }

            this.deployed = true;
        }

        /// <summary>
        /// Gets the name of the deployed files.
        /// </summary>
        public string[] Files
        {
            get { return this.names; }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="Files"/> have been deployed or not.
        /// </summary>
        public bool Deployed
        {
            get { return this.deployed; }
        }

        /// <summary>
        /// Disposes all files.
        /// </summary>
        public void Dispose()
        {
            foreach (string name in this.names)
            {
                if (!File.Exists(name))
                {
                    continue;
                }

                File.Delete(name);
            }
        }

        private static string GetPath(string directory, string fileName)
        {
            return Path.GetFullPath(Path.Combine(directory, fileName));
        }

        private static bool CheckIsNotPath(string value)
        {
            if (value.IndexOf(@"/") >= 0)
            {
                return false;
            }

            if (value.IndexOf(@"\") >= 0)
            {
                return false;
            }

            if (value.IndexOf(@":") >= 0)
            {
                return false;
            }

            return true;
        }
    }
}
