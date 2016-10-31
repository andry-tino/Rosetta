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
        protected readonly string outputFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateTaskBase"/> class.
        /// </summary>
        public GenerateTaskBase(IEnumerable<string> sourceFiles, string outputFolder)
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
            this.outputFolder = outputFolder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract void Run();

        protected static string PerformFileConversion(string source)
        {
            var program = new ProgramWrapper(source);

            return program.Output;
        }
    }
}
