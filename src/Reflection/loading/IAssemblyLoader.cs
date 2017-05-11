/// <summary>
/// IAssemblyLoader.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.IO;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Abstraction for loading the assembly.
    /// </summary>
    public interface IAssemblyLoader
    {
        /// <summary>
        /// Loads the assembly.
        /// </summary>
        /// <returns>An <see cref="IAssemblyProxy"/> after loading it from a source.</returns>
        IAssemblyProxy Load();

        /// <summary>
        /// Gets the raw assembly stream.
        /// </summary>
        /// <remarks>
        /// This property will return a value only after <see cref="Load"/> has been called. 
        /// The value corresponds to the raw assembly relatively to the last call to <see cref="Load"/>. 
        /// If <see cref="Load"/> was not called at least once, this property will return <code>null</code>.
        /// </remarks>
        Stream RawAssembly { get; }
    }
}
