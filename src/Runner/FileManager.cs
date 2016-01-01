/// <summary>
/// FileManager.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Collections;

    /// <summary>
    /// Handles files. Reading, writing and general handling.
    /// </summary>
    internal class FileManager : IEnumerable<string>
    {
        private IEnumerable<string> files;

        /// <summary>
        /// 
        /// </summary>
        public FileManager(string rootDirectoryPath)
        {
            if (rootDirectoryPath == null)
            {
                throw new ArgumentNullException("rootDirectoryPath");
            }

            FileAttributes attributes;
            try
            {
                attributes = File.GetAttributes(rootDirectoryPath);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error trying to access path {0}!", rootDirectoryPath), e);
            }

            // File was specified
            if (!attributes.HasFlag(FileAttributes.Directory))
            {
                files = new string[] { rootDirectoryPath };
                return;
            }

            // A directory was specified
            files = GetAllFiles(rootDirectoryPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return this.files.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.files.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="path"></param>
        public static void WriteToFile(string output, string path)
        {
            File.WriteAllText(path, output);
        }

        #region Utilities

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public static string ApplicationAssemblyPath
        {
            get
            {
                // To get the location the assembly normally resides on disk or the install directory
                string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                return Path.GetDirectoryName(assemblyPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectoryPathCorrect(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return Directory.Exists(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFilePathCorrect(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return File.Exists(path);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<string> CSharpFiles
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private static IEnumerable<string> GetAllFiles(string path)
        {
            if (path == null || path == string.Empty)
            {
                return new string[0];
            }

            var files = new List<string>();
            files.AddRange(Directory.GetFiles(path));

            foreach (string directory in Directory.GetDirectories(path))
            {
                files.AddRange(GetAllFiles(directory));
            }

            return files;
        }
    }
}
