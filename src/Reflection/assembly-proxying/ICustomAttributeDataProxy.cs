/// <summary>
/// ICustomAttributeDataProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstracts type info API.
    /// </summary>
    public interface ICustomAttributeDataProxy
    {
        /// <summary>
        /// Gets the type of the attribute.
        /// </summary>
        ITypeProxy AttributeType { get; }

        /// <summary>
        /// Gets the list of positional arguments specified for the attribute instance represented by the CustomAttributeData object.
        /// </summary>
        IEnumerable<ICustomAttributeTypedArgumentProxy> ConstructorArguments { get; }
    }
}
