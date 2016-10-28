/// <summary>
/// BuildTask.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Build;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    using Rosetta.ScriptSharp.Definition.AST;

    /// <summary>
    /// The build task.
    /// </summary>
    public class BuildTask : Task
    {
        /// <summary>
        /// Gets or sets the collection of source files to generate definition files from.
        /// </summary>
        [Required]
        public IEnumerable<string> Files { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTask"/> class.
        /// </summary>
        public BuildTask()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Execute()
        {
            throw new NotImplementedException();
        }

        private static string PerformConversion(string source)
        {
            var program = new ProgramWrapper(source);

            return program.Output;
        }
    }
}
