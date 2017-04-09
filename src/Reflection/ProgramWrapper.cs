/// <summary>
/// ProgramWrapper.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.AST;

    /// <summary>
    /// Initiates the translation.
    /// </summary>
    public class ProgramWrapper
    {
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

        protected virtual IASTBuilder CreateASTBuilder(Assembly assembly) => new ASTBuilder(assembly);

        private void Initialize()
        {
            Assembly assembly = null;
            try
            {
                //assembly = Assembly.LoadFrom(this.assemblyPath);
                assembly = Assembly.ReflectionOnlyLoadFrom(this.assemblyPath);
            }
            catch (FileNotFoundException ex)
            {
                throw new InvalidOperationException("Invalid assembly path", ex);
            }
            catch (FileLoadException ex)
            {
                throw new InvalidOperationException("Invalid assembly path", ex);
            }

            var builder = this.CreateASTBuilder(assembly);
            var astInfo = builder.Build();

            // Getting the AST node
            this.tree = astInfo.Tree as CSharpSyntaxTree;

            if (tree == null)
            {
                throw new InvalidOperationException("Invalid generated tree");
            }

            var node = this.tree.GetRoot();

            // Loading the semantic model
            this.semanticModel = astInfo.CompilationUnit.GetSemanticModel(this.tree);

            // Creating the walker
            this.walker = ProgramASTWalker.Create(node, null, this.semanticModel);

            // Translating
            this.output = this.walker.Walk().Translate();

            this.initialized = true;
        }
    }
}
