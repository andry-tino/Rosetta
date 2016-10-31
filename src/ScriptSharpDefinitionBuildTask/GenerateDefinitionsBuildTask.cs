/// <summary>
/// BuildTask.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Build;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// The build task.
    /// </summary>
    public class GenerateDefinitionsBuildTask : Task
    {
        /// <summary>
        /// Gets or sets the collection of source files to generate definition files from.
        /// </summary>
        [Required]
        public ITaskItem[] Files { get; set; }

        /// <summary>
        /// Gets or sets the path to the folder where to store files.
        /// </summary>
        [Required]
        public string OutputFolder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a unique definition bundle should be generated.
        /// </summary>
        public bool CreateBundle { get; set; } = false;

        /// <summary>
        /// Gets or sets the name of the generate bundle file. This name does not include the extension.
        /// </summary>
        /// <remarks>
        /// Only works if <see cref="CreateBundle"/> is <code>true</code>.
        /// </remarks>
        public string BundleName { get; set; } = "Bundle";

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateDefinitionsBuildTask"/> class.
        /// </summary>
        public GenerateDefinitionsBuildTask()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Execute()
        {
            try
            {
                this.CreateTask().Run();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        protected IEnumerable<string> SourceFiles
        {
            get { return this.Files.Select(taskItem => taskItem.GetMetadata("FullPath")); }
        }

        private GenerateTaskBase CreateTask()
        {
            return this.CreateBundle
                ? new GenerateBundleTask(this.SourceFiles, this.OutputFolder, this.BundleName)
                : new GenerateFilesTask(this.SourceFiles, this.OutputFolder) as GenerateTaskBase;
        }
    }
}
