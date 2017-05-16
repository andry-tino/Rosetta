/// <summary>
/// ITypeProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;

    /// <summary>
    /// Abstracts type API.
    /// </summary>
    /// <remarks>
    /// The difference with <see cref="ITypeInfoProxy"/> is that this one does not provide info about the type definition.
    /// </remarks>
    public interface ITypeProxy
    {
        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        string FullName { get; }
    }
}
