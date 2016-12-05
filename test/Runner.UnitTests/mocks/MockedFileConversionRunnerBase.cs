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
        /// <param name="arguments"></param>
        public MockedFileConversionRunnerBase(ConversionProvider conversionProvider, ConversionArguments arguments)
            : base(conversionProvider, arguments)
        {
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath
        {
            get { return this.arguments.FilePath; }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string OutputFolder
        {
            get { return this.arguments.OutputDirectory; }
        }
    }
}
