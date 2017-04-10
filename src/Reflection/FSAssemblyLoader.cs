/// <summary>
/// FSAssemblyLoader.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Loads an assembly from the sile system.
    /// </summary>
    public class FSAssemblyLoader : IAssemblyLoader
    {
        private readonly string assemblyPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FSAssemblyLoader"/> class.
        /// </summary>
        /// <param name="assemblyPath"></param>
        public FSAssemblyLoader(string assemblyPath)
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
        public Assembly Load()
        {
            return this.LoadCore();
        }

        protected string AssemblyPath => this.assemblyPath;

        protected virtual Assembly LoadCore() => Assembly.LoadFrom(this.AssemblyPath);
    }
}
