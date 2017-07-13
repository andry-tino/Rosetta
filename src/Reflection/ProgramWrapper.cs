/// <summary>
/// ProgramWrapper.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.IO;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.AST;
    using Rosetta.Diagnostics.Logging;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Initiates the translation.
    /// </summary>
    public class ProgramWrapper
    {
        private readonly string assemblyPath;

        private ILogger logger;

        // Lazy loaded or cached quantities
        private IASTWalker walker;
        private CSharpSyntaxTree tree;
        private SemanticModel semanticModel;
        private string output;
        private bool initialized;
        private string info;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramWrapper"/> class.
        /// </summary>
        /// <param name="assemblyPath"></param>
        public ProgramWrapper(string assemblyPath)
        {
            if (assemblyPath == null)
            {
                throw new ArgumentNullException(nameof(assemblyPath));
            }

            if (!File.Exists(assemblyPath))
            {
                throw new ArgumentException("Invalid path", nameof(assemblyPath));
            }

            this.assemblyPath = assemblyPath;
        }

        /// <summary>
        /// Gets or sets the path to the log file to write. If set to <c>null</c> no logging is performed.
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// Gets the output of the generation.
        /// </summary>
        /// <remarks>
        /// Generation is lazy and triggered here if this is the first time this property or <see cref="Info"/> is invoked.
        /// </remarks>
        public string Output
        {
            get
            {
                if (!this.initialized)
                {
                    this.Initialize();
                }

                return this.output;
            }
        }

        /// <summary>
        /// Gets the diagnostic info of the generation.
        /// </summary>
        /// <remarks>
        /// Generation is lazy and triggered here if this is the first time this property or <see cref="Output"/> is invoked.
        /// </remarks>
        public string Info
        {
            get
            {
                if (!this.initialized)
                {
                    this.Initialize();
                }

                return this.info;
            }
        }

        protected virtual IASTBuilder CreateASTBuilder(IAssemblyProxy assembly) => new ASTBuilder(assembly) { Logger = this.Logger };

        protected virtual IAssemblyLoader CreateAssemblyLoader(string assemblyPath) => new MonoFSAssemblyLoader(assemblyPath);

        protected virtual IASTWalker CreateASTWalker(CSharpSyntaxNode node, SemanticModel semanticModel)
        {
            var walker = ProgramASTWalker.Create(node, null, semanticModel);
            walker.Logger = this.Logger;

            return walker;
        }

        private void Initialize()
        {
            IAssemblyProxy assembly = this.LoadAssembly();

            var builder = this.CreateASTBuilder(assembly);
            var astInfo = builder.Build();

            // Getting the AST node
            this.tree = astInfo.Tree as CSharpSyntaxTree;

            if (tree == null)
            {
                throw new InvalidOperationException("Invalid generated tree");
            }

            var node = this.tree.GetRoot();

            // Referencing the semantic model
            this.semanticModel = astInfo.SemanticModel;

            // Creating the walker
            this.walker = this.CreateASTWalker(node, this.semanticModel);

            // Translating
            this.output = this.walker.Walk().Translate();

            // Some info
            this.info = $"AST generation: {astInfo.ToString()}";

            this.initialized = true;
        }

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

        private ILogger CreateLogger()
        {
            if (this.LogPath != null)
            {
                return new FileLogger(this.LogPath);
            }

            return null;
        }

        private IAssemblyProxy LoadAssembly()
        {
            IAssemblyProxy assembly = null;

            try
            {
                var loader = this.CreateAssemblyLoader(this.assemblyPath);
                assembly = loader.Load();
            }
            catch (FileNotFoundException ex)
            {
                throw new InvalidOperationException("Invalid assembly path", ex);
            }
            catch (FileLoadException ex)
            {
                throw new InvalidOperationException("Invalid assembly path", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while loading assembly at {this.assemblyPath}", ex);
            }

            return assembly;
        }
    }
}
