/// <summary>
/// IParameterInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;

    /// <summary>
    /// Abstracts type info API.
    /// </summary>
    public interface IParameterInfoProxy
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Type of this parameter.
        /// </summary>
        ITypeProxy ParameterType { get; }
    }
}
