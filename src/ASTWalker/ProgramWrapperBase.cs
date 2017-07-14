/// <summary>
/// ProgramWrapperBase.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Diagnostics.Logging;

    /// <summary>
    /// Acts like a wrapper for <see cref="ProgramASTWalker"/> in order to provide 
    /// an easy interface for converting C# code.
    /// </summary>
    public abstract class ProgramWrapperBase
    {
        private ILogger logger;

        // Lazy loaded or cached quantities
        protected IASTWalker walker;
        protected CSharpSyntaxTree tree;
        protected SemanticModel semanticModel;
        protected string output;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramWrapper"/> class.
        /// </summary>
        public ProgramWrapperBase()
        {
            this.Initialized = false;
        }

        /// <summary>
        /// Gets or sets the path to the log file to write. If set to <c>null</c> no logging is performed.
        /// </summary>
        /// <remarks>
        /// Protected in order not to be exposed and requiring project dependencies to include this roject. 
        /// Define accessor in deriving class as shadowing member.
        /// </remarks>
        protected string LogPath { get; set; }

        /// <summary>
        /// Gets the output.
        /// </summary>
        /// <remarks>
        /// Protected in order not to be exposed and requiring project dependencies to include this roject. 
        /// Define accessor in deriving class as shadowing member.
        /// </remarks>
        protected string Output
        {
            get
            {
                if (!this.Initialized)
                {
                    this.Initialize();
                }

                return this.output;
            }
        }

        protected bool Initialized { get; set; }

        protected ILogger Logger
        {
            get
            {
                if (this.logger == null)
                {
                    this.logger = this.CreateLogger();
                }

                return this.logger;
            }
        }

        protected IDisposable BufferedLogger => this.Logger as IDisposable;

        protected abstract void InitializeCore();

        protected void Initialize()
        {
            this.InitializeCore();

            this.Initialized = true;
            this.FLushBufferedLogger();
        }

        private void FLushBufferedLogger()
        {
            // The logger might be bufferized, in this case, we need to make sure we 
            // flush the buffer. We do this by checking whether the logger is disposable
            if (this.BufferedLogger != null)
            {
                this.BufferedLogger.Dispose(); // This will flush the buffer
            }
        }

        private ILogger CreateLogger()
        {
            if (this.LogPath != null)
            {
                //return new FileLogger(this.LogPath); // Non buffered (slower)
                return new BufferedFileLogger(this.LogPath); // Buffered logging (faster)
            }

            return null;
        }
    }
}
