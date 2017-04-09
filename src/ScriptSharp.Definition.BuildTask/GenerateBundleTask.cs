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
        private const string DefaultBundleName = "Bundle";

        private readonly string bundleName;

        private List<FileConversionInfo> outputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleTask"/> class.
        /// </summary>
        /// <param name="sourceFiles">The collection of paths to the files to consider.</param>
        /// <param name="outputFolder">The folder where to emit generated files.</param>
        /// <param name="bundleName">The name of the bundle.</param>
        public GenerateBundleTask(IEnumerable<string> sourceFiles, string outputFolder, string bundleName = null) 
            : this(sourceFiles, outputFolder, null, null, bundleName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleTask"/> class.
        /// </summary>
        /// <param name="sourceFiles">The collection of paths to the files to consider.</param>
        /// <param name="outputFolder">The folder where to emit generated files.</param>
        /// <param name="assemblyPath">The path to the assembly providing information on symbols in source files.</param>
        /// <param name="bundleName">The name of the bundle.</param>
        public GenerateBundleTask(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath, string bundleName = null)
            : this(sourceFiles, outputFolder, assemblyPath, null, bundleName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateBundleTask"/> class.
        /// </summary>
        /// <param name="sourceFiles">The collection of paths to the files to consider.</param>
        /// <param name="outputFolder">The folder where to emit generated files.</param>
        /// <param name="assemblyPath">The path to the assembly providing information on symbols in source files.</param>
        /// <param name="references">Additional references to add in the emitted bundle.</param>
        /// <param name="bundleName">The name of the bundle.</param>
        public GenerateBundleTask(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath, IEnumerable<string> references, string bundleName = null)
            : base(sourceFiles, outputFolder, assemblyPath, references)
        {
            this.bundleName = bundleName ?? DefaultBundleName;

            this.outputs = new List<FileConversionInfo>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override void Run()
        {
            var bundle = this.GenerateOutput();

            // Handling references
            bundle = this.GeneratePrependedText() + bundle;

            // Writing
            var outputPath = FileManager.GetAbsolutePath(this.outputFolder);

            if (!FileManager.IsDirectoryPathCorrect(outputPath))
            {
                throw new InvalidOperationException($"Folder '{outputPath}' does not exists!");
            }

            FileManager.WriteToFile(bundle, outputPath, this.bundleName + "." + extension);
        }

        protected virtual string GenerateOutput()
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

            return string.Join(Environment.NewLine, chuncks);
        }

        private void ConvertFile(string filePath)
        {
            var runner = new FileSilentConversionRunner(PerformFileConversion, new ConversionArguments()
            {
                FilePath = filePath,
                OutputDirectory = this.outputFolder,
                Extension = extension,
                AssemblyPath = this.assemblyPath,
                References = this.references
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
