/// <summary>
/// TypeInfoBlackList.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Filters out some types in a fixed list.
    /// </summary>
    public class TypeInfoBlackList : ITypeInfoFilter
    {
        private readonly IEnumerable<string> blackListedNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInfoBlackList"/> class.
        /// </summary>
        /// <param name="blackListedNames"></param>
        public TypeInfoBlackList(IEnumerable<string> blackListedNames)
        {
            if (blackListedNames == null)
            {
                throw new ArgumentNullException(nameof(blackListedNames));
            }

            this.blackListedNames = blackListedNames;
        }

        /// <summary>
        /// Filters a collection of <see cref="ITypeInfoProxy"/>.
        /// </summary>
        /// <param name="types">The original collection to filter.</param>
        /// <returns>A filtered collection of <see cref="ITypeInfoProxy"/>.</returns>
        public IEnumerable<ITypeInfoProxy> Filter(IEnumerable<ITypeInfoProxy> types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            return types.Where(type => BlackListedNames.Where(blackListedName => type.FullName == blackListedName).Count() == 0);
        }

        protected IEnumerable<string> BlackListedNames => this.blackListedNames;
    }
}
