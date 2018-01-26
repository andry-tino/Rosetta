/// <summary>
/// Program.GenerateMscorlibAssembly.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Runner
{
    using System;

    using Rosetta.Diagnostics.Logging;
    using Rosetta.Executable;
    using Rosetta.Reflection.ScriptSharp;

    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        protected virtual void GenerateMscorlibAssembly()
        {
            var program = new MscorlibProgramWrapper();
            program.LogPath = new SysRegLogPathProvider().LogPath;

            var output = program.Output; // We do not handle references here
            var info = program.Info; // To display

            // Writing
            var outputPath = FileManager.GetAbsolutePath(this.outputFolder);

            if (!FileManager.IsDirectoryPathCorrect(outputPath))
            {
                throw new InvalidOperationException($"Folder '{outputPath}' does not exists!");
            }

            FileManager.WriteToFile(output, outputPath, $"{this.FileName}.{Extension}");

            Console.WriteLine($"Definition generated from assembly: {info}.");
        }
    }
}
