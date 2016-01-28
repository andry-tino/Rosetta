/// <summary>
/// Program.ConvertFile.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    using Rosetta.AST;
    
    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        private void ConvertFile()
        {
            this.InitializeForFileConversion();
            this.PrepareFiles();
            this.EmitFiles();
        }

        /// <summary>
        /// Protected virtual for testability.
        /// </summary>
        protected virtual void InitializeForFileConversion()
        {
            // Setting output folder
            this.outputFolder = this.GetOutputFolderForFile(this.outputFolder);

            // Initializing the file manager
            this.fileManager = new FileManager(this.outputFolder);
            this.fileManager.FileConversionProvider = PerformConversion;
        }

        /// <summary>
        /// Protected virtual for testability.
        /// </summary>
        protected virtual void PrepareFiles()
        {
            fileManager.AddFile(filePath, fileName);
        }

        /// <summary>
        /// Protected virtual for testability.
        /// </summary>
        protected virtual void EmitFiles()
        {
            var writtenFiles = fileManager.WriteAllFilesToDestination();

            foreach (var file in writtenFiles)
            {
                Console.WriteLine("Wrote file {0}", file);
            }
        }

        #region Helpers

        /// <summary>
        /// We proceed in this order:
        /// 1. If no path specified: take the path of the input file.
        /// 2. Otherwise, a path is specified: use that.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        private string GetOutputFolderForFile(string userInput)
        {
            if (userInput != null)
            {
                // User provided a path: check the path is all right
                if (FileManager.IsDirectoryPathCorrect(userInput))
                {
                    return userInput;
                }

                // Wrong path
                throw new InvalidOperationException("Invalid path provided for output folder!");
            }

            // User did not provide a path, we get the path of the input file
            if (FileManager.IsFilePathCorrect(this.filePath))
            {
                return FileManager.ExtractDirectoryPathFromFilePath(this.filePath);
            }

            throw new InvalidOperationException("Invalid path provided for input file!");
        }

        #endregion
    }
}
