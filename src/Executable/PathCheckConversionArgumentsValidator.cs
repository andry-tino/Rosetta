/// <summary>
/// PathCheckConversionArgumentsValidator.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;
    using System.IO;

    /// <summary>
    /// A validator checking all paths if arguments are not <code>null</code>.
    /// </summary>
    public class PathCheckConversionArgumentsValidator : IConversionArgumentsValidator
    {
        private readonly ConversionArguments arguments;

        public PathCheckConversionArgumentsValidator(ConversionArguments arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            this.arguments = arguments;
        }

        /// <summary>
        /// Validated the arguments.
        /// </summary>
        /// <returns><code>true</code> if the validation process is successful, <code>false</code> otherwise.</returns>
        public bool TryValidate() => 
            this.FilePathValidation &&
            this.AssemblyPathValidation &&
            this.DirectoryValidation &&
            this.OutputDirectoryValidation;

        /// <summary>
        /// Validates the <see cref="ConversionArguments"/> and if validation is not successful throws an exception.
        /// </summary>
        /// <exception cref="ArgumentException">Raised when one of the properties fails validation.</exception>
        public void Validate()
        {
            if (!this.FilePathValidation)
            {
                throw new ArgumentException("Invalid path!", nameof(this.arguments.FilePath));
            }

            if (!this.AssemblyPathValidation)
            {
                throw new ArgumentException("Invalid path!", nameof(this.arguments.AssemblyPath));
            }

            if (!this.DirectoryValidation)
            {
                throw new ArgumentException("Invalid path!", nameof(this.arguments.Directory));
            }

            if (!this.OutputDirectoryValidation)
            {
                throw new ArgumentException("Invalid path!", nameof(this.arguments.OutputDirectory));
            }
        }

        private bool FilePathValidation => ValidatePathToFileWhenNotNull(this.arguments.FilePath);

        private bool AssemblyPathValidation => ValidatePathToFileWhenNotNull(this.arguments.AssemblyPath);

        private bool DirectoryValidation => ValidatePathToDirectoryWhenNotNull(this.arguments.Directory);

        private bool OutputDirectoryValidation => ValidatePathToDirectoryWhenNotNull(this.arguments.OutputDirectory);
        
        private static bool ValidatePathToFileWhenNotNull(string path) => path == null || File.Exists(path);
        
        private static bool ValidatePathToDirectoryWhenNotNull(string path) => path == null || Directory.Exists(path);
    }
}
