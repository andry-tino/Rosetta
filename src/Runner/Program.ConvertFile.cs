/// <summary>
/// Program.ConvertFile.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner
{
    using System;
    using System.IO;
    
    using Rosetta.Executable;

    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        private const string Extension = "ts";

        protected virtual void ConvertFile()
        {
            this.InitializeForFileConversion();
            this.PrepareFiles();
            this.EmitFiles();
        }

        // TODO: Move this in shared part as also the routine for converting projects will need to do this
        protected virtual void InitializeForFileConversion()
        {
            // Making sure we translate it into absolute path
            // Attention: Path correctness is checked later
            // Important: File path must be converted into absolute now 
            // as the calculation for output folder relies on this!
            this.filePath = this.GetFilePath(this.filePath);

            // Setting output folder
            // Making sure this gets translated into absolute path
            this.outputFolder = this.GetOutputFolderForFile(this.outputFolder);

            // Initializing the file manager
            this.fileManager = new FileManager(this.outputFolder);
            this.fileManager.FileConversionProvider = PerformConversion;
        }
        
        protected virtual void PrepareFiles()
        {
            // This will perform anothe file existing check, redundant as we 
            // do it in initialization routine, but fine!
            fileManager.AddFile(this.filePath, this.fileName);
        }
        
        protected virtual void EmitFiles()
        {
            var writtenFiles = fileManager.WriteAllFilesToDestination(Extension);

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
        /// <param name="userInput">The user input. Can be <code>null</code>.</param>
        /// <returns>The absolute path to the output folder basing on user input.</returns>
        private string GetOutputFolderForFile(string userInput)
        {
            if (userInput != null)
            {
                // User provided a path: check the path is all right
                // But since the path might be relative, we must ensure it is absolute
                string userProvidedPath = FileManager.GetAbsolutePath(userInput);
                if (FileManager.IsDirectoryPathCorrect(userProvidedPath))
                {
                    return userProvidedPath;
                }

                // Wrong path
                throw new InvalidOperationException("Invalid path provided for output folder!", 
                    new DirectoryNotFoundException(string.Format("Directory {0} not found", userInput)));
            }

            // User did not provide a path, we get the path of the input file
            // Attention, the path might be relative
            if (FileManager.IsDirectoryWhereFileResidesCorrect(this.filePath))
            {
                return FileManager.ExtractDirectoryPathFromFilePath(this.filePath);
            }

            throw new InvalidOperationException("Invalid path provided for input file for extracting output directory!",
                new FileNotFoundException("File not found", filePath));
        }

        /// <summary>
        /// Checks that the path is OK and also translates into absolute path.
        /// </summary>
        /// <param name="userInput">
        /// The user input. At this point this is supposed not to be <code>null</code>.
        /// </param>
        /// <returns></returns>
        private string GetFilePath(string userInput)
        {
            return FileManager.GetAbsolutePath(userInput);
        }

        #endregion
    }
}
