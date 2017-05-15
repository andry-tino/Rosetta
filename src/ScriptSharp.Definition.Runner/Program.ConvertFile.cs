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
        protected virtual void ConvertFile()
        {
            var arguments = new ConversionArguments()
            {
                FilePath = this.filePath,
                AssemblyPath = this.assemblyPath,
                OutputDirectory = this.outputFolder,
                Extension = Extension,
                FileName = this.FileName,
                References = this.includes
            };

            if (this.includes.Count() > 0)
            {
                new FileAppendableContentConversionRunner(PerformFileConversion, arguments, this.GeneratePrependedText()).Run();
                return;
            }

            new FileConversionRunner(PerformFileConversion, arguments).Run();
        }

        protected static string PerformFileConversion(ConversionArguments arguments)
        {
            var program = new ProgramWrapper(
                arguments.Source,
                arguments.AssemblyPath);

            return program.Output;
        }
    }
}
