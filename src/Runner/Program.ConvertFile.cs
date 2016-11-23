/// <summary>
/// Program.ConvertFile.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner
{
    using System;

    using Rosetta.AST;
    using Rosetta.Executable;

    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        protected const string Extension = "ts";

        protected IRunner fileConversionRunner;

        protected IRunner FileConversionRunner
        {
            get
            {
                if (this.fileConversionRunner == null)
                {
                    this.fileConversionRunner = this.CreateFileConversionRunner();
                }

                return this.fileConversionRunner;
            }
        }

        protected virtual IRunner CreateFileConversionRunner()
        {
            return new FileConversionRunner(PerformFileConversion, this.filePath, this.assemblyPath, this.outputFolder, Extension, this.fileName);
        }

        protected virtual void ConvertFile()
        {
            this.FileConversionRunner.Run();
        }

        protected static string PerformFileConversion(string source, string assemblyPath)
        {
            var program = new ProgramWrapper(source);

            return program.Output;
        }
    }
}
