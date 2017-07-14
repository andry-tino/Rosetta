/// <summary>
/// ProgramWrapper.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST
{
    using System;
    using System.IO;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.AST;
    using Rosetta.Diagnostics.Logging;
    using Rosetta.AST.Helpers;
    using Rosetta.AST.Transformers;
    using Rosetta.ScriptSharp.AST.Transformers;
    using Rosetta.Utils;

    /// <summary>
    /// Acts like a wrapper for <see cref="ProgramDefinitionASTWalker"/> in order to provide 
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

            if (assemblyPath != null)
            {
                ValidatePaths(new[] { assemblyPath }, nameof(assemblyPath), "The specified assembly could not be found!");
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
            // TODO: In order to target #41, add an option for using the reflector when requested

            // Getting the AST node
            this.tree = ASTExtractor.Extract(this.source);

            // Loading the semantic model
            CSharpCompilation compilation = null;
            if (this.assemblyPath != null)
            {
                compilation = this.GetCompilation(this.assemblyPath, this.tree);
            }

            IASTTransformer transformer = new ScriptNamespaceBasedASTTransformer();
            if (compilation != null)
            {
                transformer.Transform(ref this.tree, ref compilation);
                this.semanticModel = SemanticUtils.RetrieveSemanticModel(compilation, this.tree);
            }
            else
            {
                transformer.Transform(ref this.tree);
            }

            // Creating the walker
            // If no semantic model was loaded, null will just be passed
            var node = this.tree.GetRoot();
            this.walker = ProgramDefinitionASTWalker.Create(node, null, this.semanticModel);
            (this.walker as ProgramDefinitionASTWalker).Logger = this.Logger;

            // Translating
            this.output = this.walker.Walk().Translate();
        }

        private CSharpCompilation GetCompilation(string path, CSharpSyntaxTree sourceTree)
        {
            return SemanticUtils.RetrieveCompilation("LoadedAssembly", path, sourceTree, true);
        }

        private static void ValidatePaths(string[] paths, string argumentName, string message)
        {
            foreach (var path in paths)
            {
                if (!File.Exists(path))
                {
                    throw new ArgumentException(argumentName, message);
                }
            }
        }
    }
}
