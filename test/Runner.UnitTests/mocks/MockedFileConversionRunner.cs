/// <summary>
/// MockedFileConversionRunner.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Runner.UnitTests.Mocks
{
    using System;

    using Rosetta.Executable;

    /// <summary>
    /// Mock for <see cref="FileConversionRunner"/>.
    /// This mock inhibits both <see cref="FileConversionRunner.EmitFiles"/> and <see cref="FileConversionRunner.PrepareFiles"/>.
    /// </summary>
    internal class MockedFileConversionRunner : MockedFileConversionRunnerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedFileConversionRunner"/> class.
        /// </summary>
        /// <param name="conversionProvider"></param>
        /// <param name="filePath"></param>
        /// <param name="outputFolder"></param>
        /// <param name="extension"></param>
        /// <param name="fileName"></param>
        public MockedFileConversionRunner(ConversionProvider conversionProvider, string filePath, string outputFolder, string extension, string fileName = null)
            : base(conversionProvider, filePath, outputFolder, extension, fileName)
        {
        }
        
        protected override void EmitFiles()
        {
            // Do nothing, we do not want to proceed with actual translation
        }

        protected override void PrepareFiles()
        {
            // Do nothing, we do not want to proceed with actual translation
        }
    }
}
