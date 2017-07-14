/// <summary>
/// ProgramWrapper.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using System.IO;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    
    using Rosetta.Diagnostics.Logging;
    using Rosetta.AST.Helpers;
    using Rosetta.Utils;

    /// <summary>
    /// Acts like a wrapper for <see cref="ProgramASTWalker"/> in order to provide 
    /// an easy interface for converting C# code.
    /// </summary>
    public class ProgramWrapper : ProgramWrapperBase
    {
        private readonly string source;
        private readonly string assemblyPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramWrapper"/> class.
        /// </summary>
        /// <param name="source">The source code</param>
        /// <param name="assemblyPath">The path to assembly for semantic model</param>
        public ProgramWrapper(string source, string assemblyPath = null) 
            : base()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (assemblyPath != null && !File.Exists(assemblyPath))
            {
                throw new ArgumentException(nameof(assemblyPath), "The specified assembly could not be found!");
            }

            this.source = source;
            this.assemblyPath = assemblyPath;
        }

        /// <summary>
        /// Gets or sets the path to the log file to write. If set to <c>null</c> no logging is performed.
        /// </summary>
        public new string LogPath
        {
            get { return base.LogPath; }
            set { base.LogPath = value; }
        }

        /// <summary>
        /// Gets the output.
        /// </summary>
        public new string Output
        {
            get { return base.Output; }
        }

        protected override void InitializeCore()
        {
            // Getting the AST node
            this.tree = ASTExtractor.Extract(this.source);
            var node = this.tree.GetRoot();

            // Loading the semantic model
            if (this.assemblyPath != null)
            {
                this.LoadSemanticModel(this.assemblyPath, this.tree);
            }

            // Creating the walker
            // If no semantic model was loaded, null will just be passed
            this.walker = ProgramASTWalker.Create(node, null, this.semanticModel);
            (this.walker as ProgramASTWalker).Logger = this.Logger;

            // Translating
            this.output = this.walker.Walk().Translate();
        }
        
        private void LoadSemanticModel(string path, CSharpSyntaxTree sourceTree)
        {
            this.semanticModel = SemanticUtils.RetrieveSemanticModel("LoadedAssembly", path, sourceTree, true);
        }
    }
}
