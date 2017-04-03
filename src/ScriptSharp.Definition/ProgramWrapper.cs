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
    using Rosetta.AST.Helpers;
    using Rosetta.AST.Transformers;
    using Rosetta.ScriptSharp.AST.Transformers;
    using Rosetta.Utils;

    /// <summary>
    /// Acts like a wrapper for <see cref="ProgramDefinitionASTWalker"/> in order to provide 
    /// an easy interface for converting C# code.
    /// </summary>
    public class ProgramWrapper
    {
        // TODO: Make base class for program wrappers

        private readonly string source;
        private readonly string assemblyPath;

        // Lazy loaded or cached quantities
        private ProgramASTWalker walker;
        private CSharpSyntaxTree tree;
        private SemanticModel semanticModel;
        private string output;
        private bool initialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramWrapper"/> class.
        /// </summary>
        /// <param name="source">The source code</param>
        /// <param name="assemblyPath">The path to assembly for semantic model</param>
        public ProgramWrapper(string source, string assemblyPath = null)
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

            this.initialized = false;
        }

        /// <summary>
        /// Gets the output.
        /// </summary>
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

        private void Initialize()
        {
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

            // Translating
            this.output = this.walker.Walk().Translate();

            this.initialized = true;
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
