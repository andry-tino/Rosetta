/// <summary>
/// ITypeLookup.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Abstraction for looking a type definition into an assembly.
    /// </summary>
    public interface ITypeLookup
    {
        /// <summary>
        /// Searches in the assembly for all occurrances of the specified type by name (partial match on the type full name).
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>A collection of <see cref="ITypeInfoProxy"/>. If none is found, the collection will be returned empty.</returns>
        IEnumerable<ITypeInfoProxy> GetAllByName(string name);

        /// <summary>
        /// Searches in the assembly for the specified type by name (partial match on the type full name).
        /// The first match is returned.
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>An <see cref="ITypeInfoProxy"/> or <code>null</code> if not found.</returns>
        ITypeInfoProxy GetByName(string name);

        /// <summary>
        /// Searches in the assembly for all occurrances of the specified type by name (exact match on the type full name).
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>A collection of <see cref="ITypeInfoProxy"/>. If none is found, the collection will be returned empty.</returns>
        IEnumerable<ITypeInfoProxy> GetAllByFullName(string fullName);

        /// <summary>
        /// Searches in the assembly for the specified type by name (exact match on the type full name).
        /// The first match is returned.
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>An <see cref="ITypeInfoProxy"/> or <code>null</code> if not found.</returns>
        ITypeInfoProxy GetByFullName(string fullName);
    }
}
