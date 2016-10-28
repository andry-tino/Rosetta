/// <summary>
/// MockedFileConversionRunnerBase.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Runner.UnitTests.Mocks
{
    using System;

    using Rosetta.Executable;

    /// <summary>
    /// Mock for <see cref="FileConversionRunner"/>.
    /// </summary>
    internal class MockedFileConversionRunnerBase : FileConversionRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedFileConversionRunnerBase"/> class.
        /// </summary>
        /// <param name="conversionProvider"></param>
        /// <param name="filePath"></param>
        /// <param name="outputFolder"></param>
        /// <param name="extension"></param>
        /// <param name="fileName"></param>
        public MockedFileConversionRunnerBase(ConversionProvider conversionProvider, string filePath, string outputFolder, string extension, string fileName = null)
            : base(conversionProvider, filePath, outputFolder, extension, fileName)
        {
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath
        {
            get { return this.filePath; }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string OutputFolder
        {
            get { return this.outputFolder; }
        }
    }
}
