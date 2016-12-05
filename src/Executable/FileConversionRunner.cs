/// <summary>
/// FileConversionRunner.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;
    using System.IO;

    /// <summary>
    /// Converts a file and outputs the translation into a new file.
    /// </summary>
    public class FileConversionRunner : IRunner
    {
        protected readonly ConversionProvider conversionProvider;
        protected readonly ConversionArguments arguments;
        
        protected FileManager fileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileConversionRunner"/> class.
        /// </summary>
        /// <param name="conversionProvider"></param>
        /// <param name="arguments"></param>
        public FileConversionRunner(ConversionProvider conversionProvider, ConversionArguments arguments)
        {
            if (conversionProvider == null)
            {
                throw new ArgumentNullException(nameof(conversionProvider));
            }
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            // Validating arguments
            new ArgumentsValidator(arguments).Validate();

            this.conversionProvider = conversionProvider;
            this.arguments = arguments;
        }

        public void Run()
        {
            this.ConvertFile();
        }

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
            this.arguments.FilePath = this.GetFilePath(this.arguments.FilePath);

            // Setting output folder
            // Making sure this gets translated into absolute path
            this.arguments.OutputDirectory = this.GetOutputFolderForFile(this.arguments.OutputDirectory);

            // Initializing the file manager
            this.fileManager = new FileManager(this.arguments);
            this.fileManager.FileConversionProvider = this.conversionProvider;
        }

        protected virtual void PrepareFiles()
        {
            // This will perform anothe file existing check, redundant as we 
            // do it in initialization routine, but fine!
            fileManager.AddFile(this.arguments.FilePath, this.arguments.FileName); // TODO: arguments have all which is required, change signature
        }

        protected virtual void EmitFiles()
        {
            var writtenFiles = fileManager.WriteAllFilesToDestination(this.arguments.Extension, this.WriteFilesOnEmit);

            foreach (var file in writtenFiles)
            {
                Console.WriteLine("Wrote file {0}", file);
            }
        }

        protected virtual bool WriteFilesOnEmit
        {
            get { return true; }
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
            if (FileManager.IsDirectoryWhereFileResidesCorrect(this.arguments.FilePath))
            {
                return FileManager.ExtractDirectoryPathFromFilePath(this.arguments.FilePath);
            }

            throw new InvalidOperationException("Invalid path provided for input file for extracting output directory!",
                new FileNotFoundException("File not found", arguments.FilePath));
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

        #region Types

        /// <summary>
        /// Custom validator.
        /// </summary>
        private class ArgumentsValidator : IConversionArgumentsValidator
        {
            private readonly ConversionArguments arguments;

            public ArgumentsValidator(ConversionArguments arguments)
            {
                if (arguments == null)
                {
                    throw new ArgumentNullException(nameof(arguments));
                }

                this.arguments = arguments;
            }

            public bool TryValidate()
            {
                throw new NotImplementedException();
            }

            public void Validate()
            {
                if (this.arguments.FilePath == null)
                {
                    throw new ArgumentNullException(nameof(this.arguments.FilePath));
                }

                if (this.arguments.Extension == null)
                {
                    throw new ArgumentNullException(nameof(this.arguments.Extension));
                }

                if (this.arguments.AssemblyPath != null && !File.Exists(this.arguments.AssemblyPath))
                {
                    throw new ArgumentException("Invalid path!", nameof(this.arguments.FilePath));
                }
            }
        }

        #endregion
    }
}
