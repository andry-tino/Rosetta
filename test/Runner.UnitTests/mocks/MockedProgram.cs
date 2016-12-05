/// <summary>
/// MockedProgram.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Mocks
{
    using System;

    using Rosetta.Executable;

    /// <summary>
    /// Mock for <see cref="Program"/>.
    /// This mock inhibits both <see cref="Program.EmitFiles"/> and <see cref="Program.PrepareFiles"/>.
    /// </summary>
    internal class MockedProgram : MockedProgramBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedProgram"/> class.
        /// </summary>
        /// <param name="args"></param>
        public MockedProgram(string[] args)
            : base(args)
        {
        }

        protected override IRunner CreateFileConversionRunner()
        {
            return new MockedFileConversionRunner(PerformFileConversion, new ConversionArguments()
            {
                FilePath = this.filePath,
                OutputDirectory = this.outputFolder,
                Extension = Extension,
                FileName = this.fileName
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileConversionRunnerFilePath
        {
            get { return (this.FileConversionRunner as MockedFileConversionRunner).FilePath; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileConversionRunnerOutputFolder
        {
            get { return (this.FileConversionRunner as MockedFileConversionRunner).OutputFolder; }
        }
    }
}
