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
            // Prioritize argument, if none, use config, if none, use current directory
            string outputDirectory = args.Length == 1 ? 
                args[0] : 
                (ConfigurationManager.AppSettings["OutputFolderPath"] ?? string.Empty);

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
