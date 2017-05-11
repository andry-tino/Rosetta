/// <summary>
/// ITypeInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstracts type info API.
    /// </summary>
    public interface ITypeInfoProxy
    {
        /// <summary>
        /// Gets a value indicating whether the Type is a class or a delegate; that is, 
        /// not a value type or interface.
        /// </summary>
        bool IsClass { get; }

        /// <summary>
        /// Gets a value indicating whether the Type is a value type.
        /// </summary>
        bool IsValueType { get; }

        /// <summary>
        /// Gets a value indicating whether the Type is an interface; that is, 
        /// not a class or a value type.
        /// </summary>
        bool IsInterface { get; }

        /// <summary>
        /// Gets a value indicating whether the current Type represents an enumeration.
        /// </summary>
        bool IsEnum { get; }

        /// <summary>
        /// Gets the namespace of the Type.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a collection that contains this member's custom attributes.
        /// </summary>
        IEnumerable<ICustomAttributeDataProxy> CustomAttributes { get; }
    }
}
