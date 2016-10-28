/// <summary>
/// Executable.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;
    using System.Collections.Generic;

    using Mono.Options;

    /// <summary>
    /// Base class for executables.
    /// </summary>
    public abstract class Executable
    {
        protected string[] args;
        protected OptionSet options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Executable"/>.
        /// </summary>
        /// <param name="args">The arguments passed to the executable.</param>
        public Executable(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            this.args = args;
        }

        /// <summary>
        /// Starts the program.
        /// </summary>
        public void Execute()
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

            this.ExecuteCore();
        }

        /// <summary>
        /// Runs the main logic.
        /// </summary>
        protected abstract void ExecuteCore();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected abstract void HandleOptionException(OptionException e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extra"></param>
        protected abstract void HandleExtraParameters(IEnumerable<string> extra);

        /// <summary>
        /// Override this for showing help.
        /// </summary>
        protected abstract void ShowHelp();
    }
}
