/// <summary>
/// Program.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Writes all renderings to file.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            string outputDirectory = args.Length == 1 
                ? args[0]                                                   // Prioritize argument
                : (ConfigurationManager.AppSettings["OutputFolderPath"]     // If none, use config
                    ?? string.Empty);                                       // Otherwise use current directory

            if (outputDirectory == string.Empty)
            {
                outputDirectory = Environment.CurrentDirectory;
            }

            Renderer renderer = new Renderer(outputDirectory);

            // Render all
            renderer.Render();
        }
    }
}
