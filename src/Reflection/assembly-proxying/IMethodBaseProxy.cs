/// <summary>
/// IMethodBaseProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Abstracts method info API.
    /// </summary>
    public interface IMethodBaseProxy
    {
        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the parameters of the specified method or constructor.
        /// </summary>
        IEnumerable<IParameterInfoProxy> Parameters { get; }

        /// <summary>
        /// Gets a value indicating whether the method or constructor is visible only within its class 
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
    }
}
