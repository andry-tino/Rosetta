﻿/// <summary>
/// Program.ConvertAssembly.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Runner
{
    using System;

    using Rosetta.Executable;
    using Rosetta.Reflection.ScriptSharp;

    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        protected virtual void ConvertAssembly()
        {
            var output = new ProgramWrapper(this.assemblyPath).Output;

            // Handling references
            output = this.GeneratePrependedText() + output;

            // Writing
            var outputPath = FileManager.GetAbsolutePath(this.outputFolder);

            if (!FileManager.IsDirectoryPathCorrect(outputPath))
            {
                throw new InvalidOperationException($"Folder '{outputPath}' does not exists!");
            }

            FileManager.WriteToFile(output, outputPath, this.fileName + "." + Extension);
        }
    }
}
