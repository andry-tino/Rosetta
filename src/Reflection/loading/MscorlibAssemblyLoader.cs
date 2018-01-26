/// <summary>
/// MscorlibAssemblyLoader.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Loads the Mscorlib .NET assembly.
    /// </summary>
    public class MscorlibAssemblyLoader : IAssemblyLoader
    {
        private readonly string assemblyPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="MscorlibAssemblyLoader"/> class.
        /// </summary>
        public MscorlibAssemblyLoader()
        {
            this.assemblyPath = typeof(object).Assembly.Location;
        }

        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <returns>An <see cref="Assembly"/> after loading it from a source.</returns>
        public IAssemblyProxy Load() => new MonoFSAssemblyLoader(this.assemblyPath).Load();
    }
}
