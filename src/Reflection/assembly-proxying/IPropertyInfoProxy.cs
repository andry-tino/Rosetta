/// <summary>
/// IPropertyInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;

    /// <summary>
    /// Abstracts property info API.
    /// </summary>
    public interface IPropertyInfoProxy
    {
        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the return type of this method.
        /// </summary>
        ITypeProxy PropertyType { get; }

        /// <summary>
        /// Gets a value indicating whether the property can be read.
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// Gets a value indicating whether the property can be written to.
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        /// Gets a value indicating whether theproperty is visible only within its class 
        /// and derived classes.
        /// </summary>
        bool IsFamily { get; }

        /// <summary>
        /// Gets a value indicating whether this member is private.
        /// </summary>
        bool IsPrivate { get; }

        /// <summary>
        /// Gets a value indicating whether this member is public.
        /// </summary>
        bool IsPublic { get; }

        /// <summary>
        /// Gets a value indicating whether the member is static.
        /// </summary>
        bool IsStatic { get; }
    }
}
