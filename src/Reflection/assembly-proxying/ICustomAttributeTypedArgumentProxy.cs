/// <summary>
/// ICustomAttributeTypedArgumentProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;

    /// <summary>
    /// Abstracts custom attribute info API.
    /// </summary>
    public interface ICustomAttributeTypedArgumentProxy
    {
        /// <summary>
        /// Gets the type of the argument or of the array argument element.
        /// </summary>
        ITypeProxy ArgumentType { get; }

        /// <summary>
        /// Gets the value of the argument for a simple argument or for an element of an array argument; gets a collection of values for an array argument.
        /// </summary>
        object Value { get; }
    }
}
