/// <summary>
/// ITypeProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstracts type API.
    /// </summary>
    public interface ITypeProxy
    {
        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        string Name { get; }
    }
}
