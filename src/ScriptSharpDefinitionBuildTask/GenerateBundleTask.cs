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
        private readonly string bundleName;

        private List<FileConversionInfo> outputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleTask"/> class.
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="outputFolder"></param>
        /// <param name="bundleName"></param>
        public GenerateBundleTask(IEnumerable<string> sourceFiles, string outputFolder, string bundleName = "Bundle") 
            : this(sourceFiles, outputFolder, null, null, bundleName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleTask"/> class.
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="outputFolder"></param>
        /// <param name="assemblyPath"></param>
        /// <param name="bundleName"></param>
        public GenerateBundleTask(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath, string bundleName = "Bundle")
            : this(sourceFiles, outputFolder, assemblyPath, null, bundleName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleTask"/> class.
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="outputFolder"></param>
        /// <param name="assemblyPath"></param>
        /// <param name="references"></param>
        /// <param name="bundleName"></param>
        public GenerateBundleTask(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath, IEnumerable<string> references, string bundleName = "Bundle")
            : base(sourceFiles, outputFolder, assemblyPath, references)
        {
            this.bundleName = bundleName;

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

            var chuncks = this.outputs.Select(output => string.Join(Environment.NewLine, new[] {
                $"/**",
                $" * File: {output.FileName}",
                $" */",
                $"{output.FileConversion}",
                string.Empty }));

            var bundle = string.Join(Environment.NewLine, chuncks);

            // Writing
            var outputPath = FileManager.GetAbsolutePath(this.outputFolder);

            if (!FileManager.IsDirectoryPathCorrect(outputPath))
            {
                throw new InvalidOperationException($"Folder '{outputPath}' does not exists!");
            }

            FileManager.WriteToFile(bundle, outputPath, this.bundleName + "." + extension);
        }

        private void ConvertFile(string filePath)
        {
            var runner = new FileSilentConversionRunner(PerformFileConversion, new ConversionArguments()
            {
                FilePath = filePath,
                OutputDirectory = this.outputFolder,
                Extension = extension
            });
            runner.Run();

            this.outputs.Add(new FileConversionInfo() { FileName = FileManager.GetFileNameWithExtension(filePath), FileConversion = runner.FileConversion });
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
