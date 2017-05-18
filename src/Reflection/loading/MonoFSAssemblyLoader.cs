/// <summary>
/// MonoFSAssemblyLoader.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;

    using Mono.Cecil;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Loads an assembly from the file system using Mono.
    /// </summary>
    public class MonoFSAssemblyLoader : IAssemblyLoader
    {
        private readonly string assemblyPath;

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
            return this.LoadCore();
        }

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
    }
}
