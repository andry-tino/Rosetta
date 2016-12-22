/// <summary>
/// GenerateFilesTask.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Executable;

    /// <summary>
    /// The build task.
    /// </summary>
    public class GenerateFilesTask : GenerateTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateFilesTask"/> class.
        /// </summary>
        public GenerateFilesTask(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath = null, IEnumerable<string> references = null) 
            : base(sourceFiles, outputFolder, assemblyPath, references)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override void Run()
        {
            foreach (var file in this.sourceFiles)
            {
                this.ConvertFile(file);
            }
        }

        private void ConvertFile(string filePath)
        {
            var arguments = new ConversionArguments()
            {
                FilePath = filePath,
                OutputDirectory = this.outputFolder,
                Extension = extension,
                AssemblyPath = this.assemblyPath,
                References = this.references
            };

            if (this.ReferencesDefined)
            {
                new FileAppendableContentConversionRunner(PerformFileConversion, arguments, this.GeneratePrependedText()).Run();
                return;
            }

            new FileConversionRunner(PerformFileConversion, arguments).Run();
        }
    }
}
