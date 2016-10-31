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

    using Rosetta.Executable;
    using Rosetta.ScriptSharp.Definition.AST;

    /// <summary>
    /// The build task.
    /// </summary>
    public class GenerateDefinitionsBuildTask : Task
    {
        protected const string Extension = "d.ts";
        
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
        /// Initializes a new instance of the <see cref="GenerateDefinitionsBuildTask"/> class.
        /// </summary>
        public GenerateDefinitionsBuildTask()
        {
        }

        protected virtual IRunner CreateFileConversionRunner(string filePath)
        {
            return new FileConversionRunner(PerformFileConversion, filePath, this.OutputFolder, Extension);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Execute()
        {
            try
            {
                foreach (var file in this.Files)
                {
                    this.ConvertFile(file.GetMetadata("FullPath"));
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private void ConvertFile(string filePath)
        {
            this.CreateFileConversionRunner(filePath).Run();
        }

        private static string PerformFileConversion(string source)
        {
            var program = new ProgramWrapper(source);

            return program.Output;
        }
    }
}
