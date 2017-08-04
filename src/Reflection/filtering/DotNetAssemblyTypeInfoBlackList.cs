/// <summary>
/// DotNetAssemblyTypeInfoBlackList.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Filters out some types in a fixed list.
    /// </summary>
    public class DotNetAssemblyTypeInfoBlackList : ITypeInfoFilter
    {
        private readonly TypeInfoBlackList blackList;

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetAssemblyTypeInfoBlackList"/> class.
        /// </summary>
        public DotNetAssemblyTypeInfoBlackList()
        {
            this.blackList = new TypeInfoBlackList(new[] 
            {
                "<Module>"
            });
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

            return this.blackList.Filter(types);
        }
    }
}
