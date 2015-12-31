/// <summary>
/// Program.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner
{
    using System;
    using System.Collections.Generic;

    using Mono.Options;

    /// <summary>
    /// Main program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Listing all options
            string file = null;             // File to convert
            string outputFolder = null;     // The output folder path for destination files
            bool verbose = false;           // Verbosity
            bool help = false;              // Show help message

            var options = new OptionSet()
            {
                { "f|file=", "The C# {FILE} to convert into TypeScript.",
                  value => file = value },
                { "o|output=", "The {OUTPUT} folder path where to save files.",
                  value => outputFolder = value },
                { "v|verbose", "Increase debug message {VERBOSE} level.",
                  value => verbose = value != null },
                { "h|help",  "Show this message and exit.",
                  value => help = value != null },
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
            if (help)
            {
                ShowHelp(options);
                return;
            }

            // Executing
            // TODO
        }

        private static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: Rosetta [OPTIONS]+ message");
            Console.WriteLine("Converts C# files into TypeScript.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
