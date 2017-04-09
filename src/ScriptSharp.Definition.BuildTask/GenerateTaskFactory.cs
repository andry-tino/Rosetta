/// <summary>
/// GenerateTaskFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Rosetta.Executable;

    /// <summary>
    /// Factory for creating a <see cref="GenerateTaskBase"/>.
    /// </summary>
    public class GenerateTaskFactory
    {
        private readonly IEnumerable<string> sourceFiles;
        private readonly string outputFolder;
        private readonly string assemblyPath;
        private readonly IEnumerable<string> references;
        private readonly string bundleName;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateTaskFactory"/> class.
        /// </summary>
        /// <param name="sourceFiles">The collection of paths to the files to consider.</param>
        /// <param name="outputFolder">The folder where to emit generated files.</param>
        /// <param name="assemblyPath">The path to the assembly providing information on symbols in source files.</param>
        /// <param name="references">Additional references to add in the emitted bundle.</param>
        /// <param name="bundleName">The name of the bundle.</param>
        public GenerateTaskFactory(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath, IEnumerable<string> references, string bundleName)
        {
            if (outputFolder == null)
            {
                throw new ArgumentNullException(nameof(outputFolder));
            }

            this.sourceFiles = sourceFiles;
            this.outputFolder = outputFolder;
            this.assemblyPath = assemblyPath;
            this.references = references;
            this.bundleName = bundleName;
        }

        /// <summary>
        /// Creates the proper <see cref="GenerateTaskBase"/> given the input parameters.
        /// </summary>
        /// <returns>A <see cref="GenerateTaskBase"/>.</returns>
        public GenerateTaskBase Create()
        {
            if (this.CreateBundle)
            {
                return this.sourceFiles != null 
                    ? new GenerateBundleTask(this.sourceFiles, this.outputFolder, this.assemblyPath, this.references, this.bundleName) 
                    : new GenerateBundleWithReflectionTask(this.outputFolder, this.assemblyPath, this.references, this.bundleName);
            }
            
            return new GenerateFilesTask(this.sourceFiles, this.outputFolder, this.assemblyPath, this.references) as GenerateTaskBase;
        }

        private bool CreateBundle => this.bundleName != null && this.bundleName.Length > 0;
    }
}
