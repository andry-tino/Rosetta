/// <summary>
/// MonoFSAssemblyLoader.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.IO;

    using Mono.Cecil;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Loads an assembly from the file system using Mono.
    /// </summary>
    public class MonoFSAssemblyLoader : IAssemblyLoader
    {
        private readonly string assemblyPath;

        // Cached quantities
        private Stream rawAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoFSAssemblyLoader"/> class.
        /// </summary>
        /// <param name="assemblyPath"></param>
        public MonoFSAssemblyLoader(string assemblyPath)
        {
            if (assemblyPath == null)
            {
                throw new ArgumentNullException(nameof(assemblyPath));
            }

            this.assemblyPath = assemblyPath;
        }

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <returns>An <see cref="Assembly"/> after loading it from a source.</returns>
        public IAssemblyProxy Load()
        {
            this.LoadStream();

            return this.LoadCore();
        }

        /// <summary>
        /// Gets the raw assembly stream.
        /// </summary>
        /// <remarks>
        /// This property will return a value only after <see cref="Load"/> has been called. 
        /// The value corresponds to the raw assembly relatively to the last call to <see cref="Load"/>. 
        /// If <see cref="Load"/> was not called at least once, this property will return <code>null</code>.
        /// </remarks>
        public Stream RawAssembly => this.rawAssembly;

        protected string AssemblyPath => this.assemblyPath;

        protected virtual IAssemblyProxy LoadCore()
        {
            ModuleDefinition module = null;

            try
            {
                module = ModuleDefinition.ReadModule(this.AssemblyPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while loading module {this.AssemblyPath}", ex);
            }

            return new MonoAssemblyProxy(module);
        }

        protected virtual void LoadStream()
        {
            using (FileStream fs = File.OpenRead(this.AssemblyPath))
            {
                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);

                this.rawAssembly = ms;
            }
        }
    }
}
