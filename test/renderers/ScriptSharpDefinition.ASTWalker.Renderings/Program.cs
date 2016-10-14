/// <summary>
/// Program.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Renderings
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
            string outputDirectory = args.Length == 1 ?                                     // Prioritize argument
                args[0] :                                                                   // If none, use config
                (ConfigurationManager.AppSettings["OutputFolderPath"] ?? string.Empty);     // Otherwise use current forlder

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
