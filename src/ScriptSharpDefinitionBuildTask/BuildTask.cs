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
    }
}
