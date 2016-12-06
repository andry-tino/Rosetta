/// <summary>
/// FileAppendableContentConversionRunner.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;

    /// <summary>
    /// Converts a file and outputs the translation into a new file.
    /// </summary>
    public class FileAppendableContentConversionRunner : FileConversionRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileConversionRunner"/> class.
        /// </summary>
        /// <param name="conversionProvider"></param>
        /// <param name="arguments"></param>
        public FileAppendableContentConversionRunner(ConversionProvider conversionProvider, ConversionArguments arguments, string appendedContent) 
            : base(conversionProvider, arguments)
        {
            if (appendedContent == null)
            {
                throw new ArgumentNullException(nameof(appendedContent));
            }

            this.conversionProvider = args => appendedContent + conversionProvider(args);
        }
    }
}
