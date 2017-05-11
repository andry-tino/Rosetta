/// <summary>
/// MonoStreamAssemblyLoader.cs
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
    /// <remarks>
    /// Once loaded the first time, the assembly is cached.
    /// </remarks>
    public class MonoStreamAssemblyLoader : IAssemblyLoader
    {
        private readonly Stream assemblyStream;

        // Cached quantities
        private IAssemblyProxy assemblyProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoStreamAssemblyLoader"/> class.
        /// </summary>
        /// <param name="assemblyStream"></param>
        public MonoStreamAssemblyLoader(Stream assemblyStream)
        {
            if (assemblyStream == null)
            {
                throw new ArgumentNullException(nameof(assemblyStream));
            }

            this.assemblyStream = assemblyStream;
        }

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <returns>An <see cref="Assembly"/> after loading it from a source.</returns>
        public IAssemblyProxy Load()
        {
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
        public Stream RawAssembly => this.AssemblyStream;

        protected Stream AssemblyStream => this.assemblyStream;

        protected virtual IAssemblyProxy LoadCore()
        {
            if (this.assemblyProxy == null)
            {
                ModuleDefinition module = null;
                try
                {
                    module = ModuleDefinition.ReadModule(this.AssemblyStream);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"An error occurred while loading module stream", ex);
                }

                this.assemblyProxy = new MonoAssemblyProxy(module);
            }
            
            return this.assemblyProxy;
        }
    }
}
