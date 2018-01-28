/// <summary>
/// ITypeInfoFilter.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Describes a filter for <see cref="ITypeInfoProxy"/>.
    /// </summary>
    /// <remarks>
    /// This interface describes very simple filters that just remove a entry in a list of type definitions. 
    /// If the filtered type is referenced by other types, those references are not removed.
    /// </remarks>
    public interface ITypeInfoFilter
    {
        /// <summary>
        /// Filters a collection of <see cref="ITypeInfoProxy"/>.
        /// </summary>
        /// <param name="types">The original collection to filter.</param>
        /// <returns>A filtered collection of <see cref="ITypeInfoProxy"/>.</returns>
        IEnumerable<ITypeInfoProxy> Filter(IEnumerable<ITypeInfoProxy> types);
    }
}
