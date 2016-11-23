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
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Acts like a wrapper for <see cref="ProgramASTWalker"/> in order to provide 
    /// an easy interface for converting C# code.
    /// </summary>
    public class ProgramWrapper
    {
        // TODO: Make base class for program wrappers

        private readonly ProgramASTWalker walker;
        private readonly string assemblyPath;
        private readonly CSharpSyntaxTree tree;

        private SemanticModel semanticModel;
        private string output;

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

            this.assemblyPath = assemblyPath;

            // Getting the AST node
            this.tree = ASTExtractor.Extract(source);
            var node = tree.GetRoot() as CompilationUnitSyntax;

            // Creating the walker
            this.walker = ProgramASTWalker.Create(node);
            this.output = null;
        }

        /// <summary>
        /// Gets the output.
        /// </summary>
        public string Output
        {
            get
            {
                if (this.output == null)
                {
                    if (this.assemblyPath != null)
                    {
                        this.LoadSemanticModel(this.assemblyPath, this.tree);
                    }

                    this.output = this.walker.Walk().Translate();
                }

                return this.output;
            }
        }

        private void LoadSemanticModel(string path, CSharpSyntaxTree sourceTree)
        {
            var assembly = MetadataReference.CreateFromFile(path);
            var compilation = CSharpCompilation.Create("", new[] { sourceTree }, new[] { assembly });

            this.semanticModel = compilation.GetSemanticModel(sourceTree);
        }
    }
}
