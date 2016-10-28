/// <summary>
/// CustomizableMockedProgram.cs
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
    internal class CustomizableMockedProgram : MockedProgramBase
    {
        private Action emitFilesRoutine;
        private Action prepareFilesRoutine;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizableMockedProgram"/> class.
        /// </summary>
        /// <param name="emitFilesRoutine">
        /// If <code>null</code>, <see cref="Program.EmitFiles"/> will be run normally.
        /// </param>
        /// <param name="prepareFilesRoutine">
        /// If <code>null</code>, <see cref="Program.PrepareFiles"/> will be run normally.
        /// </param>
        /// <param name="args"></param>
        public CustomizableMockedProgram(Action emitFilesRoutine, Action prepareFilesRoutine, string[] args)
            : base(args)
        {
            this.prepareFilesRoutine = prepareFilesRoutine;
            this.emitFilesRoutine = emitFilesRoutine;
        }

        protected override IRunner CreateFileConversionRunner()
        {
            return new CustomizableFileConversionRunner(this.emitFilesRoutine, this.prepareFilesRoutine, PerformFileConversion, this.filePath, this.outputFolder, Extension, this.fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileConversionRunnerFilePath
        {
            get { return (this.FileConversionRunner as CustomizableFileConversionRunner).FilePath; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileConversionRunnerOutputFolder
        {
            get { return (this.FileConversionRunner as CustomizableFileConversionRunner).OutputFolder; }
        }
    }
}
