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
    internal class CustomizableFileConversionRunner : MockedFileConversionRunnerBase
    {
        private Action emitFilesRoutine;
        private Action prepareFilesRoutine;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizableFileConversionRunner"/> class.
        /// </summary>
        /// <param name="emitFilesRoutine">If <code>null</code>, <see cref="FileConversionRunner.EmitFiles"/> will be run normally.</param>
        /// <param name="prepareFilesRoutine">If <code>null</code>, <see cref="FileConversionRunner.PrepareFiles"/> will be run normally.</param>
        /// <param name="conversionProvider"></param>
        /// <param name="filePath"></param>
        /// <param name="outputFolder"></param>
        /// <param name="extension"></param>
        /// <param name="fileName"></param>
        public CustomizableFileConversionRunner(Action emitFilesRoutine, Action prepareFilesRoutine, 
            ConversionProvider conversionProvider, ConversionArguments arguments)
            : base(conversionProvider, arguments)
        {
            this.prepareFilesRoutine = prepareFilesRoutine;
            this.emitFilesRoutine = emitFilesRoutine;
        }

        protected override void EmitFiles()
        {
            if (this.emitFilesRoutine == null)
            {
                base.EmitFiles();
                return;
            }

            this.emitFilesRoutine();
        }

        protected override void PrepareFiles()
        {
            if (this.prepareFilesRoutine == null)
            {
                base.PrepareFiles();
                return;
            }

            this.prepareFilesRoutine();
        }
    }
}
