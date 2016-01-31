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
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetTestFolderAbsolutePath()
        {
            // Builds an absolute path from a relative path to get a valid folder path
            return Path.Combine(new FileInfo("file1").DirectoryName, "file1"); ;
        }
    }
}
