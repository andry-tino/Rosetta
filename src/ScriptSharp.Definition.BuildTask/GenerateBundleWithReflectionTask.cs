/// <summary>
/// GenerateBundleWithReflectionTask.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;
    
    using Rosetta.Reflection.ScriptSharp;

    /// <summary>
    /// The build task.
    /// </summary>
    public class GenerateBundleWithReflectionTask : GenerateBundleTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleWithReflectionTask"/> class.
        /// </summary>
        /// <param name="outputFolder">The folder where to emit generated files.</param>
        /// <param name="assemblyPath">The path to the assembly providing information on symbols in source files.</param>
        /// <param name="references">Additional references to add in the emitted bundle.</param>
        /// <param name="bundleName">The name of the bundle.</param>
        public GenerateBundleWithReflectionTask(string outputFolder, string assemblyPath, IEnumerable<string> references, string bundleName = null)
            : base(new string[0], outputFolder, assemblyPath, references, bundleName)
        {
        }

        protected override string GenerateOutput() => new ProgramWrapper(this.assemblyPath).Output;
    }
}
