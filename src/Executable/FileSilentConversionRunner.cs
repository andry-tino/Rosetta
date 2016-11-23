/// <summary>
/// FileConversionRunner.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;
    using System.Linq;

    /// <summary>
    /// Converts a file and outputs the translation into a new file.
    /// </summary>
    public class FileSilentConversionRunner : FileConversionRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSilentConversionRunner"/>.
        /// </summary>
        /// <param name="outputGenerator"></param>
        /// <param name="filePath"></param>
        /// <param name="outputFolder"></param>
        /// <param name="extension"></param>
        /// <param name="fileName"></param>
        public FileSilentConversionRunner(ConversionProvider conversionProvider, string filePath, string outputFolder, string extension, string fileName = null) 
            : base(conversionProvider, filePath, outputFolder, extension, fileName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSilentConversionRunner"/>.
        /// </summary>
        /// <param name="conversionProvider"></param>
        /// <param name="filePath"></param>
        /// <param name="assemblyPath"></param>
        /// <param name="outputFolder"></param>
        /// <param name="extension"></param>
        /// <param name="fileName"></param>
        public FileSilentConversionRunner(ConversionProvider conversionProvider, string filePath, string assemblyPath, string outputFolder, string extension, string fileName = null)
            : base(conversionProvider, filePath, assemblyPath, outputFolder, extension, fileName)
        {
        }

        /// <summary>
        /// Gets the file conversion.
        /// </summary>
        public string FileConversion
        {
            get { return this.fileManager.FileConversions().ElementAt(0); }
        }

        protected override bool WriteFilesOnEmit
        {
            get
            {
                return false;
            }
        }
    }
}
