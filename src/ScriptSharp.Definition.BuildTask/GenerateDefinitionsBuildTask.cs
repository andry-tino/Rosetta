/// <summary>
/// BuildTask.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
        public ITaskItem[] Files { get; set; }

        /// <summary>
        /// Gets or sets the collection of referenced files.
        /// This will cause references in the final file or bundle to be included in the emitted code.
        /// The references to files are emitted as they are, no normalization nor validation on paths is performed.
        /// </summary>
        /// <remarks>
        /// The suggested aprroach is to define an ItemGroup with references there like:
        /// <code>
        /// <ItemGroup>
        ///   <TypeScriptDefinitionReference Include="file1.d.ts" />
        ///   <TypeScriptDefinitionReference Include="file2.d.ts" />
        /// </ItemGroup>
        /// </code>
        /// And reference those in the argument:
        /// <code>
        /// <Target>
        ///   <GenerateDefinitionsBuildTask References="@(TypeScriptDefinitionReference)" />
        /// </Target>
        /// </code>
        /// </remarks>
        public ITaskItem[] References { get; set; }

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
        /// Gets or sets the path to the assembly in order to get the semantic model.
        /// </summary>
        /// <remarks>
        /// Only works if <see cref="CreateBundle"/> is <code>true</code>.
        /// </remarks>
        public string AssemblyPath { get; set; }

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
        /// Executes the build task.
        /// </summary>
        /// <returns>A value indicating whether the task could be completed or not.</returns>
        public override bool Execute()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                if (this.SourceFiles != null)
                {
                    this.Log.LogMessage("Generating TypeScript definitions for {0} file{1}...",
                        this.Files.Count(), this.Files.Count() > 1 ? "s" : string.Empty);
                }
                else
                {
                    this.Log.LogMessage("Generating TypeScript definitions...");
                }

                this.CreateTask().Run();
            }
            catch (Exception ex)
            {
                stopwatch.Reset();

#if DEBUG
                this.Log.LogErrorFromException(new Exception("An error occurred while generating TypeScript definition files.", ex), true, true, null);
#else
                this.Log.LogErrorFromException(new Exception("An error occurred while generating TypeScript definition files.", ex), false, true, null);
#endif

                return false;
            }

            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;
            var timeMessage = string.Format("{0:00}:{1:00}", elapsedTime.Seconds, elapsedTime.Milliseconds);

            this.Log.LogMessage("TypeScript definitions generation completed in {0}. Location: {1}.", timeMessage, this.OutputFolder);
            if (this.CreateBundle)
            {
                this.Log.LogMessage("Bundle '{0}' created!", this.BundleName);
            }

            return true;
        }

        protected IEnumerable<string> SourceFiles => this.Files?.Select(taskItem => taskItem.GetMetadata("FullPath"));

        protected IEnumerable<string> ReferencedFiles => this.References?.Select(taskItem => taskItem.ItemSpec);

        private GenerateTaskBase CreateTask()
        {
            return new GenerateTaskFactory(
                this.SourceFiles, 
                this.OutputFolder, 
                this.AssemblyPath, 
                this.ReferencedFiles,
                this.CreateBundle ? this.BundleName : null)
                .Create();
        }
    }
}
