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
    using Rosetta.Reflection.Proxies;

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

        protected virtual IASTBuilder CreateASTBuilder(IAssemblyProxy assembly, Stream rawAssembly) => new ASTBuilder(assembly, rawAssembly);

        protected virtual IAssemblyLoader CreateAssemblyLoader(string assemblyPath) => new MonoFSAssemblyLoader(assemblyPath);

        private void Initialize()
        {
            LoadedAssembly loadedAssembly = this.LoadAssembly();
            IAssemblyProxy assembly = loadedAssembly.AssemblyProxy;
            Stream rawAssembly = loadedAssembly.RawAssembly;

            var builder = this.CreateASTBuilder(assembly, rawAssembly);
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

        private LoadedAssembly LoadAssembly()
        {
            IAssemblyProxy assembly = null;
            Stream rawAssembly = null;

            try
            {
                var loader = this.CreateAssemblyLoader(this.assemblyPath);

                assembly = loader.Load();
                rawAssembly = loader.RawAssembly;
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

            return new LoadedAssembly
            {
                AssemblyProxy = assembly,
                RawAssembly = rawAssembly
            };
        }

        #region Types

        private class LoadedAssembly
        {
            public IAssemblyProxy AssemblyProxy { get; set; }
            public Stream RawAssembly { get; set; }
        }

        #endregion
    }
}
