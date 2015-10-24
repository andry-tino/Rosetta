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
