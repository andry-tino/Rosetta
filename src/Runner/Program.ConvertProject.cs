/// <summary>
/// Program.ConvertProject.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner
{
    using System;

    using Rosetta.Executable;

    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        [Obsolete("This scenario is covered by Rosetta.ScriptSharp.Definition.BuildTask", false)]
        protected virtual void ConvertProject()
        {
            // TODO
        }

        #region Helpers

        private static string GetOutputFolderForProject(string userInput)
        {
            if (userInput != null)
            {
                // User provided a path: check the path is all right
                if (FileManager.IsDirectoryPathCorrect(userInput))
                {
                    return userInput;
                }

                // Wrong path
                throw new InvalidOperationException("Invalid path provided!");
            }

            // User did not provide a path, we get the current path
            return FileManager.ApplicationExecutingPath;
        }

        #endregion
    }
}
