/// <summary>
/// Program.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Rosetta.AST;

    using Mono.Options;

    /// <summary>
    /// Main program.
    /// </summary>
    internal class Program
    {
        static string FilePath = null;             // File to convert
        static string OutputFolder = null;     // The output folder path for destination files
        static bool Verbose = false;           // Verbosity
        static bool Help = false;              // Show help message

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var options = new OptionSet()
            {
                { "f|file=", "The C# {FILE} to convert into TypeScript.",
                  value => FilePath = value },
                { "o|output=", "The {OUTPUT} folder path where to save files.",
                  value => OutputFolder = value },
                { "v|verbose", "Increase debug message {VERBOSE} level.",
                  value => Verbose = value != null },
                { "h|help",  "Show this message and exit.",
                  value => Help = value != null },
            };

            List<string> extra;
            try
            {
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("An error occurred: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try using option `--help' for more information.");
                return;
            }

            // Priority to help
            if (Help)
            {
                ShowHelp(options);
                return;
            }

            try
            {
                // Setting output folder
                OutputFolder = GetOutputFolder(OutputFolder);

                // We start by considering whether the user specified a file to convert
                if (FilePath != null)
                {
                    ConvertFile();
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: {0}!", e.Message);
                return;
            }
        }

        private static void ConvertFile()
        {
            // Check file path
            if (!FileManager.IsFilePathCorrect(FilePath))
            {
                throw new InvalidOperationException("File path is not correct!");
            }

            string source = File.ReadAllText(FilePath);

            string output = PerformConversion(source);
            WriteToFile(output);

            Console.WriteLine("Wrote file {0}", FilePath);
        }

        #region Helpers

        private static string PerformConversion(string source)
        {
            var program = new ProgramWrapper(source);

            return program.Output;
        }

        private static void WriteToFile(string output)
        {
            FileManager.WriteToFile(output, FilePath);
        }

        private static string GetOutputFolder(string userInput)
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

        private static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: Rosetta [OPTIONS]+ message");
            Console.WriteLine("Converts C# files into TypeScript.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }

        #endregion
    }
}
