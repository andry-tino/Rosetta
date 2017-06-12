/// <summary>
/// LinearSearchTypeLookup.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Looks up type in an assembly by performing linear search among the declared types.
    /// </summary>
    public class LinearSearchTypeLookup : ITypeLookup
    {
        private readonly IAssemblyProxy assembly;

        public LinearSearchTypeLookup(IAssemblyProxy assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            this.assembly = assembly;
        }

        /// <summary>
        /// Searches in the assembly for all occurrances of the specified type by name (partial match on the type full name).
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>A collection of <see cref="ITypeInfoProxy"/>. If none is found, the collection will be returned empty.</returns>
        public IEnumerable<ITypeInfoProxy> GetAllByName(string name)
        {
            var matches = from type in this.Types
                          where type.FullName.Contains(name)
                          select type;

            return matches;
        }

        /// <summary>
        /// Searches in the assembly for the specified type by name (partial match on the type full name).
        /// The first match is returned.
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>An <see cref="ITypeInfoProxy"/> or <code>null</code> if not found.</returns>
        public ITypeInfoProxy GetByName(string name)
        {
            var matches = this.GetAllByName(name);

            if (matches != null && matches.Count() > 0)
            {
                return matches.First();
            }

            return null;
        }

        /// <summary>
        /// Searches in the assembly for all occurrances of the specified type by name (exact match on the type full name).
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>A collection of <see cref="ITypeInfoProxy"/>. If none is found, the collection will be returned empty.</returns>
        public IEnumerable<ITypeInfoProxy> GetAllByFullName(string fullName)
        {
            var matches = from type in this.Types
                          where type.FullName == fullName
                          select type;

            return matches;
        }

        /// <summary>
        /// Searches in the assembly for the specified type by name (exact match on the type full name).
        /// The first match is returned.
        /// </summary>
        /// <param name="name">The name of the type to search for.</param>
        /// <returns>An <see cref="ITypeInfoProxy"/> or <code>null</code> if not found.</returns>
        public ITypeInfoProxy GetByFullName(string fullName)
        {
            var matches = this.GetAllByFullName(fullName);

            if (matches != null && matches.Count() > 0)
            {
                return matches.First();
            }

            return null;
        }

        private IEnumerable<ITypeInfoProxy> Types => this.assembly.DefinedTypes;
    }
}
