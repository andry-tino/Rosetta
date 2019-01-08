/// <summary>
/// ISyntaxFilterOut.cs
/// Andrea Tino - 2018
/// </summary>

namespace Rosetta.Reflection.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describes classes which can handle the filtering out of types.
    /// </summary>
    /// <remarks>
    /// The type filtering mechanism works this way:
    /// - Declarations of types which appear in the filter-out list will not be emitted.
    /// - References to types which appear in the filter-out list will be emitted as `any`.
    /// - Types whose baselist contains a type which appears in the filter-out list, will not emit that type in the baselist.
    /// </remarks>
    public interface ISyntaxFilterOut
    {

        /// <summary>
        /// Gets or sets the collection of types to filter out.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> FilteredOutTypes { get; set; }
    }

    /// <summary>
    /// Geta a value indicating whether a typename is actually to be filtered out.
    /// </summary>
    public static class SyntaxFilterOutUtilities
    {
        public static bool IsTypeFilteredOut(this string typeName, IEnumerable<string> filteredTypes)
        {
            if (typeName == null)
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            if (filteredTypes == null)
            {
                throw new ArgumentNullException(nameof(filteredTypes));
            }

            return filteredTypes.Any(typeToFilterOut => typeToFilterOut == typeName);
        }
    }
}
