/// <summary>
/// Program.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Runner
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rosetta.Executable;
    using Rosetta.Executable.Exceptions;
    using Rosetta.ScriptSharp.Definition.AST;

    using Mono.Options;

    /// <summary>
    /// Main program.
    /// </summary>
    /// <remarks>
    /// Members protected for testability.
    /// </remarks>
    internal partial class Program
    {
        protected static Program instance;

        protected string[] args;
        protected OptionSet options;

        protected string filePath = null;         // File to convert
        protected string outputFolder = null;     // The output folder path for destination files
        protected string fileName = null;         // The output file name
        protected bool verbose = false;           // Verbosity
        protected bool help = false;              // Show help message

        protected FileManager fileManager;

        public const string UnnamedArgumentName = "unnamed";

        public const string FileArgumentName = "file";
        public const string FileArgumentChar = "f";
        public const string OutputArgumentName = "output";
        public const string OutputArgumentChar = "o";
        public const string FileNameArgumentName = "filename";
        public const string FileNameArgumentChar = "n";
        public const string VerboseArgumentName = "verbose";
        public const string VerboseArgumentChar = "v";
        public const string HelpArgumentName = "help";
        public const string HelpArgumentChar = "h";

        public string FileOption
        {
            get { return string.Format("{0}|{1}=", FileArgumentName, FileArgumentChar); }
        }

        public string OutputOption
        {
            get { return string.Format("{0}|{1}=", OutputArgumentName, OutputArgumentChar); }
        }

        public string FileNameOption
        {
            get { return string.Format("{0}|{1}=", FileNameArgumentName, FileNameArgumentChar); }
        }

        public string VerboseOption
        {
            get { return string.Format("{0}|{1}", VerboseArgumentName, VerboseArgumentChar); }
        }

        public string HelpOption
        {
            get { return string.Format("{0}|{1}", HelpArgumentName, HelpArgumentChar); }
        }

        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            instance = new Program(args);
            instance.Run();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        /// <param name="args"></param>
        public Program(string[] args)
        {
            this.args = args;
            this.options = new OptionSet()
            {
                { FileOption, "The path to the C# {FILE} to convert into TypeScript.",
                  value => this.filePath = value },
                { OutputOption, "The {OUTPUT} folder path where Rosetta will emit all output files.",
                  value => this.outputFolder = value },
                { FileNameOption, "The {FILENAME} to use for output file. Valid only when {FILE} is specified.",
                  value => this.fileName = value },
                { VerboseOption, "Increase debug message {VERBOSE} level.",
                  value => this.verbose = value != null },
                { HelpOption,  "Show this message and exit.",
                  value => this.help = value != null },
            };
        }

        /// <summary>
        /// Starts the program.
        /// </summary>
        public void Run()
        {
            List<string> extra;
            try
            {
                extra = this.options.Parse(this.args);
                this.HandleExtraParameters(extra);
            }
            catch (OptionException e)
            {
                this.HandleOptionException(e);
                return;
            }

            // If user provided no input arguments, show help
            if (this.args.Length == 0)
            {
                Console.Write("No input provided!");
                this.ShowHelp();

                return;
            }

            this.RunCore();
        }

        /// <summary>
        /// Runs the main logic.
        /// </summary>
        protected virtual void RunCore()
        {
            // Priority to help
            if (help)
            {
                this.ShowHelp();
                return;
            }

            try
            {
                if (this.filePath != null)
                {
                    this.ConvertFile();
                    return;
                }

                // If we get to here, then basically nothing happens, the user needs to specify options
                this.HandleNoFeasibleExecution();
            }
            catch (Exception e)
            {
                this.HandleError(e);
                return;
            }
        }

        protected virtual void HandleError(Exception e)
        {
            Console.WriteLine("An error occurred!");

#if DEBUG
            Console.WriteLine("Error details below:");
            Console.WriteLine("An error occurred: {0}!", e.Message);
            Console.WriteLine(e.StackTrace);
#endif
        }

        protected virtual void HandleOptionException(OptionException e)
        {
            Console.Write("An error occurred while reading input: ");
            Console.WriteLine(e.Message);
            Console.WriteLine("Try using option `--help' for more information.");
        }

        protected virtual void HandleNoFeasibleExecution()
        {
            Console.WriteLine("A file or a project to convert should be specified!");
            this.ShowHelp();
        }

        protected virtual void HandleExtraParameters(IEnumerable<string> extra)
        {
            int count = extra.Count();

            if (count == 0)
            {
                return;
            }

            // An extra parameter is allowed: implicit [--file]
            // However if more unhandled parameters are found, this is an error
            if (count > 1)
            {
                throw new OptionException("Cannot handle more than one unnamed parameter!",
                    "Default parameters", new DefaultOptionException(extra.ToArray()));
            }

            // Also, if parameter --file has been specified together with one unnamed 
            // parameter, we will throw an exception indicatring the conflict
            if (this.filePath != null)
            {
                throw new OptionException("Conflict occurred when processing input: " +
                    "an unnamed parameter and a file-path parameter have been both specified.",
                    FileArgumentName, new ConflictingOptionsException(UnnamedArgumentName, FileArgumentName));
            }

            // If everything is fine, just apply to file-path
            this.filePath = extra.ElementAt(0);
        }

        private static string PerformConversion(string source)
        {
            var program = new ProgramWrapper(source);

            return program.Output;
        }

        #region Helpers

        protected virtual void ShowHelp()
        {
            Console.WriteLine("Usage: RosettaScriptSharpDefinition [OPTIONS]+ message");
            Console.WriteLine("Generates TypeScript definition files from C# files.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            this.options.WriteOptionDescriptions(Console.Out);
        }

        #endregion
    }
}
