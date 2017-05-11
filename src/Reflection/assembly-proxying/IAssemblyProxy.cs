/// <summary>
/// IAssemblyProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstracts assembly API.
    /// </summary>
    public interface IAssemblyProxy
    {
        /// <summary>
        /// Gets a collection of the types defined in this assembly.
        /// </summary>
        IEnumerable<ITypeInfoProxy> DefinedTypes { get; }
    }
}
