/// <summary>
/// GenerateBundleTask.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Rosetta.Executable;

    /// <summary>
    /// The build task.
    /// </summary>
    public class GenerateBundleTask : GenerateTaskBase
    {
        private List<FileConversionInfo> outputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleTask"/> class.
        /// </summary>
        public GenerateBundleTask(IEnumerable<string> sourceFiles, string outputFolder) 
            : base(sourceFiles, outputFolder)
        {
            this.outputs = new List<FileConversionInfo>();
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

            var bundle = this.outputs.Aggregate((current, next) => new FileConversionInfo() { FileConversion = current.FileConversion + next.FileConversion }).FileConversion;

            // Writing
            var outputPath = FileManager.GetAbsolutePath(this.outputFolder);

            if (!FileManager.IsDirectoryPathCorrect(outputPath))
            {
                throw new InvalidOperationException($"Folder '{outputPath}' does not exists!");
            }

            FileManager.WriteToFile(bundle, outputPath, "Bundle" + "." + extension);
        }

        private void ConvertFile(string filePath)
        {
            var runner = new FileSilentConversionRunner(PerformFileConversion, filePath, this.outputFolder, extension);
            runner.Run();

            this.outputs.Add(new FileConversionInfo() { FileName = "", FileConversion = runner.FileConversion });
        }

        #region Types

        /// <summary>
        /// 
        /// </summary>
        private class FileConversionInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string FileName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string FileConversion { get; set; }
        }

        #endregion
    }
}
