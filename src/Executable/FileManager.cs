/// <summary>
/// FileManager.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Executable
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;
    using System.Collections;

    /// <summary>
    /// Handles files. Reading, writing and general handling.
    /// </summary>
    /// <remarks>
    /// This class is not responsible for discvering files to convert. 
    /// It simply allows us to specify which files to convert, how to convert 
    /// them and where to write output files.
    /// </remarks>
    public class FileManager : IEnumerable<FileManager.FileEntry>
    {
        // The paths of files to convert and conversions
        private IEnumerable<FileEntry> fileEntries;

        // The path to output directory
        private readonly string directory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        public FileManager(string directoryPath)
        {
            if (directoryPath == null)
            {
                throw new ArgumentNullException(nameof(directoryPath));
            }

            if (!IsDirectoryPathCorrect(directoryPath))
            {
                throw new ArgumentException("Invalid path!", nameof(directoryPath));
            }

            this.directory = directoryPath;
            this.FileConversionProvider = input => string.Empty;

            this.fileEntries = new List<FileEntry>();
        }

        /// <summary>
        /// Gets or sets the conversion provider.
        /// </summary>
        public ConversionProvider FileConversionProvider { get; set; }

        /// <summary>
        /// Gets the directory path.
        /// </summary>
        public string DirectoryPath
        {
            get { return this.directory; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<FileEntry> GetEnumerator()
        {
            return this.fileEntries.GetEnumerator();
        }

        /// <summary>
        /// Gets the list of files.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Files()
        {
            return this.fileEntries.Select(entry => entry.FilePath);
        }

        /// <summary>
        /// Gets the list of file conversions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> FileConversions()
        {
            return this.fileEntries.Select(entry => entry.FileConversion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.fileEntries.GetEnumerator();
        }

        /// <summary>
        /// Adds a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="newName">The name to give to the output file.</param>
        public void AddFile(string filePath, string newName = null)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!FileExists(filePath))
            {
                throw new FileNotFoundException("Invalid path!", nameof(filePath));
            }

            ((List<FileEntry>)this.fileEntries).Add(new FileEntry()
                {
                    FilePath = filePath,
                    NewName = newName,
                    FileConversion = string.Empty
                });
        }

        /// <summary>
        /// Applies <see cref="FileConversionProvider"/> to all files and stores result in the couple.
        /// </summary>
        public void ApplyConversion()
        {
            foreach (var entry in this.fileEntries)
            {
                entry.FileConversion = this.FileConversionProvider(File.ReadAllText(entry.FilePath));
            }
        }

        /// <summary>
        /// Causes <see cref="ApplyConversion"/> to be called and all files to be written to <see cref="DirectoryPath"/>.
        /// </summary>
        /// <param name="extension">The extension (without dot).</param>
        /// <returns></returns>
        public IEnumerable<string> WriteAllFilesToDestination(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension), "An extension must be specified!");
            }

            var writtenFiles = new List<string>();

            this.ApplyConversion();

            foreach (var entry in this.fileEntries)
            {
                var destinationFilePath = GetDestinationFilePath(entry.FilePath, extension, entry.NewName);
                WriteToFile(entry.FileConversion, destinationFilePath);
                writtenFiles.Add(destinationFilePath);
            }

            return writtenFiles;
        }

        #region Utilities
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="path"></param>
        public static void WriteToFile(string output, string path)
        {
            File.WriteAllText(path, output);
        }

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
        public static string GetAbsolutePath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            // The path does not need to exist
            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Checks that the folder where a file resides is a valid folder path.
        /// </summary>
        /// <param name="path">Path to the file to check.</param>
        /// <returns>A value indicating whether the path of the file is correct or not.</returns>
        public static bool IsDirectoryWhereFileResidesCorrect(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return Directory.Exists(
                new FileInfo(path).DirectoryName);
        }

        /// <summary>
        /// Check that the file exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Extracts the folder path for the specified file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The absolute path to the directory.</returns>
        public static string ExtractDirectoryPathFromFilePath(string path)
        {
            return new FileInfo(path).DirectoryName;
        }

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

        #endregion

        #region Types

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public delegate string ConversionProvider(string source);

        /// <summary>
        /// The file entry.
        /// </summary>
        public sealed class FileEntry
        {
            /// <summary>
            /// The path to the C# file. This should include the file name with CS extension.
            /// </summary>
            public string FilePath { get; set; }

            /// <summary>
            /// The new name to assign to the file when emitting it. If this paramter is not specified, the 
            /// emitted file will have the same name as <see cref="FilePath"/> but TS extension. If this
            /// property is not left <code>null</code>, then the file name will have
            /// the value specified in <see cref="NewName"/>, and the extension will be overwritten as well.
            /// Thus <see cref="NewName"/> is expected to include the extension if any is wanted.
            /// </summary>
            public string NewName { get; set; }

            /// <summary>
            /// The TypeScript code conversion.
            /// </summary>
            public string FileConversion { get; set; }
        }

        #endregion

        /// <summary>
        /// Strips the file name from the path and concatenates it to <see cref="DirectoryPath"/>.
        /// Also, different extension is applied.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private string GetDestinationFilePath(string filepath, string extension, string newName = null)
        {
            if (newName == null)
            {
                return Path.Combine(directory, Path.GetFileName(Path.ChangeExtension(filepath, extension)));
            }

            return Path.Combine(directory, newName);
        }
    }
}
