/// <summary>
/// GenerateTaskBase.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;

    using Rosetta.ScriptSharp.Definition.AST;

    /// <summary>
    /// The build task base.
    /// </summary>
    public abstract class GenerateTaskBase
    {
        protected const string extension = "d.ts";

        protected readonly IEnumerable<string> sourceFiles;
        protected readonly string assemblyPath;
        protected readonly string outputFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateTaskBase"/> class.
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="outputFolder"></param>
        /// <param name="assemblyPath"></param>
        public GenerateTaskBase(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath = null)
        {
            if (sourceFiles == null)
            {
                throw new ArgumentNullException(nameof(sourceFiles));
            }
            if (outputFolder == null)
            {
                throw new ArgumentNullException(nameof(outputFolder));
            }

            this.sourceFiles = sourceFiles;
            this.assemblyPath = assemblyPath;
            this.outputFolder = outputFolder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract void Run();

        protected static string PerformFileConversion(string source, string assemblyPath)
        {
            var program = new ProgramWrapper(source, assemblyPath);

            return program.Output;
        }
    }
}
