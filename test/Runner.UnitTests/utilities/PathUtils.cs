using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// PathUtils.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Utils
{
    using System;
    using System.IO;

    /// <summary>
    /// Utils for path handling.
    /// </summary>
    internal static class PathUtils
    {
        /// <summary>
        /// Utility method to get a valid rooted path to a folder which does 
        /// not really exist in the filesystem.
        /// </summary>
        /// <returns></returns>
        public static string TestFolderAbsolutePath
        {
            get
            {
                // Builds an absolute path from a relative path to get a valid folder path
                return Path.Combine(new FileInfo("file1").DirectoryName, "file1");
            }
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
