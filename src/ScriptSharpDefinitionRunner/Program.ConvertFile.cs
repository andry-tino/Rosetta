/// <summary>
/// Program.ConvertFile.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Runner
{
    using System;
    using System.Linq;
    
    using Rosetta.Executable;
    using Rosetta.ScriptSharp.Definition.AST;

    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        protected const string Extension = "d.ts";

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
            return new FileConversionRunner(PerformFileConversion, new ConversionArguments()
            {
                FilePath = this.filePath,
                AssemblyPath = this.assemblyPath,
                OutputDirectory = this.outputFolder,
                Extension = Extension,
                FileName = this.fileName,
                References = this.includes
            });
        }

        protected virtual void ConvertFile()
        {
            this.FileConversionRunner.Run();
        }

        protected static string PerformFileConversion(ConversionArguments arguments)
        {
            var program = new ProgramWrapper(
                arguments.Source,
                arguments.AssemblyPath,
                arguments.References != null && arguments.References.Count() > 0
                    ? arguments.References.ToArray()
                    : null);

            return program.Output;
        }
    }
}
