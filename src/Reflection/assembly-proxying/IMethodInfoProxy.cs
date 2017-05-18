/// <summary>
/// IMethodInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;

    /// <summary>
    /// Abstracts method info API.
    /// </summary>
    public interface IMethodInfoProxy : IMethodBaseProxy
    {
        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the return type of this method.
        /// </summary>
        ITypeProxy ReturnType { get; }
    }
}
