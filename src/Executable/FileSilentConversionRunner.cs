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
        /// <param name="conversionProvider"></param>
        /// <param name="arguments"></param>
        public FileSilentConversionRunner(ConversionProvider conversionProvider, ConversionArguments arguments) 
            : base(conversionProvider, arguments)
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
