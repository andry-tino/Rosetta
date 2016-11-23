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
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST;
    using Rosetta.AST.Transformers;
    using Rosetta.ScriptSharp.Definition.AST.Transformers;

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

            if (assemblyPath != null && !File.Exists(assemblyPath))
            {
                throw new ArgumentException(nameof(assemblyPath), "The specified assembly could not be found!");
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
            var node = this.tree.GetRoot();

            IASTTransformer transformer = new ScriptNamespaceBasedASTTransformer();
            transformer.Transform(ref node);

            // Loading the semantic model
            if (this.assemblyPath != null)
            {
                this.LoadSemanticModel(this.assemblyPath, this.tree);
            }

            // Creating the walker
            // If no semantic model was loaded, null will just be passed
            this.walker = ProgramDefinitionASTWalker.Create(node, null, this.semanticModel);

            // Translating
            this.output = this.walker.Walk().Translate();

            this.initialized = true;
        }

        private void LoadSemanticModel(string path, CSharpSyntaxTree sourceTree)
        {
            var assembly = MetadataReference.CreateFromFile(path);
            var compilation = CSharpCompilation.Create("LoadedAssembly", new[] { sourceTree }, new[] { assembly });

            this.semanticModel = compilation.GetSemanticModel(sourceTree);
        }
    }
}
